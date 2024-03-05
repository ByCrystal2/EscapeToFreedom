using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : TaskAbstract
{
    public int ID;
    public string Name;
    public float Speed;
    public EmployeeType EmployeeType;
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
                GameManager.instance.GameOver(GameEndType.CatchingStaff);
                return;
            }
        }
        if (_distance <= 0.5)
        {
            if (!(behaviour._targetType == TargetType.Player) && behaviour._targetType == TargetType.Pacing)
            {
                behaviour.SetRandomGoTarget();
            }            
        }
        behaviour._agent.SetDestination(_target.position);
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
    List<Task> MyTasks = new List<Task>();
    public void AddTask(Task _task)
    {
        MyTasks.Add(_task);
    }
    public void RemoveTask(Task _task)
    {
        MyTasks.Remove(_task);
    }
    public bool IsHasTask()
    {
        return MyTasks.Count > 0;
    }
    public bool IsHasThisTask(Task _task)
    {
        return MyTasks.Contains(_task);
    }
}
public class Task
{
    TaskType _taskType;
    Vector3 _targetPos;
    public Task(TaskType taskType, Vector3 _targetPosition)
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
    Persomnel,
    Security
}
public enum TargetType
{
    Player,
    Pacing
}
