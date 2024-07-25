using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInform 
{
    public TypeOfEnemy attacker;
    public float timeOfAttack;
    public float damage;

    public PlayerInform(TypeOfEnemy attacker, float timeOfAttack, float damage)
    {
        this. attacker = attacker;
        this.timeOfAttack = timeOfAttack;
        this.damage = damage;
    }
}
