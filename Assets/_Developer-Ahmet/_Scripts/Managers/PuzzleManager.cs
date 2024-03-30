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
        GameMission gm1 = new GameMission(1, "Ana G�rev 1", "Hareket Et. (W-A-S-D)", MissionLevel.Easy);
        GameMission gm2 = new GameMission(2, "Ana G�rev 2", "Sa�a-Sola Bak. (Mouse Rotate)", MissionLevel.Easy);
        GameMission gm3 = new GameMission(3, "Ana G�rev 3", "Envanterini A�. (I)", MissionLevel.Easy);
        // 3 gorev basarili bir sekilde yapildiktan sonra tuvallete ki kagidi bul gorevi geliyor.
        
        MainStoryMissions.Add(gm1);
        MainStoryMissions.Add(gm2);
        MainStoryMissions.Add(gm3);
        ClearMainStoryContent();
        ReloadMissionsInMaintStoryContent(); //

        List<GameMission> Puzzle1Missions = new List<GameMission>();

        GameMission pgm1 = new GameMission(1000, "puzzel1", "Yemekhanede ki anahtar� al!", MissionLevel.Medium);
        GameMission pgm2 = new GameMission(1001, "puzzel1", "K�t�phanede ki kitap al!", MissionLevel.Medium);
        GameMission pgm3 = new GameMission(1002, "puzzel1", "Kamera odas�n� bul!", MissionLevel.Medium);
        Puzzle1Missions.Add(pgm1);
        Puzzle1Missions.Add(pgm2);
        Puzzle1Missions.Add(pgm3);
        Puzzle p1 = new Puzzle(0, "G�revi yap!", 350, Puzzle1Missions, MissionLevel.Easy);

        Puzzles.Add(p1);
        //GameMission gm1 = new GameMission(0, "G�rev 1", "Envanterini A�.", MissionLevel.Easy);
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
    private int ComplatedMainStoryMissionsCount;
    private void MainStoryMissionComplate(GameMission _mission)
    {
        if (MainStoryMissions.Contains(_mission))
        {
            if (_mission.GetIsComplate())
                Debug.Log("GameMission zaten tamamlanmis => " + _mission.Header);
            else
            {
                _mission.SetIsComplate(true);
                ComplatedMainStoryMissionsCount++;
                MainStoryMissions.Remove(_mission);
                ComplatedMainStoryControlAndAddMission(ComplatedMainStoryMissionsCount);
            }
        }
    }
    private void ComplatedMainStoryControlAndAddMission(int _id)
    {
        switch (_id)
        {
            case 3:
                GameMission gm4 = new GameMission(2000, "Ana G�rev 1", "Tuvallete ki notu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm4);
                MissionsManager.instance.ActivationDesiredMainStoryMission(0);
                break;
            case 4:
                GameMission gm34 = new GameMission(2030, "Ana G�rev 1", "Konu�an Klozeti bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm34);
                break;
            case 5:
                GameMission gm5 = new GameMission(2001, "Ana G�rev 2", "K�t�phanede ki g�venlik anahtar�n� bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm5);
                MissionsManager.instance.ActivationDesiredMainStoryMission(1);
                break;
            case 6:
                GameMission gm6 = new GameMission(2002, "Ana G�rev 2", "Konu�an Klozeti bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm6);
                break;
            case 7:
                GameMission gm7 = new GameMission(2003, "Ana G�rev 2", "Kamera odas�nda ki personel odas� anahtar�n� al.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm7);
                MissionsManager.instance.ActivationDesiredMainStoryMission(2);
                break;
            case 8:
                GameMission gm8 = new GameMission(2004, "Ana G�rev 2", "Personel odas�na git ve kap� anahtar�n� bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm8);
                MissionsManager.instance.ActivationDesiredMainStoryMission(3);
                break;            
            case 9:
                GameMission gm9 = new GameMission(2005, "Ana G�rev 2", "9. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm9);
                break;
            case 10:
                GameMission gm35 = new GameMission(2031, "Ana G�rev 2", "Arkada��n�n notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm35);
                MissionsManager.instance.ActivationDesiredMainStoryMission(4);
                break;
            case 11:
                GameMission gm10 = new GameMission(2006, "Ana G�rev 2", "3. Koridorda ki Depoyu bul", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm10);
                break;
            case 12:
                GameMission gm11 = new GameMission(2007, "Ana G�rev 2", "2. Koridorda G�VENL�K ANAHTARI'n� bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm11);
                // NOT: Bu anahtar 2. koridorda ki locker icerisinde bulunmaktadir.
                break;
            case 13:
                GameMission gm12 = new GameMission(2008, "Ana G�rev 2", "8. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm12);
                break;
            case 14:
                GameMission gm13 = new GameMission(2009, "Ana G�rev 2", "Konu�an Klozet'i bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm13);
                break;
            case 15:
                GameMission gm14 = new GameMission(2010, "Ana G�rev 2", "Garip odada ki anahtar� bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm14);
                //NOT: Bu anahtar mouse roomda ki locker icerisinde bulunmaktadir.
                break;
            case 16:
                GameMission gm15 = new GameMission(2011, "Ana G�rev 2", "7. Kata in", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm15);
                break;
            case 17:
                GameMission gm16 = new GameMission(2012, "Ana G�rev 2", "Arkada��n�n notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm16);
                MissionsManager.instance.ActivationDesiredMainStoryMission(5);
                break;
            case 18:
                GameMission gm17 = new GameMission(2013, "Ana G�rev 2", "6. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm17);
                break;
            case 19:
                GameMission gm18 = new GameMission(2014, "Ana G�rev 2", "Konu�an Klozet'i bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm18);
                break;
            case 20:
                GameMission gm19 = new GameMission(2015, "Ana G�rev 2", "Depo'da ki arkada��n�n notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm19);
                MissionsManager.instance.ActivationDesiredMainStoryMission(6);
                break;
            case 21:
                GameMission gm20 = new GameMission(2016, "Ana G�rev 2", "Konu�an Klozet'i bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm20);
                break;
            case 22:
                GameMission gm21 = new GameMission(2017, "Ana G�rev 2", "G�venlik Anahtar�'n� bul #1.", MissionLevel.Easy);
                GameMission gm22 = new GameMission(2018, "Ana G�rev 2", "G�venlik Anahtar�'n� bul #2.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm21);
                AddMissionInMainStoryContent(gm22);
                MissionsManager.instance.ActivationDesiredMainStoryMission(7);
                break;
            case 23:
                GameMission gm36 = new GameMission(2032, "Ana G�rev 2", "5. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm36);
                break;
            case 24:
                GameMission gm37= new GameMission(2033, "Ana G�rev 2", "Arkada��n�n notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm37);
                MissionsManager.instance.ActivationDesiredMainStoryMission(8);
                break;
            case 25:
                GameMission gm38 = new GameMission(2034, "Ana G�rev 2", "4. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm38);
                break;
            case 26:
                GameMission gm23 = new GameMission(2019, "Ana G�rev 2", "Konu�an Klozet'i bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm23);
                break;
            case 27:
                GameMission gm24 = new GameMission(2020, "Ana G�rev 2", "Arkada��n�n s�n�f�na git ve notunu bul", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm24);
                MissionsManager.instance.ActivationDesiredMainStoryMission(9);                
                break;
            case 28:
                GameMission gm25 = new GameMission(2021, "Ana G�rev 2", "G�venlik Anahtar�'n� bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm25);
                MissionsManager.instance.ActivationDesiredMainStoryMission(10);
                break;
            case 29:
                GameMission gm26 = new GameMission(2022, "Ana G�rev 2", "3. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm26);
                break;
            case 30:
                GameMission gm27 = new GameMission(2023, "Ana G�rev 2", "Arkada��n�n notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm27);
                MissionsManager.instance.ActivationDesiredMainStoryMission(11);
                break;
            case 31:
                GameMission gm28 = new GameMission(2024, "Ana G�rev 2", "G�venlik anahtar�n� bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm28);
                MissionsManager.instance.ActivationDesiredMainStoryMission(12);
                break;
            case 32:
                GameMission gm29 = new GameMission(2025, "Ana G�rev 2", "2. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm29);
                break;
            case 33:
                GameMission gm39 = new GameMission(2035, "Ana G�rev 2", "Konu�an Klozeti bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm39);
                break;
            case 34:
                GameMission gm30 = new GameMission(2026, "Ana G�rev 2", "Arkada��n�n notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm30);
                MissionsManager.instance.ActivationDesiredMainStoryMission(13);
                break;
            case 35:
                GameMission gm31 = new GameMission(2027, "Ana G�rev 2", "G�venlik anahtar�n� bul. #1", MissionLevel.Easy);
                GameMission gm32 = new GameMission(2028, "Ana G�rev 2", "G�venlik anahtar�n� bul. #2", MissionLevel.Easy);                
                AddMissionInMainStoryContent(gm31);
                AddMissionInMainStoryContent(gm32);
                MissionsManager.instance.ActivationDesiredMainStoryMission(14);
                break;
            case 36:
                GameMission gm40 = new GameMission(2036, "Ana G�rev 2", "1. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm40);
                MissionsManager.instance.ActivationDesiredMainStoryMission(15);
                break;
            default:
                break;
        }
       
    }
    public void AddMissionCoroutine(Coroutine newCoroutine)
    {
        // E�er kuyrukta Coroutine yoksa, gelen Coroutine'i hemen �al��t�r
        if (coroutineQueue.Count == 0)
        {
            Coroutine coroutine = StartCoroutine(WaitForCoroutine(newCoroutine));
            coroutineQueue.Enqueue(coroutine);
        }
        else
        {
            // E�er kuyrukta Coroutine varsa, s�raya ekle
            missionCompletionCoroutines.Add(newCoroutine);
        }
    }
    private IEnumerator WaitForCoroutine(Coroutine coroutine)
    {
        yield return coroutine;

        // Coroutine tamamland���nda, kuyruktan bir sonraki Coroutine'i al�p �al��t�r
        if (coroutineQueue.Count > 0)
        {
            Coroutine nextCoroutine = coroutineQueue.Dequeue();
            yield return StartCoroutine(WaitForCoroutine(nextCoroutine));
        }
        else
        {
            // E�er kuyruk bo�sa ve di�er Coroutine'ler bekliyorsa onlar� �al��t�r
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

        // Son eleman� ta��mak i�in son index'i al�yoruz
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


                speed = speed - (Time.deltaTime * speedStrength); // Zamana g�re h�z� azalt
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