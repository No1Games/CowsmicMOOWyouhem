using System;
using UnityEngine;

[System.Serializable]
public class PlayerStat
{
    public PlayerStat(StartStat startStat)
    {
        _name = startStat.Type;
        _nameStr = startStat.Name;
        _baseValue = startStat.BaseValue;
        _increseValue = startStat.IncreseValue;
        _currentValue = _baseValue;
    }

    [SerializeField] private StatsEnum _name;
    public StatsEnum Name => _name;
    [SerializeField] private string _nameStr;
    public string NameStr => _nameStr;
    [SerializeField] private float _baseValue;
    public float BaseValue => _baseValue;
    [SerializeField] private float _increseValue;
    public float IncreseValue => _increseValue;
    [SerializeField] private float _currentValue;
    public float CurrentValue
    {
        get { return _currentValue; }
        set { _currentValue = value; }
    }
}
