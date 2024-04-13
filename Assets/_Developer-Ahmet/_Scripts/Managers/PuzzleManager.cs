using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                return new Puzzle(10000,"NULL",9999,new List<GameMission>(),MissionLevel.None, SchoolFloor.FirstFloor);
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
    public void AddToiletSpeakingMissionIds()
    {
        MissionComplateController.SetToiletSpeakingIds(new List<int>
        {
            // ComplatedMainStoryControlAndAddMission => klozeti bul missions
            2500,2501,2502,2503,2504,2505,2506
        });
    }
    public void PuzzlesInit()
    {
        // UYARI! IDLER DEGISTIRILMEMELI!!!!
        GameMission gm1 = new GameMission(1, "Ana Görev 1", "Hareket Et. (W-A-S-D)", MissionLevel.Easy);
        GameMission gm2 = new GameMission(2, "Ana Görev 2", "Saða-Sola Bak. (Mouse Rotate)", MissionLevel.Easy);
        GameMission gm3 = new GameMission(3, "Ana Görev 3", "Envanterini Aç. (I)", MissionLevel.Easy);
        GameMission gm4 = new GameMission(4, "Ana Görev 4", "MainStroy Panelini Getir.(U)", MissionLevel.Easy);
        // 3 gorev basarili bir sekilde yapildiktan sonra tuvallete ki kagidi bul gorevi geliyor.
        
        MainStoryMissions.Add(gm1);
        MainStoryMissions.Add(gm2);
        MainStoryMissions.Add(gm3);
        MainStoryMissions.Add(gm4);
        ClearMainStoryContent();
        ReloadMissionsInMaintStoryContent(); //
        AddToiletSpeakingMissionIds();

        //PuzzleMission

        //p-1
        List<GameMission> Puzzle1Missions = new List<GameMission>();
        GameMission pgm1 = new GameMission(1000, "10. Kat Gorevi", "Kýzlar tuvaletinde ki mantarý al!", MissionLevel.Medium);
        GameMission pgm2 = new GameMission(1001, "10. Kat Gorevi", "Kütüphanede ki mantarý al!", MissionLevel.Medium);
        GameMission pgm3 = new GameMission(1002, "10. Kat Gorevi", "Bir lockerin yanýnda bulunan mantarý al!", MissionLevel.Medium);
        Puzzle1Missions.Add(pgm1);
        Puzzle1Missions.Add(pgm2);
        Puzzle1Missions.Add(pgm3);        
        Puzzle p1 = new Puzzle(0, "Mantarlarý Topla", 350, Puzzle1Missions, MissionLevel.Easy,SchoolFloor.TenthFloor);
        Puzzles.Add(p1);

        //p-2
        List<GameMission> Puzzle1Missions1 = new List<GameMission>();
        GameMission pgm4 = new GameMission(1003, "9. Kat Gorevi", "Erkekler tuvaletinde ki lavantayý al!", MissionLevel.Medium);
        GameMission pgm5 = new GameMission(1004, "9. Kat Gorevi", "Depoda ki lavantalarý bul!", MissionLevel.Medium);
        GameMission pgm6 = new GameMission(1005, "9. Kat Gorevi", "Depoda ki mantarý al!", MissionLevel.Medium);
        Puzzle1Missions1.Add(pgm4);
        Puzzle1Missions1.Add(pgm5);
        Puzzle1Missions1.Add(pgm6);        
        Puzzle p2 = new Puzzle(1, "Lavanta kokusu", 350, Puzzle1Missions1, MissionLevel.Easy, SchoolFloor.NinthFloor);
        Puzzles.Add(p2);

        //p-3
        List<GameMission> Puzzle1Missions2 = new List<GameMission>();
        GameMission pgm7 = new GameMission(1006, "8. Kat Gorevi", "Üç yol noktasýnda ki kitabý al!", MissionLevel.Medium);
        GameMission pgm8 = new GameMission(1007, "8. Kat Gorevi", "Koridor köþesinde ki çiçeði al!", MissionLevel.Medium);
        GameMission pgm9 = new GameMission(1008, "8. Kat Gorevi", "Kat giriþinde ki mantarý al!", MissionLevel.Medium);
        Puzzle1Missions2.Add(pgm7);
        Puzzle1Missions2.Add(pgm8);
        Puzzle1Missions2.Add(pgm9);
        Puzzle p3 = new Puzzle(2, "Kitap kokusu", 350, Puzzle1Missions2, MissionLevel.Easy, SchoolFloor.EighthFloor);
        Puzzles.Add(p3);

        //p-4
        List<GameMission> Puzzle1Missions3 = new List<GameMission>();
        GameMission pgm10 = new GameMission(1009, "7. Kat Gorevi", "Üç yol noktasýnda ki kitabý al!", MissionLevel.Medium);
        GameMission pgm11 = new GameMission(1010, "7. Kat Gorevi", "Koridor köþesinde ki çiçeði al!", MissionLevel.Medium);
        GameMission pgm12 = new GameMission(1011, "7. Kat Gorevi", "Kat giriþinde ki mantarý al!", MissionLevel.Medium);
        Puzzle1Missions3.Add(pgm10);
        Puzzle1Missions3.Add(pgm11);
        Puzzle1Missions3.Add(pgm12);
        Puzzle p4 = new Puzzle(3, "Kitap kokusu", 350, Puzzle1Missions3, MissionLevel.Easy, SchoolFloor.SeventhFloor);
        Puzzles.Add(p4);
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
    public void ClearPuzzleContent()
    {
        int length = PuzzlePanelContent.childCount;
        for (int i = 0; i < length; i++)
        {
            if (PuzzlePanelContent.GetChild(i).TryGetComponent(out MissionBehaviour _mission))
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
        MissionsManager.instance.ActivationDesiredPuzzleMission(GameManager.instance.currentFloorManager.GetFloor()-1,_personnel.Puzzle.ID);
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
        if (!Puzzles.Contains(_puzzle))
        {
            if (_puzzle.GetIsComplate())
                Debug.Log("Puzzle zaten tamamlanmis => " + _puzzle.Header);
            else
            {
                _puzzle.SetIsComplate(true);
                CurrentPuzzleOwningPersonel.MyPuzzleComplate();
                CurrentPuzzleOwningPersonel = null;
                TimeManager.instance.StopPuzzleTimeCoroutine();
                UIManager.instance.EndDOMovePuzzlePanel();
                Puzzles.Remove(_puzzle);
                Debug.Log("Puzzle tamamlandi => " + _puzzle.Header);
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
            case 4:
                GameMission gm4 = new GameMission(2000, "Ana Görev 1", "Tuvallete ki notu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm4);
                MissionsManager.instance.ActivationDesiredMainStoryMission(0);
                break;
            case 5:
                GameMission gm34 = new GameMission(2500, "Ana Görev 1", "Konuþan Klozeti bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm34);
                GameManager.instance.SetAllToiletIsSepakingActivation(true);
                break;
            case 6:
                GameMission gm5 = new GameMission(2100, "Ana Görev 2", "Kütüphanede ki güvenlik anahtarýný bul.", MissionLevel.Easy);                
                AddMissionInMainStoryContent(gm5);
                MissionsManager.instance.ActivationDesiredMainStoryMission(1);
                break;
            case 7:
                GameMission gm42 = new GameMission(2001, "Ana Görev 2", "Arkadaþýnýn notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm42);
                MissionsManager.instance.ActivationDesiredMainStoryMission(2);
                break;
            case 8:
                GameMission gm6 = new GameMission(2501, "Ana Görev 2", "Konuþan Klozeti bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm6);
                GameManager.instance.SetAllToiletIsSepakingActivation(true);
                break;
            case 9:
                GameMission gm7 = new GameMission(4000, "Ana Görev 2", "Kamera odasýnda ki personel odasý anahtarýný al.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm7);
                MissionsManager.instance.ActivationDesiredMainStoryMission(3);
                break;
            case 10:
                GameMission gm8 = new GameMission(4001, "Ana Görev 2", "Personel odasýna git ve kapý anahtarýný bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm8);
                MissionsManager.instance.ActivationDesiredMainStoryMission(4);
                break;            
            case 11:
                GameMission gm9 = new GameMission(2800, "Ana Görev 2", "9. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm9);
                break;
            case 12:
                GameMission gm35 = new GameMission(2002, "Ana Görev 2", "Arkadaþýnýn notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm35);
                MissionsManager.instance.ActivationDesiredMainStoryMission(5);
                break;
            case 13:
                GameMission gm10 = new GameMission(3002, "Ana Görev 2", "3. Koridorda ki Depoyu bul", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm10);
                break;
            case 14:
                GameMission gm11 = new GameMission(2101, "Ana Görev 2", "2. Koridorda GÜVENLÝK ANAHTARI'ný bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm11);
                // NOT: Bu anahtar 2. koridorda ki locker icerisinde bulunmaktadir.
                break;
            case 15:
                GameMission gm12 = new GameMission(2801, "Ana Görev 2", "8. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm12);
                break;
            case 16:
                GameMission gm13 = new GameMission(2502, "Ana Görev 2", "Konuþan Klozet'i bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm13);
                GameManager.instance.SetAllToiletIsSepakingActivation(true);
                break;
            case 17:
                GameMission gm14 = new GameMission(2102, "Ana Görev 2", "Garip odada ki anahtarý bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm14);
                //NOT: Bu anahtar mouse roomda ki locker icerisinde bulunmaktadir.
                break;
            case 18:
                GameMission gm15 = new GameMission(2802, "Ana Görev 2", "7. Kata in", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm15);
                break;
            case 19:
                GameMission gm16 = new GameMission(2003, "Ana Görev 2", "Arkadaþýnýn notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm16);
                MissionsManager.instance.ActivationDesiredMainStoryMission(6);
                break;
            case 20:
                GameMission gm17 = new GameMission(2803, "Ana Görev 2", "6. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm17);
                break;
            case 21:
                GameMission gm18 = new GameMission(2503, "Ana Görev 2", "Konuþan Klozet'i bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm18);
                GameManager.instance.SetAllToiletIsSepakingActivation(true);
                break;
            case 22:
                GameMission gm19 = new GameMission(2004, "Ana Görev 2", "Depo'da ki arkadaþýnýn notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm19);
                MissionsManager.instance.ActivationDesiredMainStoryMission(7);
                break;
            case 23:
                GameMission gm20 = new GameMission(2504, "Ana Görev 2", "Konuþan Klozet'i bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm20);
                GameManager.instance.SetAllToiletIsSepakingActivation(true);
                break;
            case 24:
                GameMission gm21 = new GameMission(2103, "Ana Görev 2", "Güvenlik Anahtarý'ný bul #1.", MissionLevel.Easy);
                GameMission gm22 = new GameMission(2104, "Ana Görev 2", "Güvenlik Anahtarý'ný bul #2.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm21);
                AddMissionInMainStoryContent(gm22);
                MissionsManager.instance.ActivationDesiredMainStoryMission(8);
                break;
            case 25:
                GameMission gm36 = new GameMission(2804, "Ana Görev 2", "5. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm36);
                break;
            case 26:
                GameMission gm37= new GameMission(2005, "Ana Görev 2", "Arkadaþýnýn notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm37);
                MissionsManager.instance.ActivationDesiredMainStoryMission(9);
                break;
            case 27:
                GameMission gm38 = new GameMission(2805, "Ana Görev 2", "4. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm38);
                break;
            case 28:
                GameMission gm23 = new GameMission(2505, "Ana Görev 2", "Konuþan Klozet'i bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm23);
                GameManager.instance.SetAllToiletIsSepakingActivation(true);
                break;
            case 29:
                GameMission gm24 = new GameMission(2006, "Ana Görev 2", "Arkadaþýnýn sýnýfýna git ve notunu bul", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm24);
                MissionsManager.instance.ActivationDesiredMainStoryMission(10);                
                break;
            case 30:
                GameMission gm25 = new GameMission(2105, "Ana Görev 2", "Güvenlik Anahtarý'ný bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm25);
                MissionsManager.instance.ActivationDesiredMainStoryMission(11);
                break;
            case 31:
                GameMission gm26 = new GameMission(2806, "Ana Görev 2", "3. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm26);
                break;
            case 32:
                GameMission gm27 = new GameMission(2007, "Ana Görev 2", "Arkadaþýnýn notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm27);
                MissionsManager.instance.ActivationDesiredMainStoryMission(12);
                break;
            case 33:
                GameMission gm28 = new GameMission(2106, "Ana Görev 2", "Güvenlik anahtarýný bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm28);
                MissionsManager.instance.ActivationDesiredMainStoryMission(13);
                break;
            case 34:
                GameMission gm29 = new GameMission(2807, "Ana Görev 2", "2. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm29);
                break;
            case 35:
                GameMission gm39 = new GameMission(2506, "Ana Görev 2", "Konuþan Klozeti bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm39);
                GameManager.instance.SetAllToiletIsSepakingActivation(true);
                break;
            case 36:
                GameMission gm30 = new GameMission(2008, "Ana Görev 2", "Arkadaþýnýn notunu bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm30);
                MissionsManager.instance.ActivationDesiredMainStoryMission(14);
                break;
            case 37:
                GameMission gm31 = new GameMission(2107, "Ana Görev 2", "Güvenlik anahtarýný bul. #1", MissionLevel.Easy);
                GameMission gm32 = new GameMission(2108, "Ana Görev 2", "Güvenlik anahtarýný bul. #2", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm31);
                AddMissionInMainStoryContent(gm32);
                MissionsManager.instance.ActivationDesiredMainStoryMission(15);
                break;
            case 38:
                GameMission gm40 = new GameMission(2808, "Ana Görev 2", "1. Kata in.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm40);                
                break;
            case 39:
                GameMission gm43 = new GameMission(2009, "Ana Görev 2", "Arkadaþýnýn notunu al.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm43);
                MissionsManager.instance.ActivationDesiredMainStoryMission(16);
                break;
            case 40:
                GameMission gm41 = new GameMission(2109, "Ana Görev 2", "Güvenlik anahtarýný bul.", MissionLevel.Easy);
                AddMissionInMainStoryContent(gm41);
                MissionsManager.instance.ActivationDesiredMainStoryMission(17);
                break;
            default:
                break;
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
            StartCoroutine(SmoothMissionRowDown(_missionBehaviour));
        }
        else if (_missionBehaviour._missionType == MissionType.Puzzle)
        {
            if (CurrentPuzzle.AllIsMissionsComplated())
            {
                if (CurrentPuzzle.GetIsDelivered())
                    PuzzleComplate(CurrentPuzzle);
                else
                {// tum gorevler eklendikten sonra, teslim et gorevini ekle.
                    ClearPuzzleContent();
                    GameMission deliveredMission = new GameMission(7777, "Puzzle Complated", "Ýtemleri personele teslim et!",MissionLevel.Easy);
                    CurrentPuzzle.AddMission(deliveredMission);
                    AddMissionsInCurrentPuzzleContent();
                    CurrentPuzzle.SetIsDelivered(true);
                }

            }
        }
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
            for (int i = MainStoryPanelContent.childCount / 2; i < length1; i++)
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
    //public IEnumerator SmoothPuzzleMissionDown(MissionBehaviour _behaviour)
    //{

    //}
    IEnumerator WaitForZero()
    {
        while (coroutineQueue.Count > 0)
        {
            yield return null;
        }
        Tween _endMoveT = UIManager.instance.EndDOMoveMissionPanel();
    }
    public Puzzle GetRandomPuzzle(SchoolFloor _currentFloor)
    {
        List<Puzzle> _targetPuzzles = Puzzles.Where(x => x.TargetFloor == _currentFloor).ToList();
        if (_targetPuzzles.Count > 0)
            return _targetPuzzles[Random.Range(0, _targetPuzzles.Count)];
        else
        {
            Debug.Log("Bu katta puzzle bulunmamaktadir. => " + _currentFloor);
            return null;
        }
        
    }
    public Puzzle GetRandomPuzzle(int _currentFloor)
    {
        List<Puzzle> _targetPuzzles = Puzzles.Where(x => (int)x.TargetFloor == _currentFloor).ToList();
        if (_targetPuzzles.Count > 0)
            return _targetPuzzles[Random.Range(0, _targetPuzzles.Count)];
        else
        {
            Debug.Log("Bu katta puzzle bulunmamaktadir. => " + _currentFloor);
            return null;
        }

    }
}
public enum MissionType
{
    MainStory,
    Puzzle
}