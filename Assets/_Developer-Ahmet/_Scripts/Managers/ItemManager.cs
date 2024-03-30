using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] public GameObject PaperItemPrefab;
    [SerializeField] public GameObject MushroomItemPrefab;
    [SerializeField] public GameObject KeyItemPrefab;
    [SerializeField] public GameObject FlowerItemPrefab;
    [SerializeField] public GameObject CrowbarItemPrefab;
    [SerializeField] public GameObject BookItemPrefab;
    [SerializeField] public GameObject KnifeItemPrefab;
    [SerializeField] public GameObject AppleItemPrefab;

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

        ItemData i19 = new ItemData(19, "Crowbar", "7. katta olan item", ItemType.Crowbar);


        ItemData ic_1 = new ItemData(2000, "Friend FriendPaper", "10. katta erkekler tuvaletinde bulunan arkadaþýnýn notu #1", ItemType.Paper);
        ItemData ic_13 = new ItemData(2001, "Friend FriendPaper", "10. katta kütüphanede bulunan arkadaþýnýn notu #2", ItemType.Paper);
        ItemData ic_14 = new ItemData(2002, "Friend FriendPaper", "9. katta masanýn üstünde bulunan arkadaþýnýn notu #3", ItemType.Paper);
        ItemData ic_15 = new ItemData(2003, "Friend FriendPaper", "7. katta radyatorun altýnda ki arkadaþýnýn notu #4", ItemType.Paper);
        ItemData ic_16 = new ItemData(2004, "Friend FriendPaper", "6. katta depoda bulunan arkadaþýnýn notu #5", ItemType.Paper);
        ItemData ic_17 = new ItemData(2005, "Friend FriendPaper", "5. kat giriþinde ki masanýn üstünde bulunan arkadaþýnýn notu #6", ItemType.Paper);
        ItemData ic_18 = new ItemData(2006, "Friend FriendPaper", "4. katta arkadaþýnýn sýnýfýnda bulunan arkadaþýnýn notu #7", ItemType.Paper);
        ItemData ic_19 = new ItemData(2007, "Friend FriendPaper", "3. kat giriþinde ki sandalyenin altýnda bulunan arkadaþýnýn notu #8", ItemType.Paper);
        ItemData ic_20 = new ItemData(2008, "Friend FriendPaper", "2. kat bilmeceler giriþinde bulunan arkadaþýnýn notu #9", ItemType.Paper);
        ItemData ic_21 = new ItemData(2009, "Friend FriendPaper", "1. kat giriþinde bulunan arkadaþýnýn notu #10", ItemType.Paper);
        

        ItemData ic_2 = new ItemData(2100, "Key1", "1. katta olan item", ItemType.Key);
        ItemData ic_3 = new ItemData(2101, "Key2", "2. katta olan item", ItemType.Key);
        ItemData ic_4 = new ItemData(2102, "Key3", "3. katta olan item", ItemType.Key);
        ItemData ic_5 = new ItemData(2103, "Key4", "4. katta olan item", ItemType.Key);
        ItemData ic_6 = new ItemData(2104, "Key5", "5. katta olan item", ItemType.Key);
        ItemData ic_7 = new ItemData(2105, "Key6", "6. katta olan item", ItemType.Key);
        ItemData ic_8 = new ItemData(2106, "Key7", "7. katta olan item", ItemType.Key);
        ItemData ic_9 = new ItemData(2107, "Key8", "8. katta olan item", ItemType.Key);
        ItemData ic_10 = new ItemData(2108, "Key9", "9. katta olan item", ItemType.Key);
        ItemData ic_11 = new ItemData(2109, "Key10", "10. katta olan item", ItemType.Key);

        //Normal Door Keys
        ItemData ic_12 = new ItemData(2011, "Spare Stair Door Key", "10. kat çýkýþ anahtarý", ItemType.Key);
        


        _items.Add(i19);

        _items.Add(ic_1);
        _items.Add(ic_13);
        _items.Add(ic_14);
        _items.Add(ic_15);
        _items.Add(ic_16);
        _items.Add(ic_17);
        _items.Add(ic_18);
        _items.Add(ic_19);
        _items.Add(ic_20);
        _items.Add(ic_21);



        _items.Add(ic_2);
        _items.Add(ic_3);
        _items.Add(ic_4);
        _items.Add(ic_5);
        _items.Add(ic_6);
        _items.Add(ic_7);
        _items.Add(ic_8);
        _items.Add(ic_9);
        _items.Add(ic_10);
        _items.Add(ic_11);
        _items.Add(ic_12);
        foreach (var item in _items)
        {
            if (item.ItemType == ItemType.Mushroom || item.ItemType == ItemType.Flower || item.ItemType == ItemType.Knife || item.ItemType == ItemType.Apple || item.ItemType == ItemType.Book)
            {
                item.IsOther = true;
            }
        }
    }
    public GameObject GetPrefabAccordingToItemType(ItemType _type)
    {
        GameObject prefab = (_type) switch
        {
            ItemType.None => new GameObject("None Type"),
            ItemType.Paper => PaperItemPrefab,
            ItemType.Mushroom => MushroomItemPrefab,
            ItemType.Key => KeyItemPrefab,
            ItemType.Flower => FlowerItemPrefab,
            ItemType.Crowbar => CrowbarItemPrefab,
            ItemType.Book => BookItemPrefab,
            ItemType.Knife => KnifeItemPrefab,
            ItemType.Apple => AppleItemPrefab,
            _ => new GameObject("Null Type")
        };
        return prefab;
    }
    public ItemData GetItemWithID(int _id)
    {
        return GetAllItems().Where(x=> x.ID  == _id).SingleOrDefault();
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
    public List<ItemData> GetPlayerInventoryOtherItems()
    {
        return PlayerManager.instance.playerInventory.GetOtherItems();
    }
}
public enum ItemType
{
    None,
    Paper,
    Mushroom,
    Key,
    Flower,
    Crowbar,
    Book,
    Knife,
    Apple
}
public enum ActiveInventoryPanel // su anda aktif olan envanter paneli bilgisini tutmak icin.
{
    None,
    PlayerInventory,
    LockerInventory
}
