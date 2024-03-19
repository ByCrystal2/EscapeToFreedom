using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotItem : MonoBehaviour
{
    ItemType type;
    private ItemData myData;
    [SerializeField] Sprite mySprte;
    public void SetMyData(ItemData data)
    {
        myData = data;
    }
    public ItemData GetMyData()
    {
        return myData;
    }
    public ItemType GetItemType()
    {
        return myData.ItemType;
    }
    public Sprite GetMySprite()
    {
        return mySprte;
    }
    public void SetItemType(ItemType type)
    {
        this.type = type;
    }

    public void ItemClick(SlotContentType _currentContentType)
    {
        if (_currentContentType == SlotContentType.Locker)
        {// Eger SlotContentType Locker ise yasanacak olay.
            LockerPanelController.instance.RemoveItemInCurrentLocker(myData);
            if (!(LockerPanelController.instance._inventoryPanelController._inventoryToggles[(int)myData.ItemType].isOn))
            {
                LockerPanelController.instance._inventoryPanelController._inventoryToggles[(int)myData.ItemType].isOn = true;
            }
            else
            {
                LockerPanelController.instance._inventoryPanelController.SelectToggle((int)myData.ItemType);
            }
            ItemManager.instance.currentLocker.IsEmptyCalculate();
        }
        else if (_currentContentType == SlotContentType.Inventory)
        {// Eger SlotContentType Inventory ise yasanacak olay.
            if (ItemManager.instance.CurrentActiveInventoryPanel == ActiveInventoryPanel.LockerInventory)
            {// Eger acilan panel LockerInventory ise yasanacak olay.
                Debug.Log("slotItemName => " + name);
                Debug.Log("slotItemName => " + myData.ID);
                ItemManager.instance.RemoveInventoryInPlayerInventory(myData);
                if (!(LockerPanelController.instance._inventoryPanelController._inventoryToggles[(int)myData.ItemType].isOn))
                {
                    LockerPanelController.instance._inventoryPanelController._inventoryToggles[(int)myData.ItemType].isOn = true;
                }
                else
                {
                    LockerPanelController.instance._inventoryPanelController.SelectToggle((int)myData.ItemType);
                }
                LockerPanelController.instance.AddItemInCurrentLocker(myData);
                ItemManager.instance.currentLocker.IsEmptyCalculate();
            }
            else if (ItemManager.instance.CurrentActiveInventoryPanel == ActiveInventoryPanel.PlayerInventory)
            {// Eger acilan panel PlayerInventory ise yasanacak olay.

            }
        }
        
    }    
}
public enum SlotContentType
{
    None,
    Locker,
    Inventory
}
