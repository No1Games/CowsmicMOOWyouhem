using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StartStat
{
    [SerializeField] private StatsEnum _type;
    public StatsEnum Type => _type;

    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private float _baseValue;
    public float BaseValue => _baseValue;

    [SerializeField] private float _increseValue;
    public float IncreseValue => _increseValue;
}
