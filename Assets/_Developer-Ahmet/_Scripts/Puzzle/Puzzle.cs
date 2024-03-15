using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class Puzzle
{
    public int ID;
    public string Header;
    public float TimeInterval;
    public MissionLevel Level;
    public List<GameMission> Missions = new List<GameMission>();
    private bool isComplate;
    public Puzzle(int _id, string _header, float _timeInterval, List<GameMission> _puzzleMissions, MissionLevel _level)
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
    }
    public bool GetIsComplate()
    {
        return isComplate;
    }
    public void SetIsComplate(bool _complate)
    {
        isComplate = _complate;
    }
    public List<GameMission> GetDesiredDifficultyMission(MissionLevel _level)
    {
        return Missions.Where(x=> x.Level == _level).ToList();
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
                Missions.Remove(_mission);
            }
        }
    }
}
