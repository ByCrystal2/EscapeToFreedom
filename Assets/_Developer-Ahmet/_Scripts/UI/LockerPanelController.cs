using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerPanelController : MonoBehaviour
{
    [SerializeField] Transform _lockerSlotsContent;
    [SerializeField] Transform _inventoryContent;
    [SerializeField] public InventoryPanelController _inventoryPanelController;

    private LockerBehaviour currentLocker;

    public static LockerPanelController instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;        
    }
    private void OnEnable()
    {
        currentLocker = ItemManager.instance.currentLocker;
        if (currentLocker != null)
            AddLockerItemsInLockerContentSlots();
        else
            Debug.Log("currentActiveLocker is Null!");
    }
    private void OnDisable()
    {
        ClearInventoryContent();
    }
    public void RemoveItemInCurrentLocker(ItemData _item)
    {
        currentLocker.MyItems.Remove(_item);
        ItemManager.instance.AddInventoryInPlayerInventory(_item);
        if (currentLocker.MyItems.Count <= 0)
        {
            currentLocker._isEmpty = true;
        }
        ClearInventoryContent();
        AddLockerItemsInLockerContentSlots();
    }
    public void AddItemInCurrentLocker(ItemData _item)
    {
        currentLocker.MyItems.Add(_item);
        ClearInventoryContent();
        AddLockerItemsInLockerContentSlots();
    }
    private void AddLockerItemsInLockerContentSlots()
    {
        
        AddItemsInSelectedLockerContent(currentLocker.MyItems);
    }
    private void ClearInventoryContent()
    {
        int length = _lockerSlotsContent.childCount;
        int fullSlotCount = 0;
        for (int i = 0; i < length; i++)
        {
            if (_lockerSlotsContent.GetChild(i).TryGetComponent(out InventorySlotObj _slot))
            {
                if (_slot.GetIsFull())
                {
                    Destroy(_slot.transform.GetChild(0).gameObject, 0.05f);
                    _slot.SetIsFull(false);
                    fullSlotCount++;
                }
            }
        }
        Debug.Log("Silinen slot itemi sayisi => " + fullSlotCount);
    }
    public void AddItemsInSelectedLockerContent(List<ItemData> items)
    {
        List<InventorySlotObj> SlotContents = new List<InventorySlotObj>();
        int length1 = _lockerSlotsContent.childCount;
        int SlotCount = 0;
        for (int i = 0; i < length1; i++)
        {
            if (_lockerSlotsContent.GetChild(i).TryGetComponent(out InventorySlotObj _slot))
            {
                if (!_slot.GetIsFull())
                {
                    SlotContents.Add(_slot);
                    SlotCount++;
                }                
            }
            if (SlotCount >= items.Count)
            {
                break;
            }
        }

        int length = items.Count;
        for (int i = 0; i < length; i++)
        {
            InventorySlotObj currentSlot = SlotContents[i];
            
            Debug.Log("_slot => " + currentSlot.gameObject.name + " isFull => " + currentSlot.GetIsFull());                
            currentSlot.SetIsFull(true);
            ItemData currentItem = items[i];
            GameObject CurrentItemObj = ItemManager.instance.GetPrefabAccordingToItemType(currentItem.ItemType);
            if (currentItem != null)
            {
                GameObject newItem = Instantiate(CurrentItemObj, currentSlot.transform);
                SlotItem slotItem = newItem.GetComponent<SlotItem>();
                if (slotItem != null)
                {
                    slotItem.SetMyData(currentItem);
                    slotItem.SetItemType(currentItem.ItemType);
                    Debug.Log(newItem + " is added locker content");
                }
                else Debug.Log("SlotItem is Null! Locker => " + currentLocker.name + " Slot => " + currentSlot.name);
                          
            }
            
        }   
    }
    public LockerBehaviour GetCurrentLocker()
    {
        return currentLocker;
    }
    public void SetCurrentLocker(LockerBehaviour _locker)
    {
        currentLocker = _locker;
    }
}
