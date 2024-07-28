using System.Collections;
using UnityEngine;
using MooyhemEnums;
using System.Collections.Generic;

public class BasicAlien : EnemyBase
{
    private bool _isAttack;
    private CapsuleCollider _attackCollider;
    private Coroutine _attackCoroutine;

    [Header("Base Attack Values")]
    [SerializeField] private List<float> _damagePerLevel;
    [SerializeField] private List<float> _ratePerLevel;
    [SerializeField] private List<float> _rangePerLevel;

    protected override void Start()
    {
        base.Start();

        _attackCollider = GetComponent<CapsuleCollider>();
        _attackCollider.radius = _rangePerLevel[_difficulty];
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        _agent.SetDestination(_target.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(_target))
        {
            IDamageable target = collision.gameObject.GetComponent<IDamageable>();
            
            if (!_isAttack)
            {
                _isAttack = true;
                StartCoroutine(MeleAttack(target));
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.Equals(_target))
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _isAttack = false;
            }
        }
    }

    IEnumerator MeleAttack(IDamageable target)
    {
        while (true)
        {
            DamageData data = new DamageData(_damagePerLevel[_difficulty], DamageType.Enemy, gameObject);
            target.TakeDamage(data);
            yield return new WaitForSeconds(_ratePerLevel[_difficulty]);
        }
    }
}
