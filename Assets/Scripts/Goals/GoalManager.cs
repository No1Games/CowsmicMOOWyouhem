using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GoalManager : MonoBehaviour
{
    [SerializeField] List<GoalsObject> firstLevelGoals = new List<GoalsObject>();
    [SerializeField] List<GoalsObject> hardGoals = new List<GoalsObject>();

    GoalsObject goalOne;
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

        //ClearGoalProgress(firstLevelGoals);
        //ClearGoalProgress(hardGoals);
        ChooseGoals(firstLevelGoals, hardGoals);
        textGoalOne.text = goalOne.textOfTask;
        sliderGoalOne.maxValue = goalOne.valueToReach;
        sliderGoalOne.value = 0;

        textGoalTwo.text = goalTwo.textOfTask;
        sliderGoalTwo.maxValue = goalTwo.valueToReach;
        sliderGoalTwo.value = 0;

        textGoalThree.text = goalThree.textOfTask;
        sliderGoalThree.maxValue = goalThree.valueToReach;
        sliderGoalThree.value = 0;
    }

    void Update()
    {
        CheckGoalProgress(goalOne, sliderGoalOne);
        CheckGoalProgress(goalTwo, sliderGoalTwo);
        CheckGoalProgress(goalThree, sliderGoalThree);


    }

    void ClearGoalProgress(List<GoalsObject> goalList)
    {
        foreach (GoalsObject goal in goalList)
        {
            goal.currentValue = 0;
        }
    }
    private void ChooseGoals(List<GoalsObject> simpleGoals, List<GoalsObject> hardGoals )
    {
        int firstGoalIndex = Random.Range(0, simpleGoals.Count);
        goalOne = simpleGoals[firstGoalIndex];

        
        int secondGoalIndex;

        do
        {
            secondGoalIndex = Random.Range(0, simpleGoals.Count);
        } while (secondGoalIndex == firstGoalIndex);

        goalTwo = simpleGoals[secondGoalIndex];

        int thirdGoalIndex = Random.Range(0, hardGoals.Count);
        goalThree = hardGoals[thirdGoalIndex];
    }

    void CheckGoalProgress(GoalsObject goal, Slider goalSlider)
    {
        if(goal.goalStatus != GoalStatus.Complete)
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
            case TypeOfGoal.UntilBasicGoalsDone:
                UntilBasicGoalsDone(goal);
                break;
            case TypeOfGoal.CollectDamage:
                CollectDamage(goal);
                break;
            case TypeOfGoal.AvoidDamageTime:
                AvoidDamageTime(goal);
                break;

        }
        }

        if (goal.currentValue >= goal.valueToReach)
        {
            goalSlider.value = goal.valueToReach;
            goal.goalStatus = GoalStatus.Complete;
            Debug.Log("Congrats, tou complete task: " + goal.textOfTask);
        }
        else
        {

            goalSlider.value = goal.currentValue;

        }
    }


    #region Simple Goals
    void DestroyAliens(GoalsObject goal) // «нищити певну к≥льк≥сть певних ворог≥в
    {
        if (controller.DeadMobs != null)
        {
            int preValue = 0;
            foreach (DeadMobInform mob in controller.DeadMobs)
            {
                if (mob.type == goal.mobType || goal.mobType == TypeOfEnemy.Any)
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

    private void AvoidDamageTime(GoalsObject goal)
    {
        if (controller.playerInforms != null && controller.playerInforms.Count >0)
        {
            float preValue = 0;
            preValue = controller.timer - controller.playerInforms[^1].timeOfAttack;
            goal.currentValue = (int)preValue;
        }
        
    }

#endregion

    #region Complex Goals

    void UntilBasicGoalsDone(GoalsObject goal)
    {
        if(goalOne.goalStatus == GoalStatus.Complete && goalTwo.goalStatus == GoalStatus.Complete && controller.playerInforms.Count == 0)
        {
            goal.currentValue = goal.valueToReach;
        }

    }

    void CollectDamage(GoalsObject goal)
    {
        goal.valueToReach = (int) controller.playersHpAtStart / 100 * 80;
        float preValue = 0;

        foreach(PlayerInform damage in controller.playerInforms)
        {
            preValue += damage.damage;

        }
        goal.currentValue = (int)preValue;

    }


    #endregion
}
