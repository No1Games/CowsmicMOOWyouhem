using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsSystem : MonoBehaviour
{
    [SerializeField] private StartStatsSO _startStatsSO;

    private PlayerStats _stats;
    public PlayerStats Stats => _stats;

    private PlayerAttributes _attributes;
    public PlayerAttributes Attributes => _attributes;

    private void Awake()
    {
        _stats = new PlayerStats(_startStatsSO);

        _attributes = new PlayerAttributes();
    }

    public void UpdateStats(Dictionary<AttributesEnum, int> attributesState)
    {
        _attributes.UpdateAttributes(attributesState);

        CalculateStats();
    }

    public float GetStatValue(StatsEnum statName)
    {
        return _stats.Stats.Find(stat => stat.Name == statName).CurrentValue;
    }

    public Dictionary<StatsEnum, float> GetIncresedValues(AttributesEnum attributesEnum)
    {
        Dictionary<StatsEnum, float> result = new Dictionary<StatsEnum, float>();
        PlayerStat stat = null;
        switch (attributesEnum)
        {
            case AttributesEnum.Strike:

                result.Add(StatsEnum.Damage, GetIncresedDamageWithBonus());

                stat = _stats.Stats.Find(stat => stat.Name == StatsEnum.CritMultiplier);
                result.Add(StatsEnum.CritMultiplier, stat.CurrentValue + stat.IncreseValue);

                return result;

            case AttributesEnum.Fury:

                stat = _stats.Stats.Find(stat => stat.Name == StatsEnum.CritChance);
                result.Add(StatsEnum.CritChance, stat.CurrentValue + stat.IncreseValue);

                result.Add(StatsEnum.AttkSpeed, GetIncresedAttackSpeedWithBonus());

                return result;
            
            case AttributesEnum.Energy:

                stat = _stats.Stats.Find(stat => stat.Name == StatsEnum.HealthPoints);
                result.Add(StatsEnum.HealthPoints, stat.CurrentValue + stat.IncreseValue);

                stat = _stats.Stats.Find(stat => stat.Name == StatsEnum.JumpTime);
                result.Add(StatsEnum.JumpTime, stat.CurrentValue + stat.IncreseValue);

                return result;

            case AttributesEnum.Guard:

                stat = _stats.Stats.Find(stat => stat.Name == StatsEnum.Defence);
                result.Add(StatsEnum.Defence, stat.CurrentValue + stat.IncreseValue);

                stat = _stats.Stats.Find(stat => stat.Name == StatsEnum.HpRegen);
                result.Add(StatsEnum.HpRegen, stat.CurrentValue + stat.IncreseValue);

                return result;

            case AttributesEnum.Agility:

                stat = _stats.Stats.Find(stat => stat.Name == StatsEnum.Evasion);
                result.Add(StatsEnum.Evasion, stat.CurrentValue + stat.IncreseValue);

                stat = _stats.Stats.Find(stat => stat.Name == StatsEnum.MoveSpeed);
                result.Add(StatsEnum.MoveSpeed, stat.CurrentValue + stat.IncreseValue);

                return result;

            default: return result;
        }
        
    }

    public void CalculateStats()
    {
        CalculateStrike();
        CalculateFury();
        CalculateEnergy();
        CalculateGuard();
        CalculateAgility();
    }

    private void CalculateStrike()
    {
        PlayerStat damageBonus = _stats.Stats.Find(stat => stat.Name == StatsEnum.DamageBonus);
        damageBonus.CurrentValue = damageBonus.IncreseValue * _attributes.Attributes[AttributesEnum.Strike];

        PlayerStat critMultiplier = _stats.Stats.Find(stat => stat.Name == StatsEnum.CritMultiplier);
        critMultiplier.CurrentValue = critMultiplier.IncreseValue * _attributes.Attributes[AttributesEnum.Strike];
    }

    private float GetIncresedDamageWithBonus()
    {
        PlayerStat damageBonusStat = _stats.Stats.Find(stat => stat.Name == StatsEnum.DamageBonus);
        float damageBonus = damageBonusStat.CurrentValue + damageBonusStat.IncreseValue;
        float baseDamage = _stats.Stats.Find(stat => stat.Name == StatsEnum.Damage).CurrentValue;

        return baseDamage + (damageBonus * baseDamage / 100f);
    }

    public float GetCurrentDamageWithBonus()
    {
        PlayerStat damageBonusStat = _stats.Stats.Find(stat => stat.Name == StatsEnum.DamageBonus);
        float damageBonus = damageBonusStat.CurrentValue;
        float baseDamage = _stats.Stats.Find(stat => stat.Name == StatsEnum.Damage).CurrentValue;

        return baseDamage + (damageBonus * baseDamage / 100f);
    }

    private void CalculateFury()
    {
        PlayerStat critChance = _stats.Stats.Find(stat => stat.Name == StatsEnum.CritChance);
        critChance.CurrentValue = critChance.IncreseValue * _attributes.Attributes[AttributesEnum.Fury];

        PlayerStat attackSpeedBonus = _stats.Stats.Find(stat => stat.Name == StatsEnum.AttckSpeedBonus);
        attackSpeedBonus.CurrentValue = attackSpeedBonus.IncreseValue * _attributes.Attributes[AttributesEnum.Fury];
    }

    private float GetIncresedAttackSpeedWithBonus()
    {
        PlayerStat stat = _stats.Stats.Find(stat => stat.Name == StatsEnum.AttckSpeedBonus);
        float attackSpeedBonus = stat.CurrentValue + stat.IncreseValue;
        float attackSpeed = _stats.Stats.Find(stat => stat.Name == StatsEnum.AttkSpeed).CurrentValue;

        return attackSpeed - (attackSpeedBonus * attackSpeed / 100f);
    }
    
    public float GetCurrentAttackSpeedWithBonus()
    {
        PlayerStat stat = _stats.Stats.Find(stat => stat.Name == StatsEnum.AttckSpeedBonus);
        float attackSpeedBonus = stat.CurrentValue;
        float attackSpeed = _stats.Stats.Find(stat => stat.Name == StatsEnum.AttkSpeed).CurrentValue;

        return attackSpeed - (attackSpeedBonus * attackSpeed / 100f);
    }

    private void CalculateEnergy()
    {
        PlayerStat hp = _stats.Stats.Find(stat => stat.Name == StatsEnum.HealthPoints);
        hp.CurrentValue = hp.IncreseValue * _attributes.Attributes[AttributesEnum.Energy];

        PlayerStat jumpTime = _stats.Stats.Find(stat => stat.Name == StatsEnum.JumpTime);
        jumpTime.CurrentValue = jumpTime.IncreseValue * _attributes.Attributes[AttributesEnum.Energy];
    }

    private void CalculateGuard() 
    {
        PlayerStat defence = _stats.Stats.Find(stat => stat.Name == StatsEnum.Defence);
        defence.CurrentValue = defence.IncreseValue * _attributes.Attributes[AttributesEnum.Guard];

        PlayerStat hpRegen = _stats.Stats.Find(stat => stat.Name == StatsEnum.HpRegen);
        hpRegen.CurrentValue = hpRegen.IncreseValue * _attributes.Attributes[AttributesEnum.Guard];
    }

    private void CalculateAgility()
    {
        PlayerStat evasion = _stats.Stats.Find(stat => stat.Name == StatsEnum.Evasion);
        evasion.CurrentValue = evasion.IncreseValue * _attributes.Attributes[AttributesEnum.Agility];

        PlayerStat moveSpeed = _stats.Stats.Find(stat => stat.Name == StatsEnum.MoveSpeed);
        moveSpeed.CurrentValue = moveSpeed.IncreseValue * _attributes.Attributes[AttributesEnum.Agility];
    }
}
