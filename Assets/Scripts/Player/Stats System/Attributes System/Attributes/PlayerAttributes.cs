using System;
using System.Collections.Generic;

public class PlayerAttributes
{
    private Dictionary<AttributesEnum, int> _attributes;
    public Dictionary<AttributesEnum, int> Attributes => _attributes;

    public PlayerAttributes(Dictionary<AttributesEnum, int> startAttributes = null)
    {
        if (startAttributes == null)
        {
            _attributes = new Dictionary<AttributesEnum, int>();
            foreach (AttributesEnum attr in Enum.GetValues(typeof(AttributesEnum)))
            {
                _attributes.Add(attr, 0);
            }
        }
        else
        {
            _attributes = startAttributes;
        }
    }

    public void UpdateAttributes(Dictionary<AttributesEnum, int> attributes)
    {
        _attributes = attributes;
    }
}
