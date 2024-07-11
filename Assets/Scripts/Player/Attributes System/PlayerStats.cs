using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    #region Stats & Accessors

    [Header("Health")]
    [SerializeField] private float _healthPoints;
    public float HealthPoints 
    { 
        get { return _healthPoints; } 
        set { _healthPoints = value; } 
    }
    [SerializeField] private float _hpRegen;
    public float HPRegen
    {
        get { return _hpRegen; }
        set { _hpRegen = value; }
    }

    [Header("Defence")]
    [SerializeField] private float _defence;
    public float Defence
    {
        get { return _defence; }
        set { _defence = value; }
    }
    [SerializeField] private float _evasion;
    public float Evasion
    {
        get { return _evasion; }
        set { _evasion = value; }
    }

    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }
    [SerializeField] private float _jumpTime;
    public float JumpTime
    {
        get { return _jumpTime; }
        set { _jumpTime = value; }
    }

    [Header("Attack")]
    [SerializeField] private float _baseDamage;
    public float BaseDamage
    {
        get { return _baseDamage; }
        set { _baseDamage = value; }
    }
    [SerializeField] private float _damageBonus;
    public float DamageBonus
    {
        get { return _damageBonus; }
        set { _damageBonus = value; }
    }
    [SerializeField] private float _attackSpeed;
    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }
    [SerializeField] private float _critChance;
    public float CritChance
    {
        get { return _critChance; }
        set { _critChance = value; }
    }
    [SerializeField] private float _critMultiplier;
    public float CritMultiplier
    {
        get { return _critMultiplier; }
        set { _critMultiplier = value; }
    }

    #endregion
}
