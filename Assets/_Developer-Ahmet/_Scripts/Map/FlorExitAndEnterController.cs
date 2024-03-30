using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorExitAndEnterController : MonoBehaviour
{
    [SerializeField] int TextID;
    public bool isEnter = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isEnter)
            {                
                GameManager.instance.SetCurrentSchoolFloor(false);
                PuzzleManager.instance.MissionComplateController.MainStoryMultipleMissionComplate((int)GameManager.instance.GetCurrentSchoolFlor(),TextID,ComplateType.FloorEnterComplate);

                //if ((int)GameManager.instance.GetCurrentSchoolFlor() == 8 && false)
                //{
                //    GameMission m1 = new GameMission()
                //    isFirst = true;
                //}
            }                
            else
                GameManager.instance.SetCurrentSchoolFloor(true);
            isEnter = !isEnter;


        }
        
    }
}
