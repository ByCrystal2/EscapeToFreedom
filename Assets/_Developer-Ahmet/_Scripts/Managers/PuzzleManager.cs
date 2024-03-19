using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] public PuzzleBehaviour PuzzleBehaviour;
    [SerializeField] Transform MainStoryPanelContent;
    [SerializeField] GameObject MainStoryMissionPrefab;

    [SerializeField] Transform PuzzlePanelContent;
    [SerializeField] GameObject PuzzleMissionPrefab;

    public List<Puzzle> Puzzles = new List<Puzzle>();
    public List<GameMission> MainStoryMissions = new List<GameMission>();
    public List<MissionBehaviour> MainStoryBehaviours = new List<MissionBehaviour>();
    public List<MissionBehaviour> PuzzleMissionBehaviours = new List<MissionBehaviour>();

    private Puzzle _currentPuzzle;
    public Puzzle CurrentPuzzle 
    {   get 
        {
            if (_currentPuzzle != null)
            {
                return _currentPuzzle;
            }
            else
            {
                Debug.Log("Mevcut puzzel yok. (null)");
                return new Puzzle(10000,"NULL",9999,new List<GameMission>(),MissionLevel.None);
            }
           
        }
        set
        {
            if (Puzzles.Contains(value))
            {
                if (!PuzzleBehaviour.TimeEnding)
                {
                    _currentPuzzle = value;
                    UIManager.instance.SetActivationPuzzleMissionPanel(true);
                    PuzzleBehaviour.SetPuzzleUI(value,0);
                    Puzzles.Remove(_currentPuzzle);
                }
                else
                {
                    Debug.Log("Baska bir puzzle su an aktif.");
                }
            }
            else
                Debug.Log("Gonderilen puzzle ana puzzel'lar da yer almamakta.");            
            
        }
    }
    private GameMission CurrentMainStoryMission;

    public Queue<Coroutine> coroutineQueue = new Queue<Coroutine>();
    private List<Coroutine> missionCompletionCoroutines = new List<Coroutine>();

    public PersonnelBehaviour CurrentPuzzleOwningPersonel;
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
        // 3 gorev basarili bir sekilde yapildiktan sonra tuvallete ki kagidi bul gorevi geliyor.
        
        

        MainStoryMissions.Add(gm1);
        MainStoryMissions.Add(gm2);
        MainStoryMissions.Add(gm3);
        ClearMainStoryContent();
        ReloadMissionsInMaintStoryContent(); //

        List<GameMission> Puzzle1Missions = new List<GameMission>();

        GameMission pgm1 = new GameMission(1000, "puzzel1", "Yemekhanede ki anahtarý al!", MissionLevel.Medium);
        GameMission pgm2 = new GameMission(1001, "puzzel1", "Kütüphanede ki kitap al!", MissionLevel.Medium);
        GameMission pgm3 = new GameMission(1002, "puzzel1", "Kamera odasýný bul!", MissionLevel.Medium);
        Puzzle1Missions.Add(pgm1);
        Puzzle1Missions.Add(pgm2);
        Puzzle1Missions.Add(pgm3);
        Puzzle p1 = new Puzzle(0, "Görevi yap!", 350, Puzzle1Missions, MissionLevel.Easy);

        Puzzles.Add(p1);
        //GameMission gm1 = new GameMission(0, "Görev 1", "Envanterini Aç.", MissionLevel.Easy);
    }
    public MissionBehaviour DesiredMainStoryMissionBehaviour(int _id)
    {
        return MainStoryBehaviours.Where(x=> x.GetMyMission().ID == _id).FirstOrDefault();
    }
    public MissionBehaviour DesiredPuzzleMissionBehaviour(int _id)
    {
        return PuzzleMissionBehaviours.Where(x => x.GetMyMission().ID == _id).FirstOrDefault();
    }
    public void AddMissionInMainStoryContent(GameMission _mission)
    {
        MainStoryMissions.Add(_mission);        
        ReloadMissionsInMaintStoryContent();
        UIManager.instance.StartDOMoveMissionPanel();
    }
    public void ClearMainStoryContent()
    {
        int length = MainStoryPanelContent.childCount;
        for (int i = 0; i < length; i++)
        {
            if (MainStoryPanelContent.GetChild(i).TryGetComponent(out MissionBehaviour _mission))
            {
                Destroy(_mission.gameObject);
            }            
        }
    }
    public void ReloadMissionsInMaintStoryContent()
    {
        int length = MainStoryMissions.Count;
        for (int i = 0; i < length; i++)
        {
            GameObject _newMissionObj = Instantiate(MainStoryMissionPrefab, MainStoryPanelContent);
            MissionBehaviour behaviour = _newMissionObj.GetComponent<MissionBehaviour>();
            behaviour.SetMyMission(MainStoryMissions[i]);
            behaviour.SetBaseOptions();
            behaviour._missionType = MissionType.MainStory;
            MainStoryBehaviours.Add(behaviour);
        }
    }
    public void AddMissionsInCurrentPuzzleContent()
    {
        List<GameMission> allMissions = CurrentPuzzle.GetAllMissions();
        int length = allMissions.Count;
        for (int i = 0; i < length; i++)
        {
            GameObject _newMissionObj = Instantiate(PuzzleMissionPrefab, PuzzlePanelContent);
            MissionBehaviour behaviour = _newMissionObj.GetComponent<MissionBehaviour>();
            behaviour.SetMyMission(allMissions[i]);
            behaviour.SetBaseOptions();
            behaviour._missionType = MissionType.Puzzle;
            PuzzleMissionBehaviours.Add(behaviour);
        }
    }
    public void SetCurrentPuzzle(PersonnelBehaviour _personnel)
    {
        CurrentPuzzleOwningPersonel = _personnel;
        CurrentPuzzle = _personnel.Puzzle;
    }
    public void SetCurrentMainStoryMission(GameMission _mission)
    {
        CurrentMainStoryMission = _mission;
    }
    public Puzzle GetCurrentPuzzle()
    {
        return CurrentPuzzle;
    }
    public GameMission GetCurrentMainStoryMission()
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
                CurrentPuzzleOwningPersonel.MyMissionComplate();
                CurrentPuzzleOwningPersonel = null;
                Puzzles.Remove(_puzzle);
            }
        }
    }
    private int ComplatedMissionsCount;
    private void MainStoryMissionComplate(GameMission _mission)
    {
        if (MainStoryMissions.Contains(_mission))
        {
            if (_mission.GetIsComplate())
                Debug.Log("GameMission zaten tamamlanmis => " + _mission.Header);
            else
            {
                _mission.SetIsComplate(true);
                ComplatedMissionsCount++;
                MainStoryMissions.Remove(_mission);
                if (ComplatedMissionsCount == 3)
                {
                    GameMission gm4 = new GameMission(2000, "Ana Görev 4", "Tuvallete ki notu bul.", MissionLevel.Easy);
                    AddMissionInMainStoryContent(gm4);
                }
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
        if (_missionBehaviour._missionType == MissionType.MainStory)
        {
            MainStoryMissionComplate(_missionBehaviour.GetMyMission());
        }
        else if (_missionBehaviour._missionType == MissionType.Puzzle)
        {
            if (CurrentPuzzle.AllIsMissionsComplated())
            {
                PuzzleComplate(CurrentPuzzle);                
            }
        }
        
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
        if (MainStoryPanelContent.childCount >= 10)
        {
            int length1 = MainStoryPanelContent.childCount;
            for (int i = 0; i < length1; i++)
            {
                if (MainStoryPanelContent.GetChild(i).TryGetComponent(out MissionBehaviour _mission))
                {
                    if (_behaviour != _mission)
                    {
                        Destroy(_mission.gameObject);
                    }
                }
            }
        }
        StartCoroutine(WaitForZero());
        
    }
    IEnumerator WaitForZero()
    {
        while (coroutineQueue.Count > 0)
        {
            yield return null;
        }
        Tween _endMoveT = UIManager.instance.EndDOMoveMissionPanel();
    }
    public Puzzle GetRandomPuzzle()
    {
        return Puzzles[Random.Range(0,Puzzles.Count)];
    }
}
public enum MissionType
{
    MainStory,
    Puzzle
}