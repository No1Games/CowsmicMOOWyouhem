using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Space]
    [SerializeField] float moveSpeed;
    [SerializeField] float dashPower;
    private float _currentHP;
    private float _maxHP = 100;

    [Header("Technical staff")]
    PlayersInput control;
    Rigidbody rb;
    Camera cam;
    Vector3 lookPos;

    Animator animator;

    HealthBar HBScript;

    public float maxHP
    {
        get { return _maxHP; }
        private set { _maxHP = value; }
    }
    public float currentHP
    {
        get { return _currentHP; }
        private set { _currentHP = value; }
    }

    private void Awake()
    {
        control = new PlayersInput();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        _currentHP = _maxHP;
        animator = GetComponentInChildren<Animator>();
        HBScript = GameObject.Find("healthbar").GetComponent<HealthBar>();
        HBScript.SetMaxValue(_maxHP);
        HBScript.SetCurrentValue(_currentHP);
        //healthTMP.text = currentHP.ToString();
    }

    private void OnEnable()
    {
        
        control.GameInput.Enable();
        //control.GameInput.Dash.performed += _ => Dash();
        
    }

    void Update()
    {
        PlayerMove();
        PlayerTarget();
        MoveChecker();

        //CameraFollow тимчасова
        Vector3 followPosition = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z - 20);
        cam.transform.position = followPosition;



    }

    void PlayerMove()
    {
        Vector2 moveDirection = control.GameInput.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

    }

    void MoveChecker()
    {
        Vector2 _moveDirection = control.GameInput.Movement.ReadValue<Vector2>();
        if (_moveDirection.x == 0 && _moveDirection.y == 0)
        {
            
            animator.ResetTrigger("StartWalking");
            animator.SetTrigger("StopWalking");
        }
        else
        {
            animator.ResetTrigger("StopWalking");
            animator.SetTrigger("StartWalking");
        }
    }

    void PlayerTarget() //TODO: пофіксити проблему коли рей не б'ється ні в що.
    {
        Vector2 mousePos = control.GameInput.MousePosition.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
        lookPos = hit.point;

        }

        Vector3 lookDirection = lookPos - transform.position;
        lookDirection.y = 0;

        transform.LookAt(transform.position + lookDirection, Vector3.up);

    }

    void Dash()
    {
        Vector2 moveDirection = control.GameInput.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y);
        if(movement.y !=0 || movement.x != 0)
        {
            rb.AddForce(movement * dashPower, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(transform.forward * dashPower, ForceMode.Impulse);
        }
        
    }


    public void TakeDamage(float damage)
    {
        Debug.Log("hit");
        if (_currentHP > damage)
        {
            _currentHP -= damage;
            HBScript.SetCurrentValue(_currentHP);
        }
        else
        {
            _currentHP = 0;
            HBScript.SetCurrentValue(_currentHP);
            Destroy(gameObject);
        }
        Debug.Log(_currentHP);
        //healthTMP.text = currentHP.ToString();
    }
    

    private void OnDisable()
    {
        control.GameInput.Disable();
    }
}
