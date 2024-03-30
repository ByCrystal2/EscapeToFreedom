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
    public void ActivationDesiredPuzzleMission(int _desiredChild)
    {
        _puzzleContent.GetChild(_desiredChild).gameObject.SetActive(true);
    }
}
