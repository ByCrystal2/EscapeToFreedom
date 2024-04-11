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
            DoorBehavior _myDoor = GetComponent<DoorBehavior>();
            if (_myDoor.isEndGameDoor)
            {
                int keyCount = BookManager.instance.TotalSecurityKeyIsTenInPlayerInventory();
                if (keyCount == 10)
                {
                    UIManager.instance.SetActivationEndGameDoorPanel(false);
                    _myDoor.InteractDoor();
                }
                else
                {
                    UIManager.instance.SetActivationEndGameDoorPanel(true, keyCount);
                }
                return;
            }
            if (!IsLocked)
            {
                _myDoor.InteractDoor();
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
        else if (_interactableType == InteractableType.Closet)
        {
            MainToiletBehaviour myBehaviour = GetComponent<MainToiletBehaviour>();
            if (!myBehaviour.GetIsBusy())
            {
                UIManager.instance.InteractPanelActivation(true);
                myBehaviour.InteractCloset();
            }
            else
            {

            }
        }
        else if (_interactableType == InteractableType.Personnel)
        {
            PersonnelBehaviour myBehaviour = GetComponent<PersonnelBehaviour>();
            if (!myBehaviour.GetIsCanCatchPlayer())
            {
                if (!myBehaviour.GetIsBusy() && myBehaviour.Puzzle != null)
                {
                    UIManager.instance.InteractPanelActivation(true);
                    myBehaviour.InteractPersonnel();
                }
                else
                {

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
    FireAlarm,
    Closet,
    Personnel
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
