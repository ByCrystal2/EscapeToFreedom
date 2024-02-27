using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class LockerBehaviour : MonoBehaviour
{
    public bool _isSerched;
    public bool _isEmpty = true;
    private bool _isBusy;

    public void InteractLocker()
    {
        UIManager.instance.InteractPanelActivation(true);
        InteractPanelController.instance.SetCurrentInteractionLocker(this);
    }
    public void StartInteract()
    {
        UIManager.instance.LockerInteractPanelActivation(true);
        SetIsBusy(true);
    }   
    public void SetIsBusy(bool _active)
    {
        _isBusy = _active;
    }
    public bool GetIsBusy()
    {
        return _isBusy;
    }
}
