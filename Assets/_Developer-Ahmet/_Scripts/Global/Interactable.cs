using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] InteractableType _interactableType;
    public bool IsLocked = true;
    public void Interact()
    {
        if (_interactableType == InteractableType.Door)
        {
            //DoorBehavior _myDoor = GetComponent<DoorBehavior>();
            if (!IsLocked)
            {
                UIManager.instance.InteractPanelActivation(true);
                GetComponent<DoorBehavior>().InteractDoor();
            }
            else
            {
                UIManager.instance.LockedInteractPanelActivation(true);
            }
            
        }
        else if (_interactableType == InteractableType.FireAlarm)
        {

        }
        else if (_interactableType == InteractableType.Locker)
        {
            LockerBehaviour myBehaviour = GetComponent<LockerBehaviour>();
            if (!myBehaviour.GetIsBusy())
            {
                if (!IsLocked)
                {
                    UIManager.instance.InteractPanelActivation(true);
                    GetComponent<LockerBehaviour>().InteractLocker();
                }
                else
                {
                    UIManager.instance.LockedInteractPanelActivation(true);
                }
            }            
        }
        else
        {
            UIManager.instance.InteractPanelActivation(false);
        }
    }
}

public enum InteractableType
{
    Door,
    Locker,
    FireAlarm
}
public enum Direction
{
    North,
    South,
    East,
    West
}
public interface IInteractable
{
    public void Interact();
}
