using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : TaskAbstract
{
    public int ID;
    public string Name;
    public float Speed;
    public float BodyStrength;
    public EmployeeType EmployeeType;
    public EmployeeState EmployeeState;
    public Employee(int _id, string _name, float _speed, EmployeeType _type)
    {
        ID = _id;
        Name = _name;
        Speed = _speed;
        EmployeeType = _type;
    }
    
}
public class Personnel : Employee
{
    PersonnelBehaviour behaviour;

    public Personnel(int _id, string _name, float _speed, EmployeeType _type,PersonnelBehaviour _behaviour) : base(_id, _name, _speed, _type)
    {
        ID= _id;
        Name = _name;
        Speed = _speed;
        EmployeeType = _type;
        behaviour = _behaviour;
    }
    private float GetDistanceToTarget(Vector3 _targetPosition)
    {
        return Vector3.Distance(behaviour.transform.position, _targetPosition);
    }
    public void GoToTarget(Transform _target)
    {
        float _distance = GetDistanceToTarget(_target.position);
        if (behaviour._targetType == TargetType.Player)
        {
            behaviour._agent.speed = 3;
        }
        else if (behaviour._targetType == TargetType.Pacing)
        {
            behaviour._agent.speed = 2;
        }
        if (_distance <= 1.5f)
        {
            if (behaviour._targetType == TargetType.Player)
            {
                if (!UIManager.instance.GetCatchThePlayerPanelActive())
                {
                    Debug.Log("CathThePLayerPanel Aciliyor...");
                    AudioManager.instance.PlayPersonnelSound(true, PersonnelSoundType.ISeeYou);
                    UIManager.instance.SetActivationCatchThePlayerPanel(true, behaviour.Puzzle != null);
                    PlayerManager.instance.PlayerLock();
                    GameManager.instance.SetCursorLockMode(CursorLockMode.Confined);
                    GameManager.instance.GameOver(GameEndType.CatchingStaff, behaviour);
                    GameManager.instance.CurrentCatchedPersonnelWorkAndMoveOn(false);
                    return;
                }
            }
        }
        behaviour._agent.SetDestination(_target.position);
        if (_distance <= 0.5)
        {
            if (!(behaviour._targetType == TargetType.Player) && behaviour._targetType == TargetType.Pacing)
            {
                // personel Targete (pacing) geldi.
                EmployeeState = EmployeeState.Waiting;  
                behaviour.StartWaitForIdle();
            }            
        }
        
    }    
    
}
public interface IWalkable
{
    public void Walk();
}
public interface IRunable
{
    public void Run();
}
public abstract class TaskAbstract
{
    List<Mission> MyTasks = new List<Mission>();
    public void AddTask(Mission _task)
    {
        MyTasks.Add(_task);
    }
    public void RemoveTask(Mission _task)
    {
        MyTasks.Remove(_task);
    }
    public bool IsHasTask()
    {
        return MyTasks.Count > 0;
    }
    public bool IsHasThisTask(Mission _task)
    {
        return MyTasks.Contains(_task);
    }
}
public class Mission
{
    TaskType _taskType;
    Vector3 _targetPos;
    public Mission(TaskType taskType, Vector3 _targetPosition)
    {
        _taskType = taskType;
        _targetPos = _targetPosition;
    }
    public void StartTask()
    {

    }
}
public enum TaskType
{
    ChasePlayer,
    WillPacing
}
public enum EmployeeType
{
    None,
    Personnel,
    Security
}
public enum EmployeeState
{
    None,
    Pacing,
    Chasing,
    Waiting
}
public enum TargetType
{
    Player,
    Pacing
}
