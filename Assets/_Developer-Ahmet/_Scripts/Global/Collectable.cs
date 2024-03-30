using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Collectable : MonoBehaviour, ICollectable
{
    [SerializeField] CollectType _collectableType;
    [Header("Target Tools")]
    [SerializeField] Interactable[] _targetTools;
    [Header("PaperTools")]
    public GameText _info;
    public int ItemDataID;
    public int TextInfoID;
    private bool isCollected;
    private void Awake()
    {
        _info = TextManager.instance.GetFriendTextWithID(TextInfoID);
        Debug.Log("My Text Informations => " +  _info.Text);
    }
    private void OnValidate()
    {
        TextInfoID = ItemDataID;
    }//
    public void Collect()
    {
        switch (_collectableType)
        {
            case CollectType.None:
                break;
            case CollectType.FriendPaper:
                int collectedFriendNoteCount = CollectPanelController.instance.GetCollectedFrinednoteCount();
                PuzzleManager.instance.MissionComplateController.MainStoryMultipleMissionComplate(collectedFriendNoteCount, TextInfoID, ComplateType.FriendNoteComplate);
                CollectPanelController.instance.IncreaseCollectedFirendNoteCount();
                UIManager.instance.SetActivationPaperPanel(true, _info.Title, _info.Text);

                if (collectedFriendNoteCount == 1)
                {
                    GameManager.instance.GetCurrentSchoolFloorManager().GetMyToilet().SetIsSpeaking(true);
                }
                break;
            case CollectType.Key:
                if (_targetTools != null && _targetTools.Length > 0)
                {
                    int length = _targetTools.Length;
                    for (int i = 0; i < length; i++)
                    {
                        _targetTools[i].IsLocked = false;                       
                    }
                }
                PuzzleManager.instance.MissionComplateController.MainStoryMultipleMissionComplate(CollectPanelController.instance.GetCollectedKeyCount(), TextInfoID, ComplateType.SecurityKeyComplate);
                break;
            case CollectType.Mushroom:
                break;
            case CollectType.Flower:
                break;
            case CollectType.Crowbar:
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
    }
    public void SetIsCollected(bool _isCollected)
    {
        isCollected = _isCollected;
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
public enum CollectType
{
    None,
    FriendPaper,
    Key,
    Mushroom,
    Flower,
    Crowbar,
    Book,
    Knife,
    Apple
}
public interface ICollectable
{
    public void Collect();
}