using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    private LevelUpMenu _levelUpMenu;
    private PlayerAttributes _attributes;

    [SerializeField] private StartStatsSO _startStatsSO;

    [SerializeField]
    private int _points;
    public int LeftPoints => _points;

    private void Start()
    {
        _attributes = new PlayerAttributes();

        _levelUpMenu = FindObjectOfType<LevelUpMenu>();
        _levelUpMenu.Init(this, _attributes);
        _levelUpMenu.gameObject.SetActive(false);

        _levelUpMenu.UpdateAttributes += OnAttributesUpdate;
        
    }

    private void OnAttributesUpdate(Dictionary<AttributesEnum, int> attributesState)
    {
        _points = 0;

        _attributes.UpdateAttributes(attributesState);
    }
}
