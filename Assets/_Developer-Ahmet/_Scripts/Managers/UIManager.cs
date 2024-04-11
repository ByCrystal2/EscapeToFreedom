using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("GameMain")]
    [SerializeField] GameObject GameTimePanel;
    [SerializeField] public TextMeshProUGUI txtGameTime;
    [Header("MenuTools")]
    [SerializeField] GameObject MenuPanel;
    [SerializeField] Button StartGameButton;

    [Header("InventoryTools")]
    [SerializeField] GameObject InventoryPanel;

    [Header("LockedInteract")]
    [SerializeField] GameObject LockedInteractPanel;
    [Header("Interact")]
    [SerializeField] GameObject InteractPanel;    
    [Header("LockerInteract")]
    [SerializeField] GameObject LockerInteractPanel;
    [Header("Collect")]
    [SerializeField] GameObject CollectPanel;    
    [Header("MainMissionsPanel")]
    [SerializeField] GameObject MainMissionsPanel;
    [Header("PuzzleMissionPanel")]
    [SerializeField] GameObject PuzzleMissionsPanel;
    [Header("CatchThePlayerPanel")]
    [SerializeField] GameObject CatchThePlayerPanel;
    [SerializeField] Button StartPuzzleButton;
    [SerializeField] Button KillThemButton;
    [SerializeField] Button EscapteButton;
    [Header("PaperPanel")]
    [SerializeField] GameObject PaperPanel;
    [Header("SlotItemInfoPanel")]
    [SerializeField] GameObject SlotItemInfoPanel;
    [Header("SpeakingPanel")]
    [SerializeField] GameObject SpeakingPanel;
    [Header("PlayerAwakePanel")]
    [SerializeField] GameObject PlayerAwakePanel;
    [SerializeField] PlayableDirector playerAwalePlayable;
    [Header("EndGameToolsl")]
    [SerializeField] GameObject endGamePanel;
    [SerializeField] GameObject endGameDoorPanel;
    [SerializeField] TextMeshProUGUI txtEndGameDoorText;
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

        StartPuzzleButton.onClick.AddListener(StartCathingPuzzleBTN);
        KillThemButton.onClick.AddListener(KillThePlayerBTN);
        EscapteButton.onClick.AddListener(EscaoeBTN);

        _missionPanelStartPos = MainMissionsPanel.transform.position;
        _missionPanelendPos = new Vector3(960, _missionPanelStartPos.y, _missionPanelStartPos.z);

        _puzzlePanelStartPos = PuzzleMissionsPanel.transform.position;
        _puzzlePanelendPos = new Vector3(-960, _puzzlePanelStartPos.y, _puzzlePanelStartPos.z);
    }
    public void StartTheGame()
    {
        SetActivationMenuPanel(false);
        SetActivationPlayerAwakePanel(true);
    }
    public void PlayPlayerAwakePlayable()
    {
        playerAwalePlayable.Play();
    }
    public void StopPlayerAwakePlayable()
    {
        playerAwalePlayable.Stop();
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
    public void SetActivationPuzzleMissionPanel(bool _active)
    {
        PuzzleMissionsPanel.SetActive(_active);
    }
    public void SetActivationCatchThePlayerPanel(bool _active, bool _isHaveAPuzzle = true)
    {
        if (_isHaveAPuzzle && PuzzleManager.instance.CurrentPuzzleOwningPersonel == null)
        {
            StartPuzzleButton.gameObject.SetActive(true);
        }
        else
        {
            StartPuzzleButton.gameObject.SetActive(false);
        }
        CatchThePlayerPanel.SetActive(_active);
    }
    public void SetActivationGameTimePanel(bool _active)
    {
        GameTimePanel.SetActive(_active);
    }
    public void SetActivationPaperPanel(bool _active, string _header = null, string _message = null)
    {
        PlayerManager.instance.PlayerLock();
        
        if (!_active) return;
        PaperPanel.SetActive(_active);
        PaperPanel.GetComponent<CanvasGroup>().DOFade(1, 1f).OnComplete(() =>
        {
            if (_header != null && _message != null)
            {
                PaperPanel.GetComponent<PaperPanelController>().StartPaperText(_header, _message);
            }
        });        
    }
    public void SetActivationSlotItemInfoPanel(bool _active, Vector3 _itemPos, string _itemName = null, Sprite _itemSprite = null, string _itemHeader = null, string _itemDescription = null)
    {
        SlotItemInfoPanel.SetActive(_active);
        if (_active)
        {
            SlotInfoPanelController.instance.SetSlotInfoPanelUIs(_itemName,_itemSprite, _itemHeader, _itemDescription, _itemPos);
        }
    }
    public void SetActivationSpeakingPanel(bool  _active)
    {        
        if (_active)
        {
            SpeakingPanel.SetActive(true);
            SpeakingPanelController.PlayToiletSpeakingPlayable();
        }
        else
        {
            SpeakingPanelController.StopToiletSpeakingPlayable();
            SpeakingPanel.SetActive(false);
        }
    }
    public void SetActivationPlayerAwakePanel(bool _active)
    {
        if (_active)
        {
            PlayerAwakePanel.SetActive(true);
            PlayPlayerAwakePlayable();
        }
        else
        {
            StopPlayerAwakePlayable();
            PlayerAwakePanel.SetActive(false);
        }
    }
    public void SetActivationEndGameDoorPanel(bool _active, int _keyCount = 0)
    {
        if (_active)
        {
            endGameDoorPanel.SetActive(true);
            txtEndGameDoorText.text = $"Güvenlik anahtarý eksik [{_keyCount}/10]";
        }
        else
        {
            endGameDoorPanel.SetActive(false);
        }
    }
    public void LockedInteractPanelActivation(bool _active)
    {
        LockedInteractPanel.SetActive(_active);
    }
    public void InteractPanelActivation(bool _active)
    {
        InteractPanel.SetActive(_active);
    }
    public void CollectPanelActivation(bool _active)
    {
        CollectPanel.SetActive(_active);
    }
    public bool GetLockedInteractPanelActive()
    {
        return LockedInteractPanel.activeSelf;
    }
    public bool GetInteractPanelActive()
    {
        return InteractPanel.activeSelf;
    }
    public bool GetCollectPanelActive()
    {
        return CollectPanel.activeSelf;
    }
    public bool GetLockerInteractPanelActive()
    {
        return LockerInteractPanel.activeSelf;
    }
    public bool GetCatchThePlayerPanelActive()
    {
        return CatchThePlayerPanel.activeSelf;
    }
    public bool GetSlotItemInfoPanelActive()
    {
        return SlotItemInfoPanel.activeSelf;
    }
    public bool GetSpeakingPanelActive()
    {
        return SpeakingPanel.activeSelf;
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
    Vector3 _missionPanelStartPos;
    Vector3 _missionPanelendPos;
    public Tween StartDOMoveMissionPanel(bool key_h = false)
    {
        if (key_h)
        {
            return MainMissionsPanel.transform.DOLocalMoveX(_missionPanelendPos.x, 3f);
        }
        if (MainMissionsPanel.transform.position != _missionPanelendPos)
        {
            return MainMissionsPanel.transform.DOLocalMoveX(_missionPanelendPos.x, 3f);
        }
        return null;
    }
    public Tween EndDOMoveMissionPanel(bool key_h = false)
    {
        if (key_h)
        {
            return MainMissionsPanel.transform.DOLocalMoveX(_missionPanelStartPos.x, 3f);
        }
        if (MainMissionsPanel.transform.position != _missionPanelStartPos)
        {
            return MainMissionsPanel.transform.DOLocalMoveX(_missionPanelStartPos.x, 5f);
        }
        return null;
    }
    Vector3 _puzzlePanelStartPos;
    Vector3 _puzzlePanelendPos;
    public Tween StartDOMovePuzzlePanel()
    {
        if (PuzzleMissionsPanel.transform.position != _puzzlePanelendPos)
        {
            return PuzzleMissionsPanel.transform.DOLocalMoveX(_puzzlePanelendPos.x, 2f);
        }
        return null;
    }
    public Tween EndDOMovePuzzlePanel()
    {
        if (PuzzleMissionsPanel.transform.position != _puzzlePanelStartPos)
        {
            return PuzzleMissionsPanel.transform.DOMoveX(_puzzlePanelStartPos.x, 4f);
        }
        return null;
    }
    public void StartCathingPuzzleBTN()
    {        
        GameManager.instance.CurrentCatchedPersonnelWorkAndMoveOn(true);
        SetActivationCatchThePlayerPanel(false);
        PuzzleManager.instance.SetCurrentPuzzle(GameManager.instance.CurrentCathedPlayerPersonel);
        PuzzleManager.instance.AddMissionsInCurrentPuzzleContent();
        GameManager.instance.CurrentCathedPlayerPersonel.SetIsCanCatchPlayer(false);
        PlayerManager.instance.PlayerUnlock();
        GameManager.instance.SetCursorLockMode(CursorLockMode.Locked);
    }

    public void KillThePlayerBTN()
    {
        SetActivationCatchThePlayerPanel(false);
        bool isCan = PlayerManager.instance.CanPlayerKillThem(GameManager.instance.CurrentCathedPlayerPersonel.GetMe());

        if (isCan)
        {
            GameManager.instance.CurrentCathedPlayerPersonel.KillMe();
        }
        else
        {
            GameManager.instance.GameOver(GameEndType.CatchingStaff);
        }
        PlayerManager.instance.PlayerUnlock();
        GameManager.instance.SetCursorLockMode(CursorLockMode.Locked);        
    }

    public void EscaoeBTN()
    {
        GameManager.instance.CurrentCatchedPersonnelWorkAndMoveOn(true);
        SetActivationCatchThePlayerPanel(false);
        GameManager.instance.GameOver(GameEndType.CatchingStaff);
        PlayerManager.instance.PlayerUnlock();
        GameManager.instance.SetCursorLockMode(CursorLockMode.Locked);        
    }
}
