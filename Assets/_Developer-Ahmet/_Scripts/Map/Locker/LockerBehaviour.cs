using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class LockerBehaviour : MonoBehaviour
{
    public bool _isSerched;
    public bool _isEmpty = false;
    private bool _isBusy;
    public List<ItemData> MyItems = new List<ItemData>();   
    private void Awake()
    {
        IsEmptyCalculate();
    }
    private bool GetIsEmpty()
    {
        bool isEmpty = MyItems.Count <= 0 ? true : false;
        return isEmpty;
    }
    public void IsEmptyCalculate()
    {
        if (GetIsEmpty()) _isEmpty = true;
        else _isEmpty = false;
    }
    public void InteractLocker()
    {
        if (_isBusy)
        {
            if (UIManager.instance.GetInteractPanelActive())
            {
                UIManager.instance.InteractPanelActivation(false);
            }
            return;
        }
        InteractPanelController.instance.SetCurrentInteractionLocker(this);
    }
    public void StartInteract()
    {
        ItemManager.instance.currentLocker = this;
        ItemManager.instance.CurrentActiveInventoryPanel = ActiveInventoryPanel.LockerInventory;
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
