using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Start Stats", menuName ="Player/Starting Stats")]
public class StartStatsSO : ScriptableObject
{
    [SerializeField] private List<StartStat> _stats;
    public List<StartStat> Stats => _stats;
}
