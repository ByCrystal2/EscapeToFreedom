using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPanelController : MonoBehaviour
{
    public static InteractPanelController instance { get; private set; }
    [SerializeField] TextMeshProUGUI TxtDoorInteract;
    private InteractableType _currentInteractableObject;
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
    public DoorBehavior GetCurrentInteractedDoor()
    {
        return _currentDoor;
    }
    public LockerBehaviour GetCurrentInteractedLocker()
    {
        return _currentLocker;
    }
    public void SetCurrentInteractionDoor(DoorBehavior _door)
    {
        _currentInteractableObject = InteractableType.Door;
        _currentDoor = _door;
        if (_currentDoor._isOpen)
        {
            TxtDoorInteract.text = "Close Door";
        }
        else
        {
            TxtDoorInteract.text = "Open Door";
        }
    }
    public void SetCurrentInteractionLocker(LockerBehaviour _locker)
    {
        _currentInteractableObject = InteractableType.Locker;
        _currentLocker = _locker;
        if (_currentLocker._isSerched)
        {
            TxtDoorInteract.text = "Searched!";
        }
        else
        {
            if (_currentLocker._isEmpty)            
            TxtDoorInteract.text = "Search... (Empty)";
            if (_currentLocker._isEmpty)
                TxtDoorInteract.text = "Search...";
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_currentInteractableObject == InteractableType.Door)
            {
                _currentDoor.StartInteract();
            }
            else if (_currentInteractableObject == InteractableType.Locker)
            {
                _currentLocker.StartInteract();
            }
        }
    }
}

