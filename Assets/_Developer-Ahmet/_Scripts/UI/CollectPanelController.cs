using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectPanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TxtCollect;
    private CollectType _currentCollectableObject;
    private Collectable _currentCollectableBehaviour;
    private int collectedFrinedNoteCount;
    private int collectedSecurityKeyCount;

    private int collectedPersonelCount;
    private int collectedStairDoorKeyCount;
    private int collectedNormalDoorKeyCount;

    private int collectedCrowbarCount;
    public static CollectPanelController instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void SetCurrentCollectable(Collectable _collectable)
    {
        _currentCollectableObject = _collectable.GetCollectType();
        _currentCollectableBehaviour = _collectable;
        TxtCollect.text = "Topla (" + _collectable.GetCollectType()+ ")";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !PlayerManager.instance.player.IsBusy)
        {            
            _currentCollectableBehaviour.Collect();           
        }
    }   
    public int GetCollectedFrinednoteCount()
    {
        return collectedFrinedNoteCount;
    }
    public void IncreaseCollectedFirendNoteCount()
    {
        collectedFrinedNoteCount++;
    }
    public int GetAndIncreaseDesiredKeyCount(KeyType _type)
    {
        int beforeCount = 0;
        switch (_type)
        {
            case KeyType.None:
                break;
            case KeyType.SecurityKey:
                beforeCount = GetCollectedSecurityKeyCount();
                IncreaseCollectedSecurityKeyCount();
                break;
            case KeyType.Personel:
                beforeCount = GetCollectedPersonelKeyCount();
                IncreaseCollectedPersonelKeyCount();
                break;
            case KeyType.StairDoor:
                beforeCount = GetCollectedStairDoorKeyCount();
                IncreaseCollectedStairDoorKeyCount();
                break;
            case KeyType.NormalDoor:
                beforeCount = GetCollectedNormalDoorKeyCount();
                IncreaseCollectedNormalDoorKeyCount();
                break;
            default:
                break;
        }
        return beforeCount;
    }
    private int GetCollectedSecurityKeyCount()
    {
        return collectedSecurityKeyCount;
    }
    public void IncreaseCollectedSecurityKeyCount()
    {
        collectedSecurityKeyCount++;
    }

    private int GetCollectedPersonelKeyCount()
    {
        return collectedPersonelCount;
    }
    public void IncreaseCollectedPersonelKeyCount()
    {
        collectedPersonelCount++;
    }
    private int GetCollectedStairDoorKeyCount()
    {
        return collectedStairDoorKeyCount;
    }
    public void IncreaseCollectedStairDoorKeyCount()
    {
        collectedStairDoorKeyCount++;
    }
    private int GetCollectedNormalDoorKeyCount()
    {
        return collectedNormalDoorKeyCount;
    }
    public void IncreaseCollectedNormalDoorKeyCount()
    {
        collectedNormalDoorKeyCount++;
    }

    public int GetCollectedCrowbarCount()
    {
        return collectedNormalDoorKeyCount;
    }
    public void IncreaseCollectedCrowbarCount()
    {
        collectedNormalDoorKeyCount++;
    }
}
