using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("LockedInteract")]
    [SerializeField] GameObject LockedInteractPanel;
    [Header("DoorInteract")]
    [SerializeField] GameObject InteractPanel;    
    [Header("LockerInteract")]
    [SerializeField] GameObject LockerInteractPanel;
    public static UIManager instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LockedInteractPanelActivation(bool _active)
    {
        LockedInteractPanel.SetActive(_active);
    }
    public bool GetLockedInteractPanelActive()
    {
        return LockedInteractPanel.activeSelf;
    }
    public void InteractPanelActivation(bool _active)
    {
        InteractPanel.SetActive(_active);
    }
    public bool GetInteractPanelActive()
    {
        return InteractPanel.activeSelf;
    }    
    public void LockerInteractPanelActivation(bool _active)
    {
        LockerInteractPanel.SetActive(_active);
        if (_active)
        {
            PlayerManager.instance.PlayerLock();
            GameManager.instance.SetCursorLockMode(CursorLockMode.Confined);
        }
        else
        {
            PlayerManager.instance.PlayerUnlock();
            InteractPanelController.instance.GetCurrentInteractedLocker()._isSerched = true;
            InteractPanelController.instance.GetCurrentInteractedLocker().SetIsBusy(false);
            GameManager.instance.SetCursorLockMode(CursorLockMode.Locked);
        }
    }
    public bool GetLockerInteractPanelActive()
    {
        return LockerInteractPanel.activeSelf;
    }
}
