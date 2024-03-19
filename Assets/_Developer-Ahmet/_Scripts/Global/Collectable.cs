using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Collectable : MonoBehaviour, ICollectable
{
    [SerializeField] CollectType _collectableType;
    [Header("PaperTools")]
    [SerializeField] string _headerText;
    [SerializeField] string _messageText;
    public int ItemDataID; // Collectable items Starter ID is 5000;
    private bool isCollected;
    public void Collect()
    {
        switch (_collectableType)
        {
            case CollectType.None:
                break;
            case CollectType.Paper:
                UIManager.instance.SetActivationPaperPanel(true, _headerText, _messageText);
                break;
            case CollectType.Key:
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
    Paper,
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