using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    List<ItemData> _currentItems = new List<ItemData>();
    List<Collectable> _collectedItems = new List<Collectable>();
    public bool _isBusy { get; set; }
    public List<ItemData> GetCurrentItems()
    {
        return _currentItems;
    }
    public List<ItemData> GetOtherItems()
    {
        return _currentItems.Where(x=> x.IsOther).ToList();
    }
    public List<ItemData> GetDesiredItemTypeCurrentItems(ItemType itemType)
    {
        return _currentItems.Where(x=> x.ItemType == itemType).ToList();
    }
    public List<Collectable> GetAllCollectedItems()
    {
        return _collectedItems;
    }
    public void AddCollectedItem(Collectable _collectedItem)
    {
        _collectedItems.Add(_collectedItem);
        AddItemInInventory(ItemManager.instance.GetItemWithID(_collectedItem.ItemDataID));
    }
    public void RemoveCollectedItemInInventory(Collectable _collected)
    {
        _collectedItems.Remove(_collected);
    }
    public void AddItemInInventory(ItemData itemData)
    {
        _currentItems.Add(itemData);
    }
    public void AddRandomItemInInventory()
    {
        AddItemInInventory(ItemManager.instance.GetRandomItem());
    }
    public void RemoveItemInInventory(ItemData itemData)
    {
        _currentItems.Remove(itemData);
    }  
}
