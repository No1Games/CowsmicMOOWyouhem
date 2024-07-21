using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    [Space]
    [SerializeField] private TextMeshProUGUI _availablePointsText;

    private int _availablePoints;

    [Header("Attribute Panel")]
    [SerializeField] private AttributeView _attributeViewPrefab;
    [SerializeField] private GameObject _attributesParent;

    [Header("Stats Panel")]
    [SerializeField] private StatView _statViewPrefab;
    [SerializeField] private GameObject _statsParent;

    [Header("Buttons")]
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _resetButton;

    [Space]
    [SerializeField] private List<StatsEnum> _skipStats;

    private AttributesManager _attributesManager;

    private List<AttributeView> _attributeViews;
    private List<StatView> _statViews;

    public event Action<Dictionary<AttributesEnum, int>> UpdateAttributes;

    public void Init(AttributesManager manager, PlayerAttributes attributesModel, PlayerStats statsModel)
    {
        _attributesManager = manager;

        _attributeViews = new List<AttributeView>();
        _statViews = new List<StatView>();

        _availablePointsText.text = _attributesManager.LeftPoints.ToString();
        _availablePoints = _attributesManager.LeftPoints;

        InitAttributes(attributesModel);

        InitStats(statsModel);

        if (_attributesManager.LeftPoints > 0)
        {
            EnableAll();
        }

        _confirmButton.interactable = false;
        _confirmButton.onClick.AddListener(ConfirmClick);

        _resetButton.onClick.AddListener(ResetClick);
    }

    private void OnDestroy()
    {
        foreach (var view in _attributeViews)
        {
            view.AttributeIncresed -= AttributeIncresed;
            view.AttributeDecresed -= AttributeDecresed;
        }
    }

    private void InitAttributes(PlayerAttributes attributesModel)
    {
        foreach (var attribute in attributesModel.Attributes)
        {
            AttributeView view = Instantiate(_attributeViewPrefab);
            view.Init(attribute.Key, attribute.Value);
            view.transform.SetParent(_attributesParent.transform, false);
            view.AttributeIncresed += AttributeIncresed;
            view.AttributeDecresed += AttributeDecresed;
            _attributeViews.Add(view);
        }
    }

    private void InitStats(PlayerStats statsModel)
    {
        foreach(var stat in statsModel.Stats)
        {
            if (_skipStats.Contains(stat.Name)) continue;

            StatView view = Instantiate(_statViewPrefab);
            view.InitView(stat, stat.NameStr, stat.BaseValue);
            view.transform.SetParent(_statsParent.transform, false);
            _statViews.Add(view);
        }
    }

    public void AttributeIncresed(AttributesEnum incresedAttr)
    {
        _availablePoints--;

        _availablePointsText.text = _availablePoints.ToString();

        IncreseStats(_attributesManager.GetIncresedValues(incresedAttr));
        
        if(_availablePoints == 0)
        {
            DisableAll();
            _confirmButton.interactable = true;
        }
    }

    private void IncreseStats(Dictionary<StatsEnum, float> values)
    {
        foreach (var pair in values)
        {
            StatView view = _statViews.Find(statView => statView.Stat.Name == pair.Key);
            view.ChangeNewValue(pair.Value);
        }
    }

    public void AttributeDecresed(AttributesEnum decresedAttr)
    {
        _availablePoints++;

        _availablePointsText.text = _availablePoints.ToString();

        DecreseStats(_attributesManager.GetAttributeStats(decresedAttr));

        if (_availablePoints > 0)
        {
            EnableAllButIncresed();
            _confirmButton.interactable = false;
        }
    }

    private void DecreseStats(List<StatsEnum> stats)
    {
        foreach (var stat in stats)
        {
            StatView view = _statViews.Find(statView => statView.Stat.Name == stat);
            view.ResetNewValue();
        }
    }

    private void EnableAll()
    {
        foreach(var attribute in _attributeViews)
        {
            attribute.SetIncrese(true);
        }
    }

    private void EnableAllButIncresed()
    {
        foreach(var attribute in _attributeViews)
        {
            if (!attribute.Incresed)
            {
                attribute.SetIncrese(true);
            }
        }
    }

    private void DisableAll()
    {
        foreach (var attribute in _attributeViews)
        {
            attribute.SetIncrese(false);
        }
    }

    private void ConfirmClick()
    {
        Dictionary<AttributesEnum, int> attributesState = GetCurrentAttributesState();

        UpdateAttributes?.Invoke(attributesState);

        gameObject.SetActive(false);
    }

    private void ResetClick()
    {
        _availablePoints = _attributesManager.LeftPoints;
        _availablePointsText.text = _availablePoints.ToString();
        _confirmButton.interactable = false;

        foreach(var view in _attributeViews)
        {
            view.ResetView();
        }
    }

    private Dictionary<AttributesEnum, int> GetCurrentAttributesState()
    {
        Dictionary<AttributesEnum, int> attributesState = new Dictionary<AttributesEnum, int>();

        foreach (var attribute in _attributeViews)
        {
            attributesState[attribute.Attribute] = attribute.NewValue;
        }

        return attributesState;
    }
}
