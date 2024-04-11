using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class Puzzle
{
    public int ID;
    public string Header;
    public int TimeInterval;
    public MissionLevel Level;
    public SchoolFloor TargetFloor;
    public List<GameMission> Missions = new List<GameMission>();
    private bool isComplate;
    private bool isMissionDelivered;
    public Puzzle(int _id, string _header, int _timeInterval, List<GameMission> _puzzleMissions, MissionLevel _level, SchoolFloor _targetFloor)
    {
        ID = _id;
        Header = _header;
        TimeInterval = _timeInterval;
        Level = _level;
        Missions.Clear();
        int length = _puzzleMissions.Count;
        for (int i = 0; i < length; i++)
        {
            Missions.Add(_puzzleMissions[i]);
        }
        TargetFloor = _targetFloor;
        isMissionDelivered = false;
    }
    public bool GetIsDelivered()
    {
        return isMissionDelivered;
    }
    public bool GetIsComplate()
    {
        return isComplate;
    }
    public void SetIsDelivered(bool _isDelivered)
    {
        isMissionDelivered = _isDelivered;
    }
    public void SetIsComplate(bool _complate)
    {
        isComplate = _complate;
    }
    public void AddMission(GameMission _mission)
    {
        Missions.Add(_mission);
    }
    public List<GameMission> GetDesiredDifficultyMission(MissionLevel _level)
    {
        return Missions.Where(x=> x.Level == _level).ToList();
    }
    public List<GameMission> GetAllMissions()
    {
        return Missions;
    }
    public bool AllIsMissionsComplated()
    {
        int length = Missions.Count;
        bool allComplate = true;
        for (int i = 0; i < length; i++)
        {
            if (Missions[i].ID == 7777) break;
            if (!Missions[i].GetIsComplate())
            {
                allComplate = false;
            }
        }
        return allComplate;
    }
    public GameMission GetDesiredIDMisson(int _id)
    {
        return Missions.Where(x => x.ID == _id).SingleOrDefault();
    }
    public void MissionComplate(GameMission _mission)
    {
        if (Missions.Contains(_mission))
        {
            if (_mission.GetIsComplate())
                Debug.Log("GameMission zaten tamamlanmis => " + _mission.Header);
            else
            {
                _mission.SetIsComplate(true);
                PuzzleManager.instance.MissionComplateController.PuzzleAndMissionID(this.ID, _mission.ID);
                Debug.Log(_mission.Description + " Adli gorev basariyla yapildi.");
                Missions.Remove(_mission);
            }
        }        
    }
}
