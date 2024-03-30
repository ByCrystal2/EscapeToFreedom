using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SchoolFloorManager : MonoBehaviour
{
    [SerializeField] SchoolFloor floor;
    [SerializeField] List<GameObject> Personnels = new List<GameObject>();
    [SerializeField] List<GameObject> SecurityCameras = new List<GameObject>();

    [SerializeField] MainToiletBehaviour MyFloorToilet;
    public void AllFloorPersonnelsCatchThePlayer()
    {
        foreach (var _personel in Personnels)
        {
            PersonnelBehaviour currentBehaviour = _personel.GetComponent<PersonnelBehaviour>();
            currentBehaviour.PlayerTargetDistance = 100f;
            currentBehaviour._agent.speed = 20f;
        }
    }
    public SchoolFloor GetFloor()
    {
        return floor;
    }
    public MainToiletBehaviour GetMyToilet()
    {
        return MyFloorToilet;
    }
}
