using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfGoal { DestroyAliensNumber, DestroyAliensTime, OneAfterAnother, OneWeaponOnly, OneTypeKill, UntilBasicGoalsDone, CollectDamage, AvoidDamageTime }
public enum TypeOfDamage { MainWeapon, AutoWeapon, Any }
public enum TypeOfEnemy { Basic, Jumper, BasicShooter, Any}

public enum GoalStatus { Incomplete, Complete, Failed }

[CreateAssetMenu(fileName ="New Goal", menuName ="Goal")]
public class GoalsObject : ScriptableObject
{

   public string textOfTask;
   public int valueToReach;
   public int problemValue;
   public int currentValue;
   public TypeOfEnemy mobType;
   
   public float timeToBeat;
    public GoalStatus goalStatus;
    

    public TypeOfGoal typeOfGoal;
    public TypeOfDamage firstDamageType;
    public TypeOfDamage secondDamageType;
   
      
}
