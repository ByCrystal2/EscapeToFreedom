using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "S_MissionComplateController", menuName = "ScriptableObjects/S_MissionComplateController", order = 1)]
public partial class S_MissionComplateController : ScriptableObject // Main Story Missions
{
    private bool IsMoveComplate;
    private bool IsRotateComplate;
    private bool IsInventoryComplate;
    private bool IsMainStoryKeyPressComplate;
    private bool[] FriendNoteComplate = { false, false, false, false, false, false, false, false, false, false };
    private bool[] SecurityKeyComplate = { false, false, false, false, false, false, false, false, false, false };
    private bool[] PersonelKeyComplate = { false, false, false, false, false, false, false, false, false, false };
    private bool[] StairKeyComplate = { false, false, false, false, false, false, false, false, false, false };
    private bool[] NormalKeyComplate = { false, false, false, false, false, false, false, false, false, false };
    private bool[] FloorEnterComplate = { false, false, false, false, false, false, false, false, false, false };
    private bool[] ClosetSpeakingComplate = { false, false, false, false, false, false, false, false, false, false };
    private bool[] CrowbarComplate = { false, false, false, false, false, false, false, false, false, false };
    private List<int> toiletSpeakingMissionIDs = new List<int>();
    public void ResetComplateBools()
    {
        IsMoveComplate = false;
        IsRotateComplate = false;
        IsInventoryComplate = false;
        IsMainStoryKeyPressComplate = false;

        FriendNoteComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };
        //keys
        SecurityKeyComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };
        PersonelKeyComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };
        StairKeyComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };
        NormalKeyComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };
        //keys
        FloorEnterComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };
        ClosetSpeakingComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };

        //PuzzleOthers
        CrowbarComplate = new bool[] { false, false, false, false, false, false, false, false, false, false };
        //PuzzleOthers

        //Puzzles
        Puzzle1Complated = new bool[] { false, false, false, false};
        puzzle1ComplatedCount = 0;
        //Puzzles
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
    public void MainStoryPanelKeyPressMissionComplate()
    {
        if (PlayerManager.instance.player.IsBusy) return;
        IsMainStoryKeyPressComplate = true;
        if (!PuzzleManager.instance.DesiredMainStoryMissionBehaviour(4).ComplateToggle.isOn)
            PuzzleManager.instance.DesiredMainStoryMissionBehaviour(4).ComplateToggle.isOn = true;
    }
    public void MainStoryMultipleMissionComplate(int _whichNumber, int _missionId, ComplateType _complateType, KeyType _keyType = KeyType.None)
    {
        //if (PlayerManager.instance.player.IsBusy) return;
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
            case ComplateType.KeyComplate:
                switch (_keyType)
                {
                    case KeyType.None:
                        break;
                    case KeyType.SecurityKey:
                        if (SecurityKeyComplate[_whichNumber])
                        {
                            Debug.Log("Gorev zaten yapildi.");
                            return;
                        }
                        SecurityKeyComplate[_whichNumber] = true;
                        break;
                    case KeyType.Personel:
                        if (PersonelKeyComplate[_whichNumber])
                        {
                            Debug.Log("Gorev zaten yapildi.");
                            return;
                        }
                        PersonelKeyComplate[_whichNumber] = true;
                        break;
                    case KeyType.StairDoor:
                        if (StairKeyComplate[_whichNumber])
                        {
                            Debug.Log("Gorev zaten yapildi.");
                            return;
                        }
                        StairKeyComplate[_whichNumber] = true;
                        break;
                    case KeyType.NormalDoor:
                        if (NormalKeyComplate[_whichNumber])
                        {
                            Debug.Log("Gorev zaten yapildi.");
                            return;
                        }
                        NormalKeyComplate[_whichNumber] = true;
                        break;
                    default:
                        break;
                }
                
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
                //toiletSpeakingMissionIDs.Remove(_missionId);
                break;
            case ComplateType.CrowbarComplate:
                if (CrowbarComplate[_whichNumber])
                {
                    Debug.Log("Gorev zaten yapildi.");
                    return;
                }
                CrowbarComplate[_whichNumber] = true;
                break;
            default:
                break;
        }
        
        if (!PuzzleManager.instance.DesiredMainStoryMissionBehaviour(_missionId).ComplateToggle.isOn)
            PuzzleManager.instance.DesiredMainStoryMissionBehaviour(_missionId).ComplateToggle.isOn = true;
    }    
    public void SetToiletSpeakingIds(List<int> _toiletSpeakingIds)
    {
        toiletSpeakingMissionIDs = _toiletSpeakingIds;
    }
    public int GetToiletMissionID(int _index)
    {
        return toiletSpeakingMissionIDs[_index];
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
    public bool GetIsMainStoryKeyPressComplate()
    {
        return IsMainStoryKeyPressComplate;
    }

}
public enum ComplateType
{
    FriendNoteComplate,
    KeyComplate,
    FloorEnterComplate,
    ClosetSpeakingComplate,
    CrowbarComplate
}
public partial class S_MissionComplateController : ScriptableObject //Puzzles Missions
{
    private bool[] Puzzle1Complated = new bool[]
    {
        false,false,false, false
    };
    private int puzzle1ComplatedCount;
    public void PuzzleAndMissionID(int _pid, int _mid)
    {
        if (_pid == 0)
        {
            PuzzleComplate(_mid);
            Puzzle1Complated[puzzle1ComplatedCount] = true;
            puzzle1ComplatedCount++;
        }
    }
    private void PuzzleComplate(int _id)
    {
        if (PlayerManager.instance.player.IsBusy) return;        
        if (!PuzzleManager.instance.DesiredPuzzleMissionBehaviour(_id).ComplateToggle.isOn)
            PuzzleManager.instance.DesiredPuzzleMissionBehaviour(_id).ComplateToggle.isOn = true;
    }   
    public bool GetPuzzle1Complate(int _index)
    {
        return Puzzle1Complated[_index];
    }
    
}
