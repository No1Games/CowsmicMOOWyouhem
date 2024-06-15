using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [Header("Player parameters")]
    
    [SerializeField] float moveSpeed;
    [SerializeField] float dashPower;

    [Header("Technical staff")]
    PlayersInput control;
    Rigidbody rb;
    Camera cam;
    Vector3 lookPos;
    
    [SerializeField] float camDistance;
    
   

    private void Awake()
    {
        control = new PlayersInput();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        
    }
    private void OnEnable()
    {
        control.GameInput.Enable();
        control.GameInput.Dash.performed += _ => Dash();
        
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerTarget();
        
        //CameraFollow тимчасова
        //Vector3 followPosition = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z- camDistance); 
        //cam.transform.position = followPosition;
        


    }

    void PlayerMove()
    {
        Vector2 moveDirection = control.GameInput.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

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

    

    

    private void OnDisable()
    {
        control.GameInput.Disable();
    }
}
