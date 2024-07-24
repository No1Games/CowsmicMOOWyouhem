using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeadMobInform
{
   public TypeOfEnemy type;
   public List<TypeOfDamage> damage;
   public float livingTime;

    public DeadMobInform(TypeOfEnemy type, List<TypeOfDamage> damage, float livingTime)
    {
        this.type = type;
        this.damage = damage;
        this.livingTime = livingTime;
    }

}
