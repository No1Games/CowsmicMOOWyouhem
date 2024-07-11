using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeModel
{
    private AttributesEnum _attribute;
    public AttributesEnum Attribute 
    { 
        get { return _attribute; } 
        set { _attribute = value; } 
    }
    
    private int _value;
    public int Value 
    { 
        get { return _value; } 
        set { _value = value; } 
    }
}
