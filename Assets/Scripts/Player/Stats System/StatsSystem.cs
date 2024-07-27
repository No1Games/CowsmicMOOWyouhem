using System.Collections.Generic;
using MooyhemEnums;
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

    public void UpdateStats(Dictionary<Attributes, int> attributesState)
    {
        _attributes.UpdateAttributes(attributesState);

        CalculateStats();
    }

    public float GetStatValue(Stats statName)
    {
        return _stats.Stats.Find(stat => stat.Name == statName).CurrentValue;
    }

    public Dictionary<Stats, float> GetIncresedValues(Attributes attributesEnum)
    {
        Dictionary<Stats, float> result = new Dictionary<Stats, float>();
        PlayerStat stat = null;
        switch (attributesEnum)
        {
            case MooyhemEnums.Attributes.Strike:

                result.Add(MooyhemEnums.Stats.Damage, GetIncresedDamageWithBonus());

                stat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.CritMultiplier);
                result.Add(MooyhemEnums.Stats.CritMultiplier, stat.CurrentValue + stat.IncreseValue);

                return result;

            case MooyhemEnums.Attributes.Fury:

                stat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.CritChance);
                result.Add(MooyhemEnums.Stats.CritChance, stat.CurrentValue + stat.IncreseValue);

                result.Add(MooyhemEnums.Stats.AttkSpeed, GetIncresedAttackSpeedWithBonus());

                return result;
            
            case MooyhemEnums.Attributes.Energy:

                stat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.HealthPoints);
                result.Add(MooyhemEnums.Stats.HealthPoints, stat.CurrentValue + stat.IncreseValue);

                stat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.JumpCooldown);
                result.Add(MooyhemEnums.Stats.JumpCooldown, stat.CurrentValue + stat.IncreseValue);

                return result;

            case MooyhemEnums.Attributes.Guard:

                stat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.Defence);
                result.Add(MooyhemEnums.Stats.Defence, stat.CurrentValue + stat.IncreseValue);

                stat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.HpRegen);
                result.Add(MooyhemEnums.Stats.HpRegen, stat.CurrentValue + stat.IncreseValue);

                return result;

            case MooyhemEnums.Attributes.Agility:

                stat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.Evasion);
                result.Add(MooyhemEnums.Stats.Evasion, stat.CurrentValue + stat.IncreseValue);

                stat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.MoveSpeed);
                result.Add(MooyhemEnums.Stats.MoveSpeed, stat.CurrentValue + stat.IncreseValue);

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
        PlayerStat damageBonus = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.DamageBonus);
        damageBonus.CurrentValue = damageBonus.IncreseValue * _attributes.Attributes[MooyhemEnums.Attributes.Strike];

        PlayerStat critMultiplier = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.CritMultiplier);
        critMultiplier.CurrentValue = critMultiplier.IncreseValue * _attributes.Attributes[MooyhemEnums.Attributes.Strike];
    }

    private float GetIncresedDamageWithBonus()
    {
        PlayerStat damageBonusStat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.DamageBonus);
        float damageBonus = damageBonusStat.CurrentValue + damageBonusStat.IncreseValue;
        float baseDamage = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.Damage).CurrentValue;

        return baseDamage + (damageBonus * baseDamage / 100f);
    }

    public float GetCurrentDamageWithBonus()
    {
        PlayerStat damageBonusStat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.DamageBonus);
        float damageBonus = damageBonusStat.CurrentValue;
        float baseDamage = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.Damage).CurrentValue;

        return baseDamage + (damageBonus * baseDamage / 100f);
    }

    private void CalculateFury()
    {
        PlayerStat critChance = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.CritChance);
        critChance.CurrentValue = critChance.IncreseValue * _attributes.Attributes[MooyhemEnums.Attributes.Fury];

        PlayerStat attackSpeedBonus = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.AttckSpeedBonus);
        attackSpeedBonus.CurrentValue = attackSpeedBonus.IncreseValue * _attributes.Attributes[MooyhemEnums.Attributes.Fury];
    }

    private float GetIncresedAttackSpeedWithBonus()
    {
        PlayerStat stat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.AttckSpeedBonus);
        float attackSpeedBonus = stat.CurrentValue + stat.IncreseValue;
        float attackSpeed = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.AttkSpeed).CurrentValue;

        return attackSpeed - (attackSpeedBonus * attackSpeed / 100f);
    }
    
    public float GetCurrentAttackSpeedWithBonus()
    {
        PlayerStat stat = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.AttckSpeedBonus);
        float attackSpeedBonus = stat.CurrentValue;
        float attackSpeed = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.AttkSpeed).CurrentValue;

        return attackSpeed - (attackSpeedBonus * attackSpeed / 100f);
    }

    private void CalculateEnergy()
    {
        PlayerStat hp = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.HealthPoints);
        hp.CurrentValue = hp.IncreseValue * _attributes.Attributes[MooyhemEnums.Attributes.Energy];

        PlayerStat jumpCooldown = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.JumpCooldown);
        jumpCooldown.CurrentValue = jumpCooldown.IncreseValue * _attributes.Attributes[MooyhemEnums.Attributes.Energy];
    }

    private void CalculateGuard() 
    {
        PlayerStat defence = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.Defence);
        defence.CurrentValue = defence.IncreseValue * _attributes.Attributes[MooyhemEnums.Attributes.Guard];

        PlayerStat hpRegen = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.HpRegen);
        hpRegen.CurrentValue = hpRegen.IncreseValue * _attributes.Attributes[MooyhemEnums.Attributes.Guard];
    }

    private void CalculateAgility()
    {
        PlayerStat evasion = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.Evasion);
        evasion.CurrentValue = evasion.IncreseValue * _attributes.Attributes[MooyhemEnums.Attributes.Agility];

        PlayerStat moveSpeed = _stats.Stats.Find(stat => stat.Name == MooyhemEnums.Stats.MoveSpeed);
        moveSpeed.CurrentValue = moveSpeed.IncreseValue * _attributes.Attributes[MooyhemEnums.Attributes.Agility];
    }
}
