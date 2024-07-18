using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeView : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _value;

    private AttributesEnum _attribute;
    public AttributesEnum Attribute => _attribute;
    private int _attrValue;
    public int Value => _attrValue;
    private int _newValue;
    public int NewValue => _newValue;

    [Header("Buttons")]
    [SerializeField] private Button _decreseButton;
    [SerializeField] private Button _increseButton;

    public event Action<AttributesEnum> AttributeIncresed;
    public event Action<AttributesEnum> AttributeDecresed;

    private bool _incresed = false;
    public bool Incresed => _incresed;

    public void Init(AttributesEnum attribute, int value)
    {
        _title.text = attribute.ToString();
        _value.text = value.ToString();

        _attribute = attribute;
        _attrValue = value;

        _decreseButton.interactable = false;
        _increseButton.interactable = false;

        _decreseButton.onClick.AddListener(DecreseClick);
        _increseButton.onClick.AddListener(IncreseClick);
    }

    public void IncreseClick()
    {
        _newValue = _attrValue + 1;
        _value.text = _newValue.ToString();

        SetIncrese(false);
        SetDecrese(true);

        _incresed = true;

        AttributeIncresed?.Invoke(_attribute);
    }

    public void DecreseClick()
    {
        ResetView();

        AttributeDecresed?.Invoke(_attribute);
    }

    public void ResetView()
    {
        _value.text = _attrValue.ToString();

        SetIncrese(true);
        SetDecrese(false);

        _incresed = false;
    }

    public void SetIncrese(bool interactable)
    {
        _increseButton.interactable = interactable;
    }
    
    public void SetDecrese(bool interactable)
    {
        _decreseButton.interactable = interactable;
    }
}
