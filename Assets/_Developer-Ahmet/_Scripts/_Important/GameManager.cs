using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameEndType _gameEndType;
    [SerializeField] private SchoolFloor _currentSchoolFloor = SchoolFloor.TenthFloor;
    public List<SchoolFloorManager> _FloorManagers = new List<SchoolFloorManager>();

    public GameObject CatchedPlayerPersonnelObj;
    public PersonnelBehaviour CurrentCathedPlayerPersonel;

    public SchoolFloorManager currentFloorManager;
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Init();
    }
    private void Start()
    {
        
    }
    private void Init()
    {
        PuzzleManager.instance.PuzzlesInit();
        TextManager.instance.TextsInit();
        AudioManager.instance.SourcesInit();
        List<SchoolFloorManager> list = FindObjectsOfType<SchoolFloorManager>().ToList();
        int length = list.Count;
        for (int i = 0; i < length; i++)
        {
            _FloorManagers.Add(list[i]);
        }
        _currentSchoolFloor = SchoolFloor.TenthFloor;
        currentFloorManager = GetDesiredSchoolManager(_currentSchoolFloor);
    } 
    public void SetCursorLockMode(CursorLockMode _lockMode)
    {
        Cursor.lockState = _lockMode;
        if (_lockMode == CursorLockMode.Locked)
            PlayerManager.instance.assetsInputs.cursorLocked = true;
        else
            PlayerManager.instance.assetsInputs.cursorLocked = false;
    }
    public void StartGame() // PlayerAwakePanel TimeLine Signal
    {
        UIManager.instance.SetActivationPlayerAwakePanel(false);
        UIManager.instance.SetActivationSpeakingPanel(true);        
        //PlayerManager.instance.PlayerUnlock();        
        SetCursorLockMode(CursorLockMode.Locked);       
    }
    public void GameOver(GameEndType _endType, PersonnelBehaviour _personnel = null)
    {
        PlayerManager.instance.PlayerLock();
        _gameEndType = _endType;
        if (_gameEndType == GameEndType.CatchingStaff)
        {            
            if (_personnel != null && _personnel.Puzzle != null)
            {
                CurrentCathedPlayerPersonel = _personnel;
                CatchedPlayerPersonnelObj = CurrentCathedPlayerPersonel.gameObject;
                _personnel._agent.isStopped = true;                
            }
        }
        else if (_gameEndType == GameEndType.CaughtOnCamera)
        {
            GetDesiredSchoolManager(_currentSchoolFloor).AllFloorPersonnelsCatchThePlayer();
        }
        else if (_gameEndType == GameEndType.PuzzleTimeEnding)
        {

        }
        else if (_gameEndType == GameEndType.GameTimeEnding)
        {

        }
    }
    public void OnlyCauthOnCameraGameOver() // [t:Prefab]SecurityCamera/Rotating/Sphere/Sensor/ONDetected Event.
    {
        PlayerManager.instance.PlayerLock();
        GetDesiredSchoolManager(_currentSchoolFloor).AllFloorPersonnelsCatchThePlayer();
    }
    public SchoolFloorManager GetCurrentSchoolFloorManager()
    {
        return currentFloorManager;
    }
    public SchoolFloorManager GetDesiredSchoolManager(SchoolFloor _floor)
    {
        return _FloorManagers.Where(x=> x.GetFloor() == _floor).SingleOrDefault();
    }
    public SchoolFloor GetCurrentSchoolFlor()
    {
        return _currentSchoolFloor;
    }
    public void CurrentCatchedPersonnelWorkAndMoveOn(bool _isMove)
    {        
        if (_isMove)
        {
            StartCoroutine(WaitForCatchedPlayerDistancing());
        }
        else
        {
            CatchedPlayerPersonnelObj.GetComponent<PersonnelBehaviour>().enabled = false;
        }
    }
    IEnumerator WaitForCatchedPlayerDistancing()
    {
        yield return new WaitForSeconds(5);
        CatchedPlayerPersonnelObj.GetComponent<PersonnelBehaviour>().enabled = true;
        CatchedPlayerPersonnelObj.GetComponent<PersonnelBehaviour>().SetRandomGoTarget();

    }
    public void SetCurrentSchoolFloor(bool _increase)
    {
        if (_increase)
        {
            int nextFloorValue = (int)_currentSchoolFloor + 1;

            if (nextFloorValue <= (int)SchoolFloor.TenthFloor)
            {
                _currentSchoolFloor = (SchoolFloor)nextFloorValue;
            }
            else
            {
                // Eðer en üst kattaysa, en alt kata geri dön.
                _currentSchoolFloor = SchoolFloor.TenthFloor;
            }
        }
        else
        {
            int previousFloorValue = (int)_currentSchoolFloor - 1;

            if (previousFloorValue >= (int)SchoolFloor.FirstFloor)
            {
                _currentSchoolFloor = (SchoolFloor)previousFloorValue;
            }
            else
            {
                // Eðer en alt kattaysa, en üst kata geç.
                _currentSchoolFloor = SchoolFloor.FirstFloor;
            }
        }
        currentFloorManager = GetDesiredSchoolManager(_currentSchoolFloor);       
    }
}
public enum SchoolFloor
{
    FirstFloor,
    SecondFloor,
    ThirdFloor,
    FourthFloor,
    FifthFloor,
    SixthFloor,
    SeventhFloor,
    EighthFloor,
    NinthFloor,
    TenthFloor
}
public enum GameEndType
{
    CatchingStaff,
    CaughtOnCamera,
    PuzzleTimeEnding,
    GameTimeEnding
}