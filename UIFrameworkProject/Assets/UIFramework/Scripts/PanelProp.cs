using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelType
{
    ItemMessage,
    Knapsack,
    MainMenu,
    Shop,
    Skill,
    System,
    Task
}
[System.Serializable]
public class PanelProp : ISerializationCallbackReceiver
{
    [System.NonSerialized]
    public PanelType panelType;
    public string PanelTypeString;
    //{
    //    get
    //    {
    //        return panelType.ToString();
    //    }
    //    set
    //    {
    //        PanelType type = (PanelType)System.Enum.Parse(typeof(PanelType), value);
    //        panelType = type;
    //    }
    //}
    public string panelPath;

    public void OnAfterDeserialize()
    {
        PanelType type = (PanelType)System.Enum.Parse(typeof(PanelType), PanelTypeString);
        panelType = type;
    }

    public void OnBeforeSerialize()
    {
    }
}
