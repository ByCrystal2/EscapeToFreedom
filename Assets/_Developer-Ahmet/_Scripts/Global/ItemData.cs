using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int ID;
    public string Name;
    public string Description;
    public ItemType ItemType;
    public bool IsOther;
    public ItemData(int id, string name, string description, ItemType itemType, bool isOther = false)
    {
        ID = id;
        Name = name;
        Description = description;
        ItemType = itemType;
        IsOther = isOther;
    }
}
