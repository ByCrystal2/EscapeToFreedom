using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryPanelController : MonoBehaviour
{
    [SerializeField] public Toggle[] _inventoryToggles;
    [SerializeField] Transform _inventoryContent;
    [SerializeField] Text _inventoryHeaderText;
    private void Awake()
    {
        int length = _inventoryToggles.Length;
        for (int i = 0; i < length; i++)
        {
            int toggleIndex = i; // Indisin kopyasýný oluþtur
            _inventoryToggles[i].onValueChanged.AddListener((bool value) =>
            {
                if (value)
                {
                    SelectToggle(toggleIndex);
                }
            });            
        }
    }
    private void OnEnable()
    {
        SelectToggle(0);
    }
    private void OnDisable()
    {
        ClearInventoryContent();
    }
    public void SelectToggle(int _index)
    {
        Debug.Log("SelectedToggle Index => " + _index);
        List<ItemData> playerAllItems = new List<ItemData>();
        if (_index == 0)
        {
            playerAllItems = ItemManager.instance.GetPlayerInventoryItems();
            ClearInventoryContent();
            _inventoryHeaderText.text = playerAllItems.Count > 0 ? "All" : "All (Empty)";
            int length = playerAllItems.Count;
            for (int i = 0; i < length; i++)
            {
                AddItemsInSelectedTabInvetory(playerAllItems[i]);
            }
            return;
        }
        if (_index == 4)
        {
            playerAllItems = ItemManager.instance.GetPlayerInventoryOtherItems();
            ClearInventoryContent();
            _inventoryHeaderText.text = playerAllItems.Count > 0 ? "Others" : "Others (Empty)";
            int length = playerAllItems.Count;
            for (int i = 0; i < length; i++)
            {
                AddItemsInSelectedTabInvetory(playerAllItems[i]);
            }
            return;
        }
        (List<ItemData> currentItems, bool isHaveItem, GameObject currentItemPrefab, string headerText)
            = (_index) switch
        {            
            1 => (ItemManager.instance.GetDesiredPlayerInventoryItemTypeItems(ItemType.FriendPaper), true, ItemManager.instance.PaperItemPrefab, "Notes"),
            2 => (ItemManager.instance.GetDesiredPlayerInventoryItemTypeItems(ItemType.Crowbar), true, ItemManager.instance.CrowbarItemPrefab, "Crowbars"),
            3 => (ItemManager.instance.GetDesiredPlayerInventoryItemTypeItems(ItemType.Key), true, ItemManager.instance.KeyItemPrefab, "Keys"),
            _ => (new List<ItemData>(), false,new GameObject(), "Empty")
        };
        if (isHaveItem)
        {
            Debug.Log("isHaveItem => " + isHaveItem + " and item count => " + currentItems.Count);
            ClearInventoryContent();
            _inventoryHeaderText.text = currentItems.Count > 0 ? headerText : headerText + " (Empty)";            
            int length = currentItems.Count;
            for (int i = 0; i < length; i++)
            {
                AddItemsInSelectedTabInvetory(currentItemPrefab, currentItems[i]);
            }
        }
        else
        {
            Debug.Log("index'i => " + _index + " olan Toggle'a ait bir 'case' bulunamadi.");
        }
    }    
    private void ClearInventoryContent()
    {
        int length = _inventoryContent.childCount;
        int fullSlotCount = 0;
        for (int i = 0; i < length; i++)
        {
            if (_inventoryContent.GetChild(i).TryGetComponent(out InventorySlotObj _slot))
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
    public void AddItemsInSelectedTabInvetory(GameObject item, ItemData playerItemData)
    {
        int length = _inventoryContent.childCount;
        for (int i = 0; i < length; i++)
        {
            if (_inventoryContent.GetChild(i).TryGetComponent(out InventorySlotObj _slot))
            {
                Debug.Log("_slot => " + _slot.gameObject.name + " isFull => " + _slot.GetIsFull());
                if (!_slot.GetIsFull())
                {
                    _slot.SetIsFull(true);
                    GameObject newItem = Instantiate(item, _slot.transform);
                    newItem.GetComponent<SlotItem>().SetMyData(playerItemData);
                    newItem.GetComponent<Collectable>().SetIDOptions(playerItemData.ID,true,true);
                    Debug.Log(newItem + " is added selected tab inventory");
                    break;
                }
            }
        }
    }
    public void AddItemsInSelectedTabInvetory(ItemData playerItemData)
    {
        int length = _inventoryContent.childCount;
        for (int i = 0; i < length; i++)
        {
            if (_inventoryContent.GetChild(i).TryGetComponent(out InventorySlotObj _slot))
            {
                Debug.Log("_slot => " + _slot.gameObject.name + " isFull => " + _slot.GetIsFull());
                if (!_slot.GetIsFull())
                {
                    _slot.SetIsFull(true);
                    GameObject newItem = Instantiate(ItemManager.instance.GetPrefabAccordingToItemType(playerItemData.ItemType), _slot.transform);
                    newItem.GetComponent<SlotItem>().SetMyData(playerItemData);
                    Debug.Log(newItem + " is added selected tab inventory");
                    break;
                }
            }
        }
    }
}
