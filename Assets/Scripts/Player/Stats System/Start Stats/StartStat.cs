using MooyhemEnums;
using UnityEngine;

[System.Serializable]
public class StartStat
{
    [SerializeField] private Stats _type;
    public Stats Type => _type;

    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private float _baseValue;
    public float BaseValue => _baseValue;

    [SerializeField] private float _increseValue;
    public float IncreseValue => _increseValue;
}
