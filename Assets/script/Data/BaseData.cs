using System;
using UnityEngine;

[Serializable]
public class BaseData
{
    public string label;
    [TextArea] public string captions;

    public virtual void DisplayName()
    {
        Debug.Log("Base : " + label);
    }

    public BaseData(string label, string captions)
    {
        this.label = label;
        this.captions = captions;
    }
}
