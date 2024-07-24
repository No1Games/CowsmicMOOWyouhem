using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
    [SerializeField] GoalsObject goalOne;
    [SerializeField] TextMeshProUGUI textGoalOne;
    [SerializeField] Slider sliderGoalOne;

    GoalsObject goalTwo;
    [SerializeField] TextMeshProUGUI textGoalTwo;
    [SerializeField] Slider sliderGoalTwo;

    GoalsObject goalThree;
    [SerializeField] TextMeshProUGUI textGoalThree;
    [SerializeField] Slider sliderGoalThree;

    [SerializeField] SceneControllerScript controller;

    private void Awake()
    {
        textGoalOne.text = goalOne.textOfTask;
        sliderGoalOne.maxValue = goalOne.valueToReach;
        sliderGoalOne.value = 0;

    }

    void Update()
    {
        CheckGoalProgress(goalOne);
        sliderGoalOne.value = goalOne.currentValue;
        
        //CheckGoalProgress(goalTwo);
        //CheckGoalProgress(goalThree);


    }

    void CheckGoalProgress(GoalsObject goal)
    {
        switch (goal.typeOfGoal)
        {
            case TypeOfGoal.DestroyAliensNumber:
                DestroyAliens(goal);
                break;
            case TypeOfGoal.DestroyAliensTime:
                
                DestroyAliensTime(goal);
                break;
            case TypeOfGoal.OneAfterAnother:
                OneAfterAnother(goal);
                break;
            case TypeOfGoal.OneWeaponOnly:
                OneWeaponOnly(goal);
                break;
            case TypeOfGoal.OneTypeKill:
                OneTypeKill(goal);
                break;

        }
    }

    void DestroyAliens(GoalsObject goal) // «нищити певну к≥льк≥сть певних ворог≥в
    {
        if (controller.DeadMobs != null)
        {
            int preValue = 0;
            foreach (DeadMobInform mob in controller.DeadMobs)
            {
                if (mob.type == goal.mobType)
                {
                    preValue++;
                }
            }
            goal.currentValue = preValue;
        }
    }

    private void DestroyAliensTime(GoalsObject goal) // «нищити ворог≥в за певний час
    {
        

        if (controller.DeadMobs != null && controller.DeadMobs.Count > 0)
        {
            
            DeadMobInform mobToTrack = null;
            
            
            for(int i = 0; i<controller.DeadMobs.Count; i++)
            {
                bool haveTime = controller.timer - controller.DeadMobs[i].livingTime < goal.timeToBeat;
                if (haveTime)
                {
                mobToTrack = controller.DeadMobs[i];
                    break;

                }
                
                
            }

            if(mobToTrack != null)
            {
                goal.currentValue = controller.mobsKilled - controller.DeadMobs.IndexOf(mobToTrack) + 1;
            }
            Debug.Log("goal " + goal.currentValue);
            //else
            //{
            //    goal.currentValue = 0;
            //}
            
            //bool haveTime = controller.timer - mobToTrack.livingTime < goal.timeToBeat;
            
            //if (haveTime)
            //{
            //    prevValue = controller.mobsKilled - controller.DeadMobs.IndexOf(mobToTrack) + 1;
            //}
            //else
            //{
                
            //    bool mobIsFound = false;
            //    for (int i = 0; i < controller.DeadMobs.Count; i++)
            //    {
            //        if (controller.timer - controller.DeadMobs[i].livingTime < 15 && !mobIsFound)
            //        {
            //            mobToTrack = controller.DeadMobs[i];
            //            mobIsFound = true;
            //        }
            //    }
            //}
        }
    }

    private void OneAfterAnother(GoalsObject goal) // ¬бити ворога damage1 п≥сл€ атаки damage2
    {
        if (controller.DeadMobs != null)
        {
            int prevValue = 0;
            foreach (DeadMobInform mob in controller.DeadMobs)
            {
                int finalMob = mob.damage.Count - 1;
                bool attackedBySideWeapon = false;

                foreach(TypeOfDamage damage in mob.damage)
                {
                    if(damage == goal.secondDamageType)
                    {
                        attackedBySideWeapon = true;
                    }
                }

                if (mob.damage[finalMob] == goal.firstDamageType && attackedBySideWeapon)
                {
                    prevValue++;
                }
            }
            goal.currentValue++;


        }
    }

    private void OneWeaponOnly(GoalsObject goal) // ¬бити ворога т≥льки одн≥Їю зброЇю
    {
        if (controller.DeadMobs != null)
        {
            int prevValue = 0;
            
            foreach (DeadMobInform mob in controller.DeadMobs)
            {
                if (mob.damage.Contains(goal.firstDamageType))
                {
                    prevValue++;
                }
            }
            goal.currentValue = prevValue;
        }
    }

    private void OneTypeKill(GoalsObject goal)
    {
        int prevValue = 0;


        for (int i = 0; i <  controller.DeadMobs.Count; i++)
        {
            if (controller.DeadMobs[i].type == goal.mobType)
            {
                int neededMobs = 0;
                int problemMobs = 0;
                for (int j = i; j < controller.DeadMobs.Count; j++)
                {
                    if(controller.DeadMobs[j].type == goal.mobType)
                    {
                        neededMobs++;
                    }
                    else if (controller.DeadMobs[j].type != goal.mobType)
                    {
                        problemMobs++;
                    }

                    if (problemMobs >= goal.problemValue)
                    {
                        
                        neededMobs = 0;
                        problemMobs = 0;
                        break;
                    }

                }
                if( neededMobs > prevValue ) 
                {
                prevValue = neededMobs;
                }
            }
        }
        goal.currentValue = prevValue;

    } // ¬бити ворога т≥льки одного типу


}
