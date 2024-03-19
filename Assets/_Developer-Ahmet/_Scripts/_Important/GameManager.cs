using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameEndType _gameEndType;
    private SchoolFloor _currentSchoolFloor;
    public List<SchoolFloorManager> _FloorManagers = new List<SchoolFloorManager>();

    public GameObject CatchedPlayerPersonnelObj;
    public PersonnelBehaviour CurrentCathedPlayerPersonel;
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
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        PuzzleManager.instance.PuzzlesInit();
        AudioManager.instance.SourcesInit();
        List<SchoolFloorManager> list = FindObjectsOfType<SchoolFloorManager>().ToList();
        int length = list.Count;
        for (int i = 0; i < length; i++)
        {
            _FloorManagers.Add(list[i]);
        }
        _currentSchoolFloor = SchoolFloor.FirstFloor;
    } 
    public void SetCursorLockMode(CursorLockMode _lockMode)
    {
        Cursor.lockState = _lockMode;
        if (_lockMode == CursorLockMode.Locked)
            PlayerManager.instance.assetsInputs.cursorLocked = true;
        else
            PlayerManager.instance.assetsInputs.cursorLocked = false;
    }
    public void StartGame()
    {
        UIManager.instance.SetActivationMainMissionPanel(true);
        PlayerManager.instance.PlayerUnlock();
        UIManager.instance.SetActivationMenuPanel(false);
        SetCursorLockMode(CursorLockMode.Locked);
        AudioManager.instance.StartGameSource();
        TimeManager.instance.StartGameTime();
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
            GetSchoolManager().AllFloorPersonnelsCatchThePlayer();
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
        GetSchoolManager().AllFloorPersonnelsCatchThePlayer();
    }
    public SchoolFloorManager GetSchoolManager(SchoolFloor _floor)
    {
        return _FloorManagers[(int)_floor];
    }
    public SchoolFloorManager GetSchoolManager()
    {
        return _FloorManagers[(int)_currentSchoolFloor];
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