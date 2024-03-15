using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{    
    [SerializeField] Transform MainStoryPanelContent;
    [SerializeField] GameObject MainStoryMissionPrefab;
    public List<Puzzle> Puzzles = new List<Puzzle>();
    public List<GameMission> MainStoryMissions = new List<GameMission>();
    public List<MissionBehaviour> MainStoryBehaviours = new List<MissionBehaviour>();

    private Puzzle _currentPuzzle;
    public Puzzle CurrentPuzzle 
    {   get 
        { 
            return _currentPuzzle;
        }
        set
        {

        }
    }
    private GameMission CurrentMainStoryMission;

    public Queue<Coroutine> coroutineQueue = new Queue<Coroutine>();
    private List<Coroutine> missionCompletionCoroutines = new List<Coroutine>();

    [SerializeField] public S_MissionComplateController MissionComplateController;
    public static PuzzleManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        MissionComplateController.ResetComplateBools();
    }
    float _timer = 2f;
    float _currentTime = 2f;
    int _combo = 0;
    List<string> list = new List<string>() { "w", "a", "s", "d" };
    private void Update()
    {
        
        if (!MissionComplateController.GetIsMoveComplate())
        {
            string keyboardinput = Input.inputString;
            if (!string.IsNullOrEmpty(keyboardinput))
            {
                if (list.Contains(keyboardinput))
                {
                    list.Remove(keyboardinput);
                    _currentTime = _timer;
                    _combo++;
                    if (_combo >= 4)
                    {
                        MissionComplateController.MoveMissionComplate();
                        return;
                    }
                }
            }
            
            _currentTime -= Time.deltaTime;            
            if (_currentTime <= 0)
            {
                _combo = 0;
                _currentTime = _timer;
                //foreach (var item in list)
                //{
                //    Debug.Log("MoveListItem => " + item);
                //}
                list.Clear();
                list = new List<string>() { "w", "a", "s", "d" };
            }
        }
    }
    public void PuzzlesInit()
    {
        // UYARI! IDLER DEGISTIRILMEMELI!!!!
        GameMission gm1 = new GameMission(1, "Ana Görev 1", "Hareket Et. (W-A-S-D)", MissionLevel.Easy);
        GameMission gm2 = new GameMission(2, "Ana Görev 2", "Saða-Sola Bak. (Mouse Rotate)", MissionLevel.Easy);
        GameMission gm3 = new GameMission(3, "Ana Görev 3", "Envanterini Aç. (I)", MissionLevel.Easy);

        GameMission gm4 = new GameMission(4, "Ana Görev 4", "Test1", MissionLevel.Easy);
        GameMission gm5 = new GameMission(5, "Ana Görev 5", "Test2", MissionLevel.Easy);
        GameMission gm6 = new GameMission(6, "Ana Görev 6", "Test2", MissionLevel.Easy);

        MainStoryMissions.Add(gm1);
        MainStoryMissions.Add(gm2);
        MainStoryMissions.Add(gm3);
        MainStoryMissions.Add(gm4);
        MainStoryMissions.Add(gm5);
        MainStoryMissions.Add(gm6);
        AddMissionsInMaintStoryContent(); //

        List<GameMission> Puzzle1Missions = new List<GameMission>();
        //GameMission gm1 = new GameMission(0, "Görev 1", "Envanterini Aç.", MissionLevel.Easy);
    }
    public MissionBehaviour DesiredMissionBehaviour(int _id)
    {
        return MainStoryBehaviours.Where(x=> x.GetMyMission().ID == _id).FirstOrDefault();
    }
    public void AddMissionsInMaintStoryContent()
    {
        int length = MainStoryMissions.Count;
        for (int i = 0; i < length; i++)
        {
            GameObject _newMissionObj = Instantiate(MainStoryMissionPrefab, MainStoryPanelContent);
            MissionBehaviour behaviour = _newMissionObj.GetComponent<MissionBehaviour>();
            behaviour.SetMyMission(MainStoryMissions[i]);
            MainStoryBehaviours.Add(behaviour);
        }
    }
    public void SetCurrentPuzzle(Puzzle _puzzle)
    {
        CurrentPuzzle = _puzzle;
    }
    public void SetCurrentMainStoryMission(GameMission _mission)
    {
        CurrentMainStoryMission = _mission;
    }
    public Puzzle GetCurrentPuzzle()
    {
        return CurrentPuzzle;
    }
    public GameMission GetCurrentAminStoryMission()
    {
        return CurrentMainStoryMission;
    }
    public void PuzzleComplate(Puzzle _puzzle)
    {
        if (Puzzles.Contains(_puzzle))
        {
            if (_puzzle.GetIsComplate())
                Debug.Log("Puzzle zaten tamamlanmis => " + _puzzle.Header);
            else
            {
                _puzzle.SetIsComplate(true);
                Puzzles.Remove(_puzzle);
            }
        }
    }
    private void MainStoryMissionComplate(GameMission _mission)
    {
        if (MainStoryMissions.Contains(_mission))
        {
            if (_mission.GetIsComplate())
                Debug.Log("GameMission zaten tamamlanmis => " + _mission.Header);
            else
            {
                _mission.SetIsComplate(true);
                MainStoryMissions.Remove(_mission);
            }
        }
    }
    public void AddMissionCoroutine(Coroutine newCoroutine)
    {
        // Eðer kuyrukta Coroutine yoksa, gelen Coroutine'i hemen çalýþtýr
        if (coroutineQueue.Count == 0)
        {
            Coroutine coroutine = StartCoroutine(WaitForCoroutine(newCoroutine));
            coroutineQueue.Enqueue(coroutine);
        }
        else
        {
            // Eðer kuyrukta Coroutine varsa, sýraya ekle
            missionCompletionCoroutines.Add(newCoroutine);
        }
    }
    private IEnumerator WaitForCoroutine(Coroutine coroutine)
    {
        yield return coroutine;

        // Coroutine tamamlandýðýnda, kuyruktan bir sonraki Coroutine'i alýp çalýþtýr
        if (coroutineQueue.Count > 0)
        {
            Coroutine nextCoroutine = coroutineQueue.Dequeue();
            yield return StartCoroutine(WaitForCoroutine(nextCoroutine));
        }
        else
        {
            // Eðer kuyruk boþsa ve diðer Coroutine'ler bekliyorsa onlarý çalýþtýr
            if (missionCompletionCoroutines.Count > 0)
            {
                Coroutine nextCoroutine = missionCompletionCoroutines[0];
                missionCompletionCoroutines.RemoveAt(0);
                Coroutine newCoroutine = StartCoroutine(WaitForCoroutine(nextCoroutine));
                coroutineQueue.Enqueue(newCoroutine);
            }
        }
    }
    public void MissionCompateManipulation(MissionBehaviour _missionBehaviour)
    {
        SendMission(_missionBehaviour);        
    }
    private void SendMission(MissionBehaviour _missionBehaviour)
    {
        MainStoryMissionComplate(_missionBehaviour.GetMyMission());
        StartCoroutine(SmoothMissionRowDown(_missionBehaviour));
    }

    IEnumerator SmoothMissionRowDown(MissionBehaviour _behaviour)
    {
        Vector3 _startPosition = _behaviour.gameObject.transform.position;
        int length = MainStoryPanelContent.childCount;
        float speed = 1f;
        float speedStrength = 5f;

        // Son elemaný taþýmak için son index'i alýyoruz
        int currentIndex = _behaviour.transform.GetSiblingIndex();
        List<MissionBehaviour> endPosMissions = new List<MissionBehaviour>();
        for (int i = currentIndex; i < length; i++)
        {
            Transform _currentChild = MainStoryPanelContent.GetChild(i).transform;
            if (_currentChild.TryGetComponent(out MissionBehaviour _b) && _b != _behaviour && !_b.GetMyMission().GetIsComplate())
            {
                int currentSiblingIndex = _behaviour.transform.GetSiblingIndex();
                int otherSiblingIndex = _b.transform.GetSiblingIndex();
                Vector3 _previousPosition = _behaviour.gameObject.transform.position;
                //_behaviour.gameObject.transform.position = _currentChild.position;
                _behaviour.transform.SetSiblingIndex(otherSiblingIndex);
                yield return new WaitForEndOfFrame();
                //_currentChild.transform.position = _previousPosition;
                _b.transform.SetSiblingIndex(currentSiblingIndex);


                speed = speed - (Time.deltaTime * speedStrength); // Zamana göre hýzý azalt
                yield return new WaitForSeconds(speed);
            }
        }
        Tween _endMoveT = UIManager.instance.EndDOMoveMissionPanel();
        _endMoveT.OnUpdate(() =>
        {
            if (coroutineQueue.Count > 0)
            {
                UIManager.instance.StartDOMoveMissionPanel();
            }            
        });
    }
    IEnumerator WaitForZero()
    {
        while (coroutineQueue.Count > 0)
        {
            yield return null;
        }
        
    }
}