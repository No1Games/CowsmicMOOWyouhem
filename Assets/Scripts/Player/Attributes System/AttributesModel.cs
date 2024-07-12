using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesModel
{
    private List<AttributeModel> _attributes = new List<AttributeModel>();
    public List<AttributeModel> Attributes => _attributes;

    public AttributesModel(List<AttributesModel> initValues = null)
    {
        if (initValues == null)
        {
            foreach (AttributesEnum attribute in Enum.GetValues(typeof(AttributesEnum)))
            {
                _attributes.Add(new AttributeModel { Attribute = attribute, Value = 0 });
            }
        }
    }

    public void IncreseAttribute(AttributesEnum attribute)
    {
        _attributes.Find(attr => attr.Attribute == attribute).Value++;
    }
}
