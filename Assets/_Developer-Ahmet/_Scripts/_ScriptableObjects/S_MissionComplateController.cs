using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "S_MissionComplateController", menuName = "ScriptableObjects/S_MissionComplateController", order = 1)]
public class S_MissionComplateController : ScriptableObject
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
        if (!PuzzleManager.instance.DesiredMissionBehaviour(1).ComplateToggle.isOn)
        PuzzleManager.instance.DesiredMissionBehaviour(1).ComplateToggle.isOn = true;
    }
    public void RotateMissionComplate()
    {
        if (PlayerManager.instance.player.IsBusy) return;
        IsRotateComplate = true;
        if (!PuzzleManager.instance.DesiredMissionBehaviour(2).ComplateToggle.isOn)
            PuzzleManager.instance.DesiredMissionBehaviour(2).ComplateToggle.isOn = true;
    }
    public void InventoryMissionComplate()
    {
        if (PlayerManager.instance.player.IsBusy) return;
        IsInventoryComplate = true;
        if (!PuzzleManager.instance.DesiredMissionBehaviour(3).ComplateToggle.isOn)
            PuzzleManager.instance.DesiredMissionBehaviour(3).ComplateToggle.isOn = true;
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
