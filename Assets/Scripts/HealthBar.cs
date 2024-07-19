using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   
    private Slider healthSlider;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

    public void SetMaxValue(float value)
    {
        healthSlider.maxValue = value;
    }
    public void SetCurrentValue(float value)
    {
        healthSlider.value = value;
    }
}
