using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Collectable : MonoBehaviour, ICollectable
{
    [SerializeField] CollectType _collectableType;
    [SerializeField] public bool IsMainStoryMission = false;
    [Header("Target Tools")]
    [SerializeField] Interactable[] _targetTools;
    [Header("FriendPaper Tools")]
    [Header("KeyComplate Tools")]
    [SerializeField] KeyType _keyType = KeyType.None;
    [Header("Global Tools")]
    [HideInInspector] public GameText _info;
    public ItemData _data;
    public int ItemDataID;
    public int TextInfoID;
    private bool isCollected;
    
    void OnValidate()
    {
        SetIDOptions(ItemDataID);
    }

    private void Start()
    {       
        SetIDOptions(ItemDataID, true);
    }
    public void SetIDOptions(int _itemId, bool _external = false, bool _isMainStory = false)
    {
        ItemDataID = _itemId;
        TextInfoID = ItemDataID;
        if (_isMainStory)
        {
            IsMainStoryMission = _isMainStory;
        }
        if (_external)
        {
            _data = ItemManager.instance.GetItemWithID(ItemDataID);
            if (_data == null)
                return;
            Debug.Log(_data.Name);
            _collectableType = (CollectType)_data.ItemType;
            if (_collectableType == CollectType.FriendPaper)
            {
                _info = TextManager.instance.GetFriendTextWithID(TextInfoID);
            }
            else if (_collectableType == CollectType.Key)
            {
                _info = TextManager.instance.GetKeyTextWithID(TextInfoID);
                _keyType = _data.KeyType;
            }
            else if (_collectableType == CollectType.Knife || _collectableType == CollectType.WalkieTalkie || _collectableType == CollectType.Crowbar)
            {
                _info = TextManager.instance.GetPuzzleOtherTextWithID(TextInfoID);
            }
            else if (_collectableType == CollectType.Mushroom || _collectableType == CollectType.Flower || _collectableType == CollectType.Book)
            {
                _info = TextManager.instance.GetPuzzlesTextsWithID(TextInfoID);
            }
        }
    }
    public void Collect()
    {
        if (_targetTools != null && _targetTools.Length > 0)
        {
            int length = _targetTools.Length;
            for (int i = 0; i < length; i++)
            {
                _targetTools[i].IsLocked = false;
            }
        }
        switch (_collectableType)
        {
            case CollectType.None:
                break;
            case CollectType.FriendPaper:
                if (IsMainStoryMission && !GetIsCollected())
                {
                    int collectedFriendNoteCount = CollectPanelController.instance.GetCollectedFrinednoteCount();
                    PuzzleManager.instance.MissionComplateController.MainStoryMultipleMissionComplate(collectedFriendNoteCount, TextInfoID, ComplateType.FriendNoteComplate);
                    CollectPanelController.instance.IncreaseCollectedFirendNoteCount();
                    UIManager.instance.SetActivationPaperPanel(true, _info.Title, _info.Text);
                }
                else
                {// main story gorevi degilse... (Puzzle gorevi.)

                }
                
                break;
            case CollectType.Key:
                if (IsMainStoryMission && !GetIsCollected())
                {
                    int collectedKeyCount = CollectPanelController.instance.GetAndIncreaseDesiredKeyCount(_keyType); PuzzleManager.instance.MissionComplateController.MainStoryMultipleMissionComplate(collectedKeyCount, TextInfoID, ComplateType.KeyComplate, _keyType);
                }
                else
                {// main story gorevi degilse... (Puzzle gorevi.)

                }

                break;
            case CollectType.Mushroom:
                if (GetIsCollected())//obje zaten toplanmissa islem yapma.
                    break; 
                if (IsMainStoryMission)
                {// main story.

                }
                else
                {// puzzle.
                    GameMission mission = PuzzleManager.instance.CurrentPuzzle.GetDesiredIDMisson(ItemDataID);
                    PuzzleManager.instance.CurrentPuzzle.MissionComplate(mission);
                    Debug.Log($"{_info.Title} isimli mantar toplandi. puzzleId => {PuzzleManager.instance.CurrentPuzzle.ID}  ItemID => {ItemDataID}");
                }
                break;
            case CollectType.Flower:
                break;
            case CollectType.Crowbar:
                if (IsMainStoryMission && !GetIsCollected())
                {
                    int collectedCrowbarCount = CollectPanelController.instance.GetCollectedCrowbarCount();
                    PuzzleManager.instance.MissionComplateController.MainStoryMultipleMissionComplate(collectedCrowbarCount, TextInfoID, ComplateType.CrowbarComplate);
                    CollectPanelController.instance.IncreaseCollectedCrowbarCount();
                }                
                else
                {// main story gorevi degilse... (Puzzle gorevi.)

                }
                break;
            case CollectType.Book:
                break;
            case CollectType.Knife:
                break;
            case CollectType.Apple:
                
                break;
            default:
                break;
        }
        Debug.Log("Toplanan nesne => " + _collectableType);
        PlayerManager.instance.playerInventory.AddCollectedItem(this);
        gameObject.SetActive(false);
        SetIsCollected(true);
    }
    public void SetIsCollected(bool _isCollected)
    {
        isCollected = _isCollected;
    }
    public void SetCollectType(CollectType _type)
    {
        _collectableType = _type;
    }
    public bool GetIsCollected()
    {
        return isCollected;
    }
    public CollectType GetCollectType()
    {
        return _collectableType;
    }
}
[System.Serializable]
public class CollectableDatas
{
    public CollectType collectableType;
    public Interactable[] targetTools;
    public KeyType keyType;
    public GameText info;
    public ItemData data;
    public int ItemDataID;
    public int TextInfoID;
    public bool isCollected;

    public CollectableDatas(int _itemId, bool _isCollected, CollectType _colType, KeyType _keyType, Interactable[] _targetTools, GameText _info, ItemData _data)
    {
        ItemDataID = _itemId;
        TextInfoID = _itemId;
        isCollected = _isCollected;
        collectableType = _colType;
        keyType = _keyType;
        System.Array.Clear(targetTools,0,targetTools.Length);
        int length = _targetTools.Length;
        for (int i = 0; i < length; i++)
        {
            targetTools[i] = _targetTools[i];
        }
        
        info = _info;
        data = _data;
    }
}
public enum CollectType
{//ITEMTYPE ILE AYNI OLMALIDIR!
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
public enum KeyType
{
    None,
    SecurityKey,
    Personel,
    StairDoor,
    NormalDoor
}
public interface ICollectable
{
    public void Collect();
}