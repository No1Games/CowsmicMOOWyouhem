using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [Space]
    [SerializeField] protected EnemyData _enemyData;
    public EnemyData EnemyData => _enemyData;
    protected int _difficulty;

    protected NavMeshAgent _agent;

    protected GameObject _target;

    protected float _maxHP;
    public float MaxHP => _maxHP;
    protected float _currentHP;
    public float CurrentHP => _currentHP;
    protected float _defence;
    public float Defence => _defence;

    public void InitEnemy(int level, GameObject target)
    {
        _maxHP = _enemyData.HPPerLevel[level];
        _currentHP = _maxHP;

        _defence = _enemyData.DefencePerLevel[level];

        _agent.angularSpeed = _enemyData.AngularSpeed;
        _agent.acceleration = _enemyData.Acceleration;

        _agent.speed = _enemyData.SpeedPerLevel[level];

        _target = target;

        _difficulty = level;
    }

    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        GameObject target = GameObject.Find("Player"); // !!! GET FROM SPAWNER OR LEVEL CONTROLLER WHEN IT'S READY !!!

        InitEnemy(0, target); // !!! REMOVE WHEN SPAWNER READY !!!
    }

    #region Damage & Death

    public void TakeDamage(DamageData damageData)
    {
        float amountAfterDefence;

        // CHANGE IF ANY DAMAGE TYPE

        if (damageData.Type != MooyhemEnums.DamageType.Effect)
        {
            amountAfterDefence = damageData.Amount - (_defence / 100 * damageData.Amount);
        }
        else
        {
            amountAfterDefence = damageData.Amount;
        }

        if(amountAfterDefence <= 0)
        {
            return;
        }

        float newHealth = _currentHP - amountAfterDefence;

        if (newHealth <= 0)
        {
            _currentHP = 0;
            Death(damageData);
            return;
        }

        _currentHP = newHealth;
    }

    public void Death(DamageData damageData)
    {
        Debug.Log($"{_enemyData.Name} diead from {damageData.Type}");
    }

    #endregion
}
