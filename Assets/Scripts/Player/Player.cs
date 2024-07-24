using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _playerRB;

    private StatsSystem _statsSystem;

    private PlayersInput _playersInput;

    private float _currentHP;
    private float _moveSpeed;

    [Header("Jumping")]
    private float _jumpTime;
    [SerializeField] float _jumpHeight;
    [SerializeField] float _jumpForce;
    [SerializeField] float _fallMultiplier;
    Vector3 _vecGravity;

    private float _currentJumpTime;
    private bool _isJumping = false;
    private bool _isFalling = false;

    private Vector3 _lookPos;

    private void OnEnable()
    {
        _playersInput.GameInput.Enable();
        
        _playersInput.GameInput.Jump.performed += _ => Jump();
        
        //_playersInput.GameInput.Jump.performed += _ => StartJump();
        //_playersInput.GameInput.Jump.canceled += _ => StartFall();
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

        _vecGravity = new Vector3(0, -Physics2D.gravity.y, 0);

        _currentHP = _statsSystem.GetStatValue(StatsEnum.HealthPoints);
        _moveSpeed = _statsSystem.GetStatValue(StatsEnum.MoveSpeed);
        _jumpTime = _statsSystem.GetStatValue(StatsEnum.JumpTime);
    }

    void Update()
    {        
        Move();
        Facing();
        //AirborneMove();
        //CheckGround();

        if(_playerRB.velocity.y < 0)
        {
            _playerRB.velocity -= _vecGravity * _fallMultiplier * Time.deltaTime;
        }
    }

    private void Jump()
    {
        if(_isJumping) return;

        Debug.Log("JUMP");

        _isJumping = true;
        _playerRB.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
    }

    private void Move()
    {
        Vector2 moveDirection = _playersInput.GameInput.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y);

        _playerRB.MovePosition(transform.position + movement * _moveSpeed * Time.deltaTime);
    }

    private void AirborneMove()
    {
        if (!_isJumping && !_isFalling) return;

        _currentJumpTime += Time.deltaTime;
        float t = _currentJumpTime / _jumpTime;

        if (_isJumping)
        {
            if (t <= 0.25f) // First 25% of the jump
            {
                _playerRB.velocity = new Vector3(_playerRB.velocity.x, _jumpHeight * 4 * (t / 0.25f), _playerRB.velocity.z);
            }
            else if (t <= 0.75f) // Middle 50% of the jump
            {
                _playerRB.velocity = new Vector3(_playerRB.velocity.x, _jumpHeight, _playerRB.velocity.z);
            }
            else if (t <= 1.0f) // Last 25% of the jump
            {
                _isFalling = true;
                _isJumping = false;
            }
        }

        if (_isFalling)
        {
            _playerRB.velocity += Vector3.up * Physics.gravity.y * Time.deltaTime;
        }

        // Allow horizontal movement while in the air
        Vector2 moveDirection = _playersInput.GameInput.Movement.ReadValue<Vector2>();
        Vector3 horizontalMovement = new Vector3(moveDirection.x, 0, moveDirection.y);
        _playerRB.velocity = new Vector3(horizontalMovement.x * _moveSpeed, _playerRB.velocity.y, horizontalMovement.z * _moveSpeed);
    }

    private void Facing()
    {
        Vector2 mousePos = _playersInput.GameInput.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _lookPos = hit.point;
        }

        Vector3 lookDirection = _lookPos - transform.position;
        lookDirection.y = 0;

        transform.LookAt(transform.position + lookDirection, Vector3.up);
    }

    private void StartJump()
    {
        if (_isJumping || _isFalling) return; // Prevent double jumps

        _isJumping = true;
        _isFalling = false;
        _currentJumpTime = 0.0f;
        _playerRB.velocity = new Vector3(_playerRB.velocity.x, 0, _playerRB.velocity.z); // Reset vertical velocity
        Debug.Log("JUMPING!");
    }

    private void StartFall()
    {
        if (_isJumping)
        {
            _isFalling = true;
            _isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }

    private void CheckGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f))
        {
            if (_isJumping/* || _isFalling*/)
            {
                _isFalling = false;
                _isJumping = false;
                _playerRB.velocity = new Vector3(_playerRB.velocity.x, 0, _playerRB.velocity.z); // Reset vertical velocity
                Debug.Log("LANDED!");
            }
        }
    }
}
