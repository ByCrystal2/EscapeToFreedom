using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TxtInteract;
    [SerializeField] GameObject InteractInputObj;
    private InteractableType _currentInteractableObject;

    private int closetSpeakedCount;
    public static InteractPanelController instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    DoorBehavior _currentDoor;
    LockerBehaviour _currentLocker;
    MainToiletBehaviour _currentCloset;
    PersonnelBehaviour _currentPersonnel;
    public DoorBehavior GetCurrentInteractedDoor()
    {
        return _currentDoor;
    }
    public LockerBehaviour GetCurrentInteractedLocker()
    {
        return _currentLocker;
    }
    public MainToiletBehaviour GetCurrentInteractedCloset()
    {
        return _currentCloset;
    }
    public PersonnelBehaviour GetCurrentInteractedPersonnel()
    {
        return _currentPersonnel;
    }
    public void SetCurrentInteractionDoor(DoorBehavior _door)
    {
        InteractInputObj.SetActive(true);
        _currentInteractableObject = InteractableType.Door;
        _currentDoor = _door;
        if (_currentDoor._isOpen)
        {
            TxtInteract.text = "Close Door";
        }
        else
        {
            TxtInteract.text = "Open Door";
        }
    }
    public void SetCurrentInteractionLocker(LockerBehaviour _locker)
    {
        InteractInputObj.SetActive(true);
        _currentInteractableObject = InteractableType.Locker;
        _currentLocker = _locker;
        if (_currentLocker._isSerched)
        {
            TxtInteract.text = "Searched!";
            if (_currentLocker._isEmpty)
            {
                TxtInteract.text += " (Empty)";
            }
        }
        else
        {
            TxtInteract.text = "Search...";
        }
    }
    public void SetCurrentInteractionCloset(MainToiletBehaviour _closet)
    {
        _currentInteractableObject = InteractableType.Closet;
        _currentCloset = _closet;
        if (_currentCloset.GetIsSpeaking())
        {
            InteractInputObj.SetActive(true);
            TxtInteract.text = "Konuþ";            
        }
        else
        {
            InteractInputObj.SetActive(false);
            TxtInteract.text = "Görevini yap!";
        }
    }
    public void SetCurrentInteractionPersonnel(PersonnelBehaviour _personnel)
    {
        InteractInputObj.SetActive(true);
        _currentInteractableObject = InteractableType.Personnel;
        _currentPersonnel = _personnel;
        if (_currentPersonnel.Puzzle.AllIsMissionsComplated())
        {
            TxtInteract.text = "Teslim Et";
        }
        else
        {
            InteractInputObj.SetActive(false);
            TxtInteract.text = "Görevini bitir!";
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !PlayerManager.instance.player.IsBusy)
        {
            if (_currentInteractableObject == InteractableType.Door)
            {
                _currentDoor.StartInteract();
            }
            else if (_currentInteractableObject == InteractableType.Locker)
            {
                _currentLocker.StartInteract();
            }
            else if (_currentInteractableObject == InteractableType.Closet)
            {
                if (_currentCloset.GetIsSpeaking())
                {
                    _currentCloset.StartInteract();
                }                
            }
            else if (_currentInteractableObject == InteractableType.Personnel)
            {
                if (_currentPersonnel.Puzzle.AllIsMissionsComplated())
                {
                    _currentPersonnel.StartInteract();
                }                
            }
        }
    }
    public int GetClosetSpeakedCount()
    {
        return closetSpeakedCount;
    }
    public void IncreaseClosetSpeakedCount()
    {
        closetSpeakedCount++;
    }
}

