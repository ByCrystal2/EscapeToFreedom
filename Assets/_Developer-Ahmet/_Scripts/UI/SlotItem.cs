using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.DualShock;

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
            switch (myData.ItemType)
            {
                case ItemType.None:
                    break;
                case ItemType.FriendPaper:
                    SetToggleControl(1);
                    break;
                case ItemType.Key:
                    SetToggleControl(3);
                    break;
                case ItemType.Crowbar:
                    SetToggleControl(2);
                    break;
                case ItemType.Mushroom:
                case ItemType.Flower:
                case ItemType.Book:
                case ItemType.Knife:
                case ItemType.WalkieTalkie:
                case ItemType.Apple:
                    SetToggleControl(4);
                    break;
                default:
                    break;
            }
            ItemManager.instance.currentLocker.IsEmptyCalculate();

            Collectable currentColObjInLocker = GetComponent<Collectable>();

            if (!currentColObjInLocker.GetIsCollected())
                currentColObjInLocker.Collect();
            else
                Debug.Log($"Nesne Zaten Toplandi. Infos => Header({currentColObjInLocker._info.Text}), Message({currentColObjInLocker._info.Description})");
        }
        else if (_currentContentType == SlotContentType.Inventory)
        {// Eger SlotContentType Inventory ise yasanacak olay.
            if (ItemManager.instance.CurrentActiveInventoryPanel == ActiveInventoryPanel.LockerInventory)
            {// Eger acilan panel LockerInventory ise yasanacak olay.
                Debug.Log("slotItemName => " + name);
                Debug.Log("slotItemName => " + myData.ID);
                ItemManager.instance.RemoveInventoryInPlayerInventory(myData);
                SetToggleControl((int)myData.ItemType);
                LockerPanelController.instance.AddItemInCurrentLocker(myData);
                ItemManager.instance.currentLocker.IsEmptyCalculate();
            }
            else if (ItemManager.instance.CurrentActiveInventoryPanel == ActiveInventoryPanel.PlayerInventory)
            {// Eger acilan panel PlayerInventory ise yasanacak olay.

            }
        }
        
    }    
    public void SetToggleControl(int _index)
    {
        if (!(LockerPanelController.instance._inventoryPanelController._inventoryToggles[_index].isOn))
        {
            LockerPanelController.instance._inventoryPanelController._inventoryToggles[_index].isOn = true;
        }
        else
        {
            LockerPanelController.instance._inventoryPanelController.SelectToggle(_index);
        }
    }
}
public enum SlotContentType
{
    None,
    Locker,
    Inventory
}
