using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        currentLocker.MyItemsIDs.Remove(_item.ID);
        ItemManager.instance.AddInventoryInPlayerInventory(_item);
        if (currentLocker.MyItemsIDs.Count <= 0)
        {
            currentLocker._isEmpty = true;
        }
        ClearInventoryContent();
        AddLockerItemsInLockerContentSlots();
    }
    public void AddItemInCurrentLocker(ItemData _item)
    {
        currentLocker.MyItemsIDs.Add(_item.ID);
        ClearInventoryContent();
        AddLockerItemsInLockerContentSlots();
    }
    private void AddLockerItemsInLockerContentSlots()
    {
        
        AddItemsInSelectedLockerContent(currentLocker.MyItemsIDs);
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
    
    public void AddItemsInSelectedLockerContent(List<int> _items)
    {
        List<ItemData> items = new List<ItemData>();
        List<ItemData> allItems = ItemManager.instance.GetAllItems();
        int length = _items.Count;
        for (int i = 0; i < length; i++)
        {
            ItemData currentItem = allItems.Where(x => x.ID == _items[i]).SingleOrDefault();
            items.Add(currentItem);
        }
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

        int length2 = items.Count;
        for (int i = 0; i < length2; i++)
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
                Collectable collectable = newItem.GetComponent<Collectable>();
                if (slotItem != null)
                {
                    slotItem.SetMyData(currentItem);
                    slotItem.SetItemType(currentItem.ItemType);
                    Debug.Log(newItem + " is added locker content");
                }
                else Debug.Log("SlotItem is Null! Locker => " + currentLocker.name + " Slot => " + currentSlot.name);
                if (collectable != null)
                {
                    collectable.SetIDOptions(currentItem.ID, true,true);
                }
                else Debug.Log("collectable is Null! Locker => " + currentLocker.name + " Collectable => " + slotItem.name);
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
