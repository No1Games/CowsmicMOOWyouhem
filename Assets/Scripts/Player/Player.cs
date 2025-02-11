using UnityEngine;
using MooyhemEnums;
using System.Collections;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody _playerRB;

    private StatsSystem _statsSystem;

    private PlayersInput _playersInput;

    private float _currentHP;
    private float _moveSpeed;

    #region Jump Values
    
    private float _jumpForce;
    private float _jumpCooldown;
    private bool _isJumpCooldown;
    private bool _isJumping = false;

    #endregion

    private Vector3 _lookPos;

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

        _currentHP = _statsSystem.GetStatValue(Stats.HealthPoints);
        _moveSpeed = _statsSystem.GetStatValue(Stats.MoveSpeed);
        _jumpCooldown = _statsSystem.GetStatValue(Stats.JumpCooldown);
        _jumpForce = _statsSystem.GetStatValue(Stats.JumpForce);
    }

    void Update()
    {        
        Move();
        Facing();
    }

    private void Jump()
    {
        if(_isJumping || _isJumpCooldown) return;

        _isJumping = true;
        _isJumpCooldown = true;
        _playerRB.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(_jumpCooldown);

        _isJumpCooldown = false;
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
            _lookPos = hit.point;
        }

        Vector3 lookDirection = _lookPos - transform.position;
        lookDirection.y = 0;

        transform.LookAt(transform.position + lookDirection, Vector3.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _isJumping)
        {
            _isJumping = false;

            StartCoroutine(JumpCooldown());
        }
    }

    public void TakeDamage(DamageData damageData)
    {
        Debug.Log($"Player was attacked by {damageData.Attacker.name}");

        float amountAfterDefence = damageData.Amount - _statsSystem.GetStatValue(Stats.Defence);

        if(amountAfterDefence <= 0)
        {
            Debug.Log($"All damage have been reduced by defend value: {damageData.Amount} - {_statsSystem.GetStatValue(Stats.Defence)}");
            return;
        }

        float newHealth = _currentHP - amountAfterDefence;

        if(newHealth <= 0)
        {
            _currentHP = 0;
            Death(damageData);
            return;
        }

        _currentHP = newHealth;

        // !!! HEALTHBAR !!!
    }

    public void Death(DamageData damageData)
    {
        Debug.Log($"Player was killed by {damageData.Attacker}");
    }
}
