using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    Transform _targetPosition;
    public TargetType _targetType;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
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
    }
    void Update()
    {
        float _playerTargetDistance = Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position);
        if (_playerTargetDistance <= PlayerTargetDistance)
        {
            _targetPosition = PlayerManager.instance.player.transform;
            _targetType = TargetType.Player;
            Me.GoToTarget(_targetPosition);
            return;
        }
        else
        {
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
        Debug.Log("_ToGoList.Count => " + _ToGoList.Count);
        _targetPosition = GetRandomGoTarget();
        _ToGoList.Remove(_targetPosition);
        if (_ToGoList.Count <= 0)
        {
            Debug.Log(_name + " Adli personelin her konuma gitti. Liste resetleniyor..");
            _ToGoList.Clear();
            int length = _defaultToGoList.Count;
            for (int i = 0; i < length; i++)
            {
                _ToGoList.Add(_defaultToGoList[i]);
                Debug.Log("Listeye " + _ToGoList[i].name + " Adli konum eklendi.");
            }
            _targetPosition = GetRandomGoTarget();
            return;
        }       
    }
    
    private Transform GetRandomGoTarget()
    {
        return _ToGoList[Random.Range(0, _ToGoList.Count)];
    }
}
