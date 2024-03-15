using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameMission
{
    public int ID;
    public string Header;
    public string Description;
    public MissionLevel Level;
    public float AnswerTime;

    private bool isCompleted;

    public GameMission(int _id, string _header, string _description, MissionLevel _level, float _answerTime = 0)
    {
        ID = _id;
        Header = _header;
        Description = _description;
        Level = _level;
        AnswerTime = _answerTime;
    }
    public bool GetIsComplate()
    {
        return isCompleted;
    }
    public void SetIsComplate(bool _isCompleted)
    {
        isCompleted = _isCompleted;
    }
}
public enum MissionLevel
{
    None,
    Easy,
    Medium,
    Hard
}
