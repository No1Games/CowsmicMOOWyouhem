using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleTM;
    [SerializeField] private TextMeshProUGUI _valueTM;

    private string _title;
    private float _value;
    private float _newValue;

    public void InitView(string statName, float statValue)
    {
        _title = statName;
        _value = statValue;

        _titleTM.text = _title;
        _valueTM.text = _value.ToString();
    }

}
