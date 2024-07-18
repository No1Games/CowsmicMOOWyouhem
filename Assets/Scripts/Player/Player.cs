using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody _playerRB;

    private StatsSystem _statsSystem;

    private PlayersInput _playersInput;

    private float _currentHP;
    private float _moveSpeed;
    private float _jumpTime;

    private Vector3 lookPos;

    private void OnEnable()
    {
        _playersInput.GameInput.Enable();
        _playersInput.GameInput.Jump.performed += _ => Jump();
    }

    private void OnDisable()
    {
        _playersInput.GameInput.Disable();
    }

    private void Awake()
    {
        _playersInput = new PlayersInput();
    }

    void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
        _statsSystem = FindObjectOfType<StatsSystem>();

        _currentHP = _statsSystem.GetStatValue(StatsEnum.HealthPoints);
        _moveSpeed = _statsSystem.GetStatValue(StatsEnum.MoveSpeed);
        _jumpTime = _statsSystem.GetStatValue(StatsEnum.JumpTime);
    }

    void Update()
    {
        Move();
        Facing();
    }

    private void Move()
    {
        Vector2 moveDirection = _playersInput.GameInput.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y);

        _playerRB.MovePosition(transform.position + movement * _moveSpeed * Time.deltaTime);
    }

    private void Facing()
    {
        Vector2 mousePos = _playersInput.GameInput.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            lookPos = hit.point;
        }

        Vector3 lookDirection = lookPos - transform.position;
        lookDirection.y = 0;

        transform.LookAt(transform.position + lookDirection, Vector3.up);
    }

    private void Jump()
    {
        Debug.Log("JUMPING");
    }
}
