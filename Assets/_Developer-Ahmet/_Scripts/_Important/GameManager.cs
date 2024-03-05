using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameEndType _gameEndType;
    private SchoolFloor _currentSchoolFloor;
    public List<SchoolFloorManager> _FloorManagers = new List<SchoolFloorManager>();
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
        PlayerManager.instance.PlayerUnlock();
        UIManager.instance.SetActivationMenuPanel(false);
        SetCursorLockMode(CursorLockMode.Locked);
        AudioManager.instance.StartGameSource();
    }
    public void GameOver(GameEndType _endType)
    {
        PlayerManager.instance.PlayerLock();
        _gameEndType = _endType;
        if (_gameEndType == GameEndType.CatchingStaff)
        {
            
        }
        else if (_gameEndType == GameEndType.CaughtOnCamera)
        {
            GetSchoolManager().AllFloorPersonnelsCatchThePlayer();
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
    CaughtOnCamera
}
