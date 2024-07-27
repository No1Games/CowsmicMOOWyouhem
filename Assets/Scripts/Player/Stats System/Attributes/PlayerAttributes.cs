using System;
using System.Collections.Generic;
using MooyhemEnums;

public class PlayerAttributes
{
    private Dictionary<Attributes, int> _attributes;
    public Dictionary<Attributes, int> Attributes => _attributes;

    public PlayerAttributes(Dictionary<Attributes, int> startAttributes = null)
    {
        if (startAttributes == null)
        {
            _attributes = new Dictionary<Attributes, int>();
            foreach (Attributes attr in Enum.GetValues(typeof(Attributes)))
            {
                _attributes.Add(attr, 0);
            }
        }
        else
        {
            _attributes = startAttributes;
        }
    }

    public void UpdateAttributes(Dictionary<Attributes, int> attributes)
    {
        _attributes = attributes;
    }
}
