using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleTM;
    [SerializeField] private TextMeshProUGUI _valueTM;

    private PlayerStat _stat;
    public PlayerStat Stat => _stat;

    private string _title;
    private float _value;
    private float _newValue;

    public void InitView(PlayerStat stat, string statName, float statValue)
    {
        _stat = stat;

        _title = statName;
        _value = statValue;

        _titleTM.text = $"{_title}:";
        _valueTM.text = _value.ToString();
    }

    public void ChangeNewValue(float newValue)
    {
        _valueTM.text = $"{_value} -> {newValue}";
        _newValue = newValue;
    }

    public void ResetNewValue()
    {
        _valueTM.text = _value.ToString();
    }
}
