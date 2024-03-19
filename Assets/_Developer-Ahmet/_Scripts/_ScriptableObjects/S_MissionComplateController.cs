using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "S_MissionComplateController", menuName = "ScriptableObjects/S_MissionComplateController", order = 1)]
public partial class S_MissionComplateController : ScriptableObject // Main Story Missions
{
    private bool IsMoveComplate;
    private bool IsRotateComplate;
    private bool IsInventoryComplate;

    public void ResetComplateBools()
    {
        IsMoveComplate = false;
        IsRotateComplate = false;
        IsInventoryComplate = false;
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
