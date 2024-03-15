using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("MenuTools")]
    [SerializeField] GameObject MenuPanel;
    [SerializeField] Button StartGameButton;

    [Header("InventoryTools")]
    [SerializeField] GameObject InventoryPanel;

    [Header("LockedInteract")]
    [SerializeField] GameObject LockedInteractPanel;
    [Header("DoorInteract")]
    [SerializeField] GameObject InteractPanel;    
    [Header("LockerInteract")]
    [SerializeField] GameObject LockerInteractPanel;
    [Header("MainMissionsPanel")]
    [SerializeField] GameObject MainMissionsPanel;
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
    private void Start()
    {
        StartGameButton.onClick.AddListener(StartTheGame);
        _startPos = MainMissionsPanel.transform.position;
        _endPos = new Vector3(960, _startPos.y, _startPos.z);
    }
    public void StartTheGame()
    {
        GameManager.instance.StartGame();
    }
    public void SetActivationMenuPanel(bool _active)
    {
        MenuPanel.SetActive(_active);
    }
    public void SetActivationInventoryPanel(bool _active)
    {
        InventoryPanel.SetActive(_active);
    }
    public void SetActivationMainMissionPanel(bool _active)
    {
        MainMissionsPanel.SetActive(_active);
    }
    public void LockedInteractPanelActivation(bool _active)
    {
        LockedInteractPanel.SetActive(_active);
    }
    public void InteractPanelActivation(bool _active)
    {
        InteractPanel.SetActive(_active);
    }
    public bool GetLockedInteractPanelActive()
    {
        return LockedInteractPanel.activeSelf;
    }
    public bool GetInteractPanelActive()
    {
        return InteractPanel.activeSelf;
    }
    public bool GetLockerInteractPanelActive()
    {
        return LockerInteractPanel.activeSelf;
    }
    public void LockerInteractPanelActivation(bool _active)
    {
        LockerInteractPanel.SetActive(_active);
        if (_active)
        {
            PlayerManager.instance.PlayerLock();
            PlayerManager.instance.playerInventory._isBusy = true;
            GameManager.instance.SetCursorLockMode(CursorLockMode.Confined);
        }
        else
        {
            PlayerManager.instance.PlayerUnlock();
            PlayerManager.instance.playerInventory._isBusy = false;
            InteractPanelController.instance.GetCurrentInteractedLocker()._isSerched = true;
            InteractPanelController.instance.GetCurrentInteractedLocker().SetIsBusy(false);
            GameManager.instance.SetCursorLockMode(CursorLockMode.Locked);
        }
    }
    Vector3 _startPos;
    Vector3 _endPos;
    public Tween StartDOMoveMissionPanel()
    {
        if (MainMissionsPanel.transform.position != _endPos)
        {
            return MainMissionsPanel.transform.DOLocalMoveX(_endPos.x, 2f);
        }
        return null;
    }
    public Tween EndDOMoveMissionPanel()
    {
        if (MainMissionsPanel.transform.position != _startPos)
        {
            return MainMissionsPanel.transform.DOMoveX(_startPos.x, 4f);
        }
        return null;
    }
}
