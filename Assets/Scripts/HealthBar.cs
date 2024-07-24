using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   
    private Slider healthSlider;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
        //TestMethodMe();
    }

    public void SetMaxValue(float value)
    {
        healthSlider.maxValue = value;
    }
    public void SetCurrentValue(float value)
    {
        healthSlider.value = value;
    }

    //public void TestMethodMe()
    //{
    //    List<int> list = new List<int>() {2, 1, 1,2,1,1,1,2,2,1,1,1,2,1,1,1,2,1};
    //    int streak = 0;
        
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        if (list[i] == 1)
    //        {
    //            int currentStreak = 0;
    //            int currentBadStreak = 0;

    //            for (int j = i; j < list.Count; j++)
    //            {
    //                if (list[j] == 1)
    //                {
    //                    currentStreak++;
    //                }
    //                else if (list[j] == 2)
    //                {
    //                    currentBadStreak++;
    //                }

    //                if (currentBadStreak >= 2)
    //                {
    //                    currentStreak = 0;
    //                    currentBadStreak = 0;

    //                    break;
    //                }
    //            }
    //            if(currentStreak > streak)
    //            {
    //                streak = currentStreak;
    //            }
    //        }
    //    }
    //    Debug.Log(streak);

    //}
}
