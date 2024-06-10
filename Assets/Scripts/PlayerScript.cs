using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    PlayersInput control;
    Rigidbody rb;
    [SerializeField] float moveSpeed;

    private void Awake()
    {
        control = new PlayersInput();
        rb = GetComponent<Rigidbody>();
        
    }
    private void OnEnable()
    {
        control.GameInput.Enable();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerTarget();


    }

    void PlayerMove()
    {
        Vector2 moveDirection = control.GameInput.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

     }

    void PlayerTarget() // TODO треба переробити те як персонаж слідкує за курсором бо це не працює
    {
        Vector2 mousePos = control.GameInput.MousePosition.ReadValue<Vector2>();
        Vector3 targetPos = new Vector3(mousePos.x, 0, mousePos.y);
        transform.LookAt(targetPos);
    }

    private void OnDisable()
    {
        control.GameInput.Disable();
    }
}
