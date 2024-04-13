using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    [SerializeField] Transform _mainstoryContent;
    [SerializeField] Transform _puzzleContent;

    public static MissionsManager instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void ActivationDesiredMainStoryMission(int _desiredChild)
    {
        _mainstoryContent.GetChild(_desiredChild).gameObject.SetActive(true);
    }
    public void ActivationDesiredPuzzleMission(SchoolFloor _targetFloor, int _puzzleID)
    {
        Transform puzzleChildObj = _puzzleContent.GetChild((int)_targetFloor);
        int length = puzzleChildObj.transform.childCount;
        for (int i = 0; i < length; i++)
        {
            if (puzzleChildObj.GetChild(i).TryGetComponent(out PuzzleChild _pc))
            {
                if (_pc.GetTargetPuzzleID() == _puzzleID)
                {
                    puzzleChildObj.GetChild(i).gameObject.SetActive(true);
                }                
            }
        }
    }
    public void ActivationDesiredPuzzleMission(int _targetFloor, int _puzzleID)
    {
        Transform puzzleChildObj = _puzzleContent.GetChild(_targetFloor);
        int length = puzzleChildObj.transform.childCount;
        for (int i = 0; i < length; i++)
        {
            if (puzzleChildObj.GetChild(i).TryGetComponent(out PuzzleChild _pc))
            {
                if (_pc.GetTargetPuzzleID() == _puzzleID)
                {
                    puzzleChildObj.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}
