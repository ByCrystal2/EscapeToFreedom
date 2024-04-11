using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class PersonnelBehaviour : MonoBehaviour
{
    Personnel Me;
    [SerializeField] int _id;
    [SerializeField] string _name;
    [SerializeField] EmployeeType _type;
    [SerializeField] List<Transform> _ToGoList = new List<Transform>();
    List<Transform> _defaultToGoList = new List<Transform>();
    public float PlayerTargetDistance = 8f;
    [HideInInspector]
    public NavMeshAgent _agent;
    public Animator anim;
    Transform _targetPosition;
    public TargetType _targetType;

    public Puzzle Puzzle;

    bool isCanCathPlayer = true;
    bool isBusy;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        _defaultToGoList.Clear();
        int length = _ToGoList.Count;
        for (int i = 0; i < length; i++)
        {
            _defaultToGoList.Add(_ToGoList[i]);
        }
        Me = new Personnel(_id, _name, _agent.speed, _type, this);        
    }
    private void Start()
    {        
        SetRandomGoTarget();
        StartCoroutine(WaitForPuzzleManager());
    }    
    IEnumerator WaitForPuzzleManager()
    {
        yield return new WaitUntil(() => PuzzleManager.instance != null);
        int random = Random.Range(0, 101);
        if (random <= 101) // BURASI %20 OLACAK! TEST AMACLÝ %100
        {
            Puzzle p = PuzzleManager.instance.GetRandomPuzzle(GetComponentInParent<SchoolFloorManager>().GetFloor());
            if (p != null)
            {
                Puzzle = p;
            }
        }
    }
    void Update()
    {
        float _playerTargetDistance = Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position);
        if (_playerTargetDistance <= PlayerTargetDistance && isCanCathPlayer)
        {
            Me.EmployeeState = EmployeeState.Chasing;
            anim.SetBool("Run", true);
            _targetPosition = PlayerManager.instance.player.transform;
            _targetType = TargetType.Player;
            Me.GoToTarget(_targetPosition);
            return;
        }
        else
        {
            if (Me.EmployeeState == EmployeeState.Waiting) return;            
            Me.EmployeeState = EmployeeState.Pacing;
            anim.SetBool("Run", false);
            if (_targetType == TargetType.Player)
            {
                _targetType = TargetType.Pacing;
                SetRandomGoTarget();
            }
        }
        if (_ToGoList.Count > 0)
        {
            if (_targetPosition != null)
            {
                _targetType = TargetType.Pacing;
                Me.GoToTarget(_targetPosition);
            }
        }
    }
    
    public void SetRandomGoTarget()
    {
        _targetPosition = GetRandomGoTarget();
        _ToGoList.Remove(_targetPosition);
        if (_ToGoList.Count <= 0)
        {
            _ToGoList.Clear();
            int length = _defaultToGoList.Count;
            for (int i = 0; i < length; i++)
            {
                _ToGoList.Add(_defaultToGoList[i]);
            }
            _targetPosition = GetRandomGoTarget();
            return;
        }       
    }
    public void StartWaitForIdle()
    {
        StartCoroutine(WaitForIdle());
    }
    private IEnumerator WaitForIdle()
    {
        int random = Random.Range(1, 101);
        if (random <= 30)
        {
            anim.SetInteger("Work", Random.Range(1, 3));
        }
        else
        {
            anim.SetInteger("Idle", Random.Range(1, 3));
        }
        int randomWaitTime = Random.Range(3, 9);
        for (int i = 0; i < randomWaitTime; i++)
        {
            if (_targetType == TargetType.Player)
                break;
            yield return new WaitForSeconds(1);
        }
        if (_targetType == TargetType.Player)
        {
            Me.EmployeeState = EmployeeState.Chasing;
            anim.SetBool("Run", true);
        }
        Me.EmployeeState = EmployeeState.Pacing;
        anim.SetBool("Walk", true);
        SetRandomGoTarget();
    }
    public void MyPuzzleComplate()
    {
        Puzzle = null;        
    }
    public bool IsHaveAnPuzzle()
    {
        return Puzzle != null;
    }
    public bool GetIsCanCatchPlayer()
    {
        return isCanCathPlayer; 
    }
    public bool GetIsBusy()
    {
        return isBusy;
    }
    public void SetIsBusy(bool _isBusy)
    {
        isBusy = _isBusy;
    }
    public void SetIsCanCatchPlayer(bool _catch)
    {
        isCanCathPlayer = _catch;
    }
    public void KillMe()
    {
        Destroy(gameObject);
    }
    public Personnel GetMe()
    {
        return Me;
    }
    private Transform GetRandomGoTarget()
    {
        return _ToGoList[Random.Range(0, _ToGoList.Count)];
    }
    public void InteractPersonnel()
    {
        UIManager.instance.SetActivationCatchThePlayerPanel(false);
        if (isBusy)
        {
            if (UIManager.instance.GetInteractPanelActive())
            {
                UIManager.instance.InteractPanelActivation(false);
            }
            return;
        }
        InteractPanelController.instance.SetCurrentInteractionPersonnel(this);
    }
    public void StartInteract()
    {
        PuzzleManager.instance.MissionComplateController.PuzzleAndMissionID(PuzzleManager.instance.CurrentPuzzle.ID, 7777);
    }
}
