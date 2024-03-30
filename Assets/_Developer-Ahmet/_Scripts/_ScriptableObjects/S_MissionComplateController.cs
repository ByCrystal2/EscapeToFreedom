using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "S_MissionComplateController", menuName = "ScriptableObjects/S_MissionComplateController", order = 1)]
public partial class S_MissionComplateController : ScriptableObject // Main Story Missions
{
    private bool IsMoveComplate;
    private bool IsRotateComplate;
    private bool IsInventoryComplate;
    private bool[] FriendNoteComplate = { false, false, false, false, false, false, false, false, false, false };
    private bool[] SecurityKeyComplate = { false, false, false, false, false, false, false, false, false, false };
    private bool[] FloorEnterComplate = { false, false, false, false, false, false, false, false, false, false };
    private bool[] ClosetSpeakingComplate = { false, false, false, false, false, false, false, false, false, false };

    public void ResetComplateBools()
    {
        IsMoveComplate = false;
        IsRotateComplate = false;
        IsInventoryComplate = false;
        FriendNoteComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };
        SecurityKeyComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };
        FloorEnterComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };
        ClosetSpeakingComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };
}

    public void MoveMissionComplate()
    {
        if (PlayerManager.instance.player.IsBusy) return;
        IsMoveComplate = true;
        if (!PuzzleManager.instance.DesiredMainStoryMissionBehaviour(1).ComplateToggle.isOn)
        PuzzleManager.instance.DesiredMainStoryMissionBehaviour(1).ComplateToggle.isOn = true;
    }
    public void RotateMissionComplate()
    {
        if (PlayerManager.instance.player.IsBusy) return;
        IsRotateComplate = true;
        if (!PuzzleManager.instance.DesiredMainStoryMissionBehaviour(2).ComplateToggle.isOn)
            PuzzleManager.instance.DesiredMainStoryMissionBehaviour(2).ComplateToggle.isOn = true;
    }
    public void InventoryMissionComplate()
    {
        if (PlayerManager.instance.player.IsBusy) return;
        IsInventoryComplate = true;
        if (!PuzzleManager.instance.DesiredMainStoryMissionBehaviour(3).ComplateToggle.isOn)
            PuzzleManager.instance.DesiredMainStoryMissionBehaviour(3).ComplateToggle.isOn = true;
    }
    public void MainStoryMultipleMissionComplate(int _whichNumber, int _missionId, ComplateType _complateType)
    {
        if (PlayerManager.instance.player.IsBusy) return;
        switch (_complateType)
        {
            case ComplateType.FriendNoteComplate:
                if (FriendNoteComplate[_whichNumber])
                {
                    Debug.Log("Gorev zaten yapildi.");
                    return;
                }
                FriendNoteComplate[_whichNumber] = true;
                break;
            case ComplateType.SecurityKeyComplate:
                if (SecurityKeyComplate[_whichNumber])
                {
                    Debug.Log("Gorev zaten yapildi.");
                    return;
                }
                SecurityKeyComplate[_whichNumber] = true;
                break;
            case ComplateType.FloorEnterComplate:
                if (FloorEnterComplate[_whichNumber])
                {
                    Debug.Log("Gorev zaten yapildi.");
                    return;
                }
                FloorEnterComplate[_whichNumber] = true;
                break;
            case ComplateType.ClosetSpeakingComplate:
                if (ClosetSpeakingComplate[_whichNumber])
                {
                    Debug.Log("Gorev zaten yapildi.");
                    return;
                }
                ClosetSpeakingComplate[_whichNumber] = true;
                break;
            default:
                break;
        }
        
        if (!PuzzleManager.instance.DesiredMainStoryMissionBehaviour(_missionId).ComplateToggle.isOn)
            PuzzleManager.instance.DesiredMainStoryMissionBehaviour(_missionId).ComplateToggle.isOn = true;
    }    
    
    public bool GetIsMoveComplate()
    {
        return IsMoveComplate;
    }
    public bool GetIsRotateComplate()
    {
        return IsRotateComplate;
    }

    public bool GetIsInventoryComplate()
    {
        return IsInventoryComplate;
    }

}
public enum ComplateType
{
    FriendNoteComplate,
    SecurityKeyComplate,
    FloorEnterComplate,
    ClosetSpeakingComplate
}
public partial class S_MissionComplateController : ScriptableObject //Puzzles Missions
{
    private bool Puzzle1_1;
    private bool Puzzle1_2;
    private bool Puzzle1_3;

    public void PuzzleAndMissionID(int _pid, int _mid)
    {
        if (_pid == 1)
        {
            if (_mid == 1000)
            {
                Puzzle1_1Complate(_mid);
            }
            else if (_mid == 1001)
            {
                Puzzle1_2Complate(_mid);
            }
            else if (_mid == 1002)
            {
                Puzzle1_3Complate(_mid);
            }
        }
    }
    private void Puzzle1_1Complate(int _id)
    {
        if (PlayerManager.instance.player.IsBusy) return;
        Puzzle1_1 = true;
        if (!PuzzleManager.instance.DesiredPuzzleMissionBehaviour(_id).ComplateToggle.isOn)
            PuzzleManager.instance.DesiredPuzzleMissionBehaviour(_id).ComplateToggle.isOn = true;
    }
    private void Puzzle1_2Complate(int _id)
    {
        if (PlayerManager.instance.player.IsBusy) return;
        Puzzle1_2 = true;
        if (!PuzzleManager.instance.DesiredPuzzleMissionBehaviour(_id).ComplateToggle.isOn)
            PuzzleManager.instance.DesiredPuzzleMissionBehaviour(_id).ComplateToggle.isOn = true;
    }
    private void Puzzle1_3Complate(int _id)
    {
        if (PlayerManager.instance.player.IsBusy) return;
        Puzzle1_3 = true;
        if (!PuzzleManager.instance.DesiredPuzzleMissionBehaviour(_id).ComplateToggle.isOn)
            PuzzleManager.instance.DesiredPuzzleMissionBehaviour(_id).ComplateToggle.isOn = true;
    }
    public bool GetPuzzle1_1Complate()
    {
        return Puzzle1_1;
    }
    public bool GetPuzzle1_2Complate()
    {
        return Puzzle1_2;
    }
    public bool GetPuzzle1_3Complate()
    {
        return Puzzle1_3;
    }
}
