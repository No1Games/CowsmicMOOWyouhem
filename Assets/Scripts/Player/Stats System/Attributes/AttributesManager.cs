using System.Collections.Generic;
using MooyhemEnums;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    private LevelUpMenu _levelUpMenu;
    private PlayerAttributes _attributes;

    private StatsSystem _statsSystem;

    [SerializeField] private StartStatsSO _startStatsSO;

    [SerializeField]
    private int _points;
    public int LeftPoints => _points;

    private void Start()
    {
        _statsSystem = FindObjectOfType<StatsSystem>();

        _levelUpMenu = FindObjectOfType<LevelUpMenu>();
        _levelUpMenu.Init(this, _statsSystem.Attributes, _statsSystem.Stats);
        _levelUpMenu.gameObject.SetActive(false);

        _levelUpMenu.UpdateAttributes += OnAttributesUpdate;
    }

    private void OnAttributesUpdate(Dictionary<Attributes, int> attributesState)
    {
        _points = 0;

        _statsSystem.UpdateStats(attributesState);
    }

    public Dictionary<Stats, float> GetIncresedValues(Attributes attributesEnum)
    {
        return _statsSystem.GetIncresedValues(attributesEnum);
    }

    public List<Stats> GetAttributeStats(Attributes attribute)
    {
        switch(attribute) 
        {
            case Attributes.Strike:
                return new List<Stats> { Stats.Damage, Stats.CritMultiplier };
            case Attributes.Fury:
                return new List<Stats> { Stats.CritChance, Stats.AttkSpeed };
            case Attributes.Energy:
                return new List<Stats> { Stats.HealthPoints, Stats.JumpCooldown };
            case Attributes.Guard:
                return new List<Stats> { Stats.Defence, Stats.HpRegen };
            case Attributes.Agility:
                return new List<Stats> { Stats.Evasion, Stats.MoveSpeed };
            default: return new List<Stats>();
        }
    }
}
