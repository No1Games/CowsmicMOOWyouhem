using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Start Stats", menuName ="Player/Starting Stats")]
public class StartStatsSO : ScriptableObject
{
    [SerializeField] private PlayerStats playerStats;
    public PlayerStats PlayerStats => playerStats;
}
