using System.Collections.Generic;
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

    private void OnAttributesUpdate(Dictionary<AttributesEnum, int> attributesState)
    {
        _points = 0;

        _statsSystem.UpdateStats(attributesState);
    }

    public Dictionary<StatsEnum, float> GetIncresedValues(AttributesEnum attributesEnum)
    {
        return _statsSystem.GetIncresedValues(attributesEnum);
    }

    public List<StatsEnum> GetAttributeStats(AttributesEnum attribute)
    {
        switch(attribute) 
        {
            case AttributesEnum.Strike:
                return new List<StatsEnum> { StatsEnum.Damage, StatsEnum.CritMultiplier };
            case AttributesEnum.Fury:
                return new List<StatsEnum> { StatsEnum.CritChance, StatsEnum.AttkSpeed };
            case AttributesEnum.Energy:
                return new List<StatsEnum> { StatsEnum.HealthPoints, StatsEnum.JumpTime };
            case AttributesEnum.Guard:
                return new List<StatsEnum> { StatsEnum.Defence, StatsEnum.HpRegen };
            case AttributesEnum.Agility:
                return new List<StatsEnum> { StatsEnum.Evasion, StatsEnum.MoveSpeed };
            default: return new List<StatsEnum>();
        }
    }
}
