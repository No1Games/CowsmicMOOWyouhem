using UnityEngine;
using MooyhemEnums;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Enemies/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private EnemyType _type;
    public EnemyType Type => _type;

    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] List<float> _hpPerLevel;
    public List<float> HPPerLevel => _hpPerLevel;

    [SerializeField] private List<float> _defencePerLevel;
    public List<float> DefencePerLevel => _defencePerLevel;

    #region Movement

    [SerializeField] private List<float> _speedPerLevel;
    public List<float> SpeedPerLevel => _speedPerLevel;

    [SerializeField] private float _angularSpeed;
    public float AngularSpeed => _angularSpeed;

    [SerializeField] private float _acceleration;
    public float Acceleration => _acceleration;

    #endregion

    
}
