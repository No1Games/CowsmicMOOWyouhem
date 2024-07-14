using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _availablePointsText;

    private int _availablePoints;

    [SerializeField] private AttributeView _attributeViewPrefab;
    [SerializeField] private GameObject _attributesParent;

    [SerializeField] private StatView _statViewPrefab;
    [SerializeField] private GameObject _statsParent;

    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _resetButton;

    private AttributesManager _attributesManager;

    private List<AttributeView> _attributeViews;

    public event Action<Dictionary<AttributesEnum, int>> UpdateAttributes;

    public void Init(AttributesManager manager, PlayerAttributes attributesModel)
    {
        _attributesManager = manager;

        _attributeViews = new List<AttributeView>();

        _availablePointsText.text = _attributesManager.LeftPoints.ToString();
        _availablePoints = _attributesManager.LeftPoints;

        InitAttributes(attributesModel);

        //InitStats();

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

    public void AttributeIncresed()
    {
        _availablePoints--;

        _availablePointsText.text = _availablePoints.ToString();
        
        if(_availablePoints == 0)
        {
            DisableAll();
            _confirmButton.interactable = true;
        }
    }

    public void AttributeDecresed()
    {
        _availablePoints++;

        _availablePointsText.text = _availablePoints.ToString();

        if (_availablePoints > 0)
        {
            EnableAllButIncresed();
            _confirmButton.interactable = false;
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
            attributesState[attribute.Attribute] = attribute.Value;
        }

        return attributesState;
    }
}
