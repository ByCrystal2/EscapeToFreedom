using JetBrains.Annotations;
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
    [SerializeField] public GameObject WalkieTalkieItemPrefab;

    public ActiveInventoryPanel CurrentActiveInventoryPanel;
    [SerializeField]List<ItemData> _items = new List<ItemData>();
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
    public void InitItems()
    {
        ItemData ic_12 = new ItemData(2000, "Friend FriendPaper", "10. katta erkekler tuvaletinde bulunan arkada��n�n notu #1", ItemType.FriendPaper);
        ItemData ic_13 = new ItemData(2001, "Friend FriendPaper", "10. katta k�t�phanede bulunan arkada��n�n notu #2", ItemType.FriendPaper);
        ItemData ic_14 = new ItemData(2002, "Friend FriendPaper", "9. katta masan�n �st�nde bulunan arkada��n�n notu #3", ItemType.FriendPaper);
        ItemData ic_15 = new ItemData(2003, "Friend FriendPaper", "7. katta radyatorun alt�nda ki arkada��n�n notu #4", ItemType.FriendPaper);
        ItemData ic_16 = new ItemData(2004, "Friend FriendPaper", "6. katta depoda bulunan arkada��n�n notu #5", ItemType.FriendPaper);
        ItemData ic_17 = new ItemData(2005, "Friend FriendPaper", "5. kat giri�inde ki masan�n �st�nde bulunan arkada��n�n notu #6", ItemType.FriendPaper);
        ItemData ic_18 = new ItemData(2006, "Friend FriendPaper", "4. katta arkada��n�n s�n�f�nda bulunan arkada��n�n notu #7", ItemType.FriendPaper);
        ItemData ic_19 = new ItemData(2007, "Friend FriendPaper", "3. kat giri�inde ki sandalyenin alt�nda bulunan arkada��n�n notu #8", ItemType.FriendPaper);
        ItemData ic_20 = new ItemData(2008, "Friend FriendPaper", "2. kat bilmeceler giri�inde bulunan arkada��n�n notu #9", ItemType.FriendPaper);
        ItemData ic_21 = new ItemData(2009, "Friend FriendPaper", "1. kat giri�inde bulunan arkada��n�n notu #10", ItemType.FriendPaper);


        ItemData ic_2 = new ItemData(2100, "Security KeyComplate #1", "10. kat", ItemType.Key, KeyType.SecurityKey);

        ItemData ic_3 = new ItemData(2101, "Security KeyComplate #2", "9. kat", ItemType.Key, KeyType.SecurityKey); //lockerde bulunuyor.

        ItemData ic_4 = new ItemData(2102, "Security KeyComplate #3", "8. katta olan item", ItemType.Key, KeyType.SecurityKey);
        ItemData ic_5 = new ItemData(2103, "Security KeyComplate #4", "6. katta olan item", ItemType.Key, KeyType.SecurityKey);
        ItemData ic_6 = new ItemData(2104, "Security KeyComplate #5", "6. katta olan item", ItemType.Key, KeyType.SecurityKey);
        ItemData ic_7 = new ItemData(2105, "Security KeyComplate #6", "4. katta olan item", ItemType.Key, KeyType.SecurityKey);
        ItemData ic_8 = new ItemData(2106, "Security KeyComplate #7", "3. katta olan item", ItemType.Key, KeyType.SecurityKey);
        ItemData ic_9 = new ItemData(2107, "Security KeyComplate #8", "2. katta olan item", ItemType.Key, KeyType.SecurityKey);
        ItemData ic_10 = new ItemData(2108, "Security KeyComplate #9", "2. katta olan item", ItemType.Key, KeyType.SecurityKey);
        ItemData ic_11 = new ItemData(2109, "Security KeyComplate #10", "1. katta olan item", ItemType.Key, KeyType.SecurityKey);

        //Normal Door Keys
        ItemData ic_22 = new ItemData(4000, "Personel Room KeyComplate", "10. kat personel odas� giri� anahtar�", ItemType.Key, KeyType.Personel);
        ItemData ic_23 = new ItemData(4001, "Spare Stair Door KeyComplate", "10. kat ��k�� anahtar�", ItemType.Key, KeyType.StairDoor);

        // Puzzle Others
        ItemData ic_100 = new ItemData(3000, "B��ak", "Bir insan� ciddi �ekilde yaralayabilir veya �ld�rebilir!", ItemType.Knife);
        ItemData ic_101 = new ItemData(3001, "Telsiz", "G�venlik kameralar�n� iptal edebilir.", ItemType.Knife);
        ItemData ic_102 = new ItemData(3002, "Crowbar", "Kilitli nesneleri a�abilir.", ItemType.Crowbar);

        //----Puzzles------

        // 1-) Mushrooms
        ItemData ic_500 = new ItemData(1000, "3 Ba�l� Mantar", "Zehirli olabilir.", ItemType.Mushroom);
        ItemData ic_501 = new ItemData(1001, "Dev Ba�l� Mantar.", "Lezzetli bir mantar.", ItemType.Mushroom);
        ItemData ic_502 = new ItemData(1002, "T�s Mantar�.", "�la�lar i�in kullan�l�r.", ItemType.Mushroom);
        ItemData ic_505 = new ItemData(1005, "3 Ba�l� Mantar", "Zehirli olabilir.", ItemType.Mushroom);

        // 2-) Flowers
        ItemData ic_503 = new ItemData(1003, "Tekli Lavanta", "G�zel kokar.", ItemType.Flower);
        ItemData ic_504 = new ItemData(1004, "�oklu Lavanta", "B�ceklerle �evrilidir.", ItemType.Flower);

        _items.Add(ic_12);
        _items.Add(ic_13);
        _items.Add(ic_14);
        _items.Add(ic_15);
        _items.Add(ic_16);
        _items.Add(ic_17);
        _items.Add(ic_18);
        _items.Add(ic_19);
        _items.Add(ic_20);
        _items.Add(ic_21);

        _items.Add(ic_22);
        _items.Add(ic_23);

        //others
        _items.Add(ic_100);
        _items.Add(ic_101);
        _items.Add(ic_102);
        //Puzzles
        _items.Add(ic_500);
        _items.Add(ic_501);
        _items.Add(ic_502);
        _items.Add(ic_503);
        _items.Add(ic_504);
        _items.Add(ic_505);

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

        foreach (var item in _items)
        {
            if (item.ItemType == ItemType.Mushroom || item.ItemType == ItemType.Flower || item.ItemType == ItemType.Knife || item.ItemType == ItemType.Apple || item.ItemType == ItemType.Book || item.ItemType == ItemType.WalkieTalkie)
            {
                item.IsOther = true;
            }
        }
    }
    private void Start()
    {
        
        
    }
    public GameObject GetPrefabAccordingToItemType(ItemType _type)
    {
        GameObject prefab = (_type) switch
        {
            ItemType.None => new GameObject("None Type"),
            ItemType.FriendPaper => PaperItemPrefab,
            ItemType.Mushroom => MushroomItemPrefab,
            ItemType.Key => KeyItemPrefab,
            ItemType.Flower => FlowerItemPrefab,
            ItemType.Crowbar => CrowbarItemPrefab,
            ItemType.Book => BookItemPrefab,
            ItemType.Knife => KnifeItemPrefab,
            ItemType.Apple => AppleItemPrefab,
            ItemType.WalkieTalkie => WalkieTalkieItemPrefab,
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
{//COLLECTYPE ILE AYNI OLMALIDIR!
    None,
    FriendPaper,
    Key,
    Mushroom,
    Flower,
    Crowbar,
    Book,
    Knife,
    WalkieTalkie,
    Apple
}
public enum ActiveInventoryPanel // su anda aktif olan envanter paneli bilgisini tutmak icin.
{
    None,
    PlayerInventory,
    LockerInventory
}
