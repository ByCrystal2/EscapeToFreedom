using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] public GameObject Test1ItemPrefab;
    [SerializeField] public GameObject Test2ItemPrefab;
    [SerializeField] public GameObject KeyItemPrefab;
    [SerializeField] public GameObject CrowbarItemPrefab;

    public ActiveInventoryPanel CurrentActiveInventoryPanel;
    List<ItemData> _items = new List<ItemData>();
    [HideInInspector] public LockerBehaviour currentLocker;
    public static ItemManager instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        ItemData i1 = new ItemData(1, "Key1", "1. katta olan item", ItemType.Key);
        ItemData i2 = new ItemData(2, "Key2", "2. katta olan item", ItemType.Key);
        ItemData i3 = new ItemData(3, "Key3", "3. katta olan item", ItemType.Key);
        ItemData i4 = new ItemData(4, "Key4", "4. katta olan item", ItemType.Key);
        ItemData i5 = new ItemData(5, "Key5", "5. katta olan item", ItemType.Key);
        ItemData i6 = new ItemData(6, "Key6", "6. katta olan item", ItemType.Key);
        ItemData i7 = new ItemData(7, "Key7", "7. katta olan item", ItemType.Key);
        ItemData i8 = new ItemData(8, "Key8", "8. katta olan item", ItemType.Key);
        ItemData i9 = new ItemData(9, "Key9", "9. katta olan item", ItemType.Key);
        ItemData i10 = new ItemData(10, "Key10", "10. katta olan item", ItemType.Key);

        ItemData i11 = new ItemData(11, "Test1_1", "1. katta olan item", ItemType.Test1);
        ItemData i12 = new ItemData(12, "Test1_2", "2. katta olan item", ItemType.Test1);
        ItemData i13 = new ItemData(13, "Test1_3", "3. katta olan item", ItemType.Test1);
        ItemData i14 = new ItemData(14, "Test1_4", "4. katta olan item", ItemType.Test1);

        ItemData i15 = new ItemData(15, "Test2_1", "1. katta olan item", ItemType.Test2);
        ItemData i16 = new ItemData(16, "Test2_2", "2. katta olan item", ItemType.Test2);
        ItemData i17 = new ItemData(17, "Test2_3", "3. katta olan item", ItemType.Test2);
        ItemData i18 = new ItemData(18, "Test2_4", "4. katta olan item", ItemType.Test2);

        ItemData i19 = new ItemData(19, "Crowbar", "7. katta olan item", ItemType.Crowbar);

        _items.Add(i1);
        _items.Add(i2);
        _items.Add(i3);
        _items.Add(i4);
        _items.Add(i5);
        _items.Add(i6);
        _items.Add(i7);
        _items.Add(i8);
        _items.Add(i9);
        _items.Add(i10);

        _items.Add(i11);
        _items.Add(i12);
        _items.Add(i13);
        _items.Add(i14);

        _items.Add(i15);
        _items.Add(i16);
        _items.Add(i17);
        _items.Add(i18);

        _items.Add(i19);
    }
    public GameObject GetPrefabAccordingToItemType(ItemType _type)
    {
        GameObject prefab = (_type) switch
        {
            ItemType.None => new GameObject("None Type"),
            ItemType.Test1 => Test1ItemPrefab,
            ItemType.Test2 => Test2ItemPrefab,
            ItemType.Key => KeyItemPrefab,
            ItemType.Crowbar => CrowbarItemPrefab,
            _ => new GameObject("Null Type")
        };
        return prefab;
    }
    public void AddInventoryInPlayerInventory(ItemData itemData)
    {
        PlayerManager.instance.playerInventory.AddItemInInventory(itemData);
    }
    public void RemoveInventoryInPlayerInventory(ItemData itemData)
    {
        PlayerManager.instance.playerInventory.RemoveItemInInventory(itemData);
    }
    public List<ItemData> GetAllItems()
    {
        return _items;
    }
    public List<ItemData> GetDesiredItemTypeItems(ItemType itemType)
    {
        return _items.Where(x=> x.ItemType == itemType).ToList();
    }
    public ItemData GetRandomItem()
    {
        int index = Random.Range(0, _items.Count);
        return _items[index];
    }
    public ItemData GetDesiredItemTypeRandomItem(ItemType itemType)
    {
        List<ItemData> currentItems = _items.Where(x => x.ItemType == itemType).ToList();
        int index = Random.Range(0,currentItems.Count);
        return currentItems[index];
    }
    public List<ItemData> GetDesiredPlayerInventoryItemTypeItems(ItemType itemType)
    {
        return PlayerManager.instance.playerInventory.GetDesiredItemTypeCurrentItems(itemType);
    }
    public List<ItemData> GetPlayerInventoryItems()
    {
        return PlayerManager.instance.playerInventory.GetCurrentItems();
    }
}
public enum ItemType
{
    None,
    Test1,//degistirelecek.
    Test2,//degistirelecek.
    Key,
    Crowbar
}
public enum ActiveInventoryPanel // su anda aktif olan envanter paneli bilgisini tutmak icin.
{
    None,
    PlayerInventory,
    LockerInventory
}
