using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TxtInteract;
    private InteractableType _currentInteractableObject;
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
            TxtInteract.text = "Close Door";
        }
        else
        {
            TxtInteract.text = "Open Door";
        }
    }
    public void SetCurrentInteractionLocker(LockerBehaviour _locker)
    {
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
        }
    }
}

