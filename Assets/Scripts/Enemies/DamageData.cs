using MooyhemEnums;
using UnityEngine;

public class DamageData
{
    private float _amount;
    public float Amount => _amount;
    private DamageType _type;
    public DamageType Type => _type;
    private GameObject _attacker;
    public GameObject Attacker => _attacker;

    public DamageData(float amount, DamageType type, GameObject attacker)
    {
        _amount = amount;
        _type = type;
        _attacker = attacker;
    }
}
