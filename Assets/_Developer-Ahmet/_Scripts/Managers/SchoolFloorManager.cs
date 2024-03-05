using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SchoolFloorManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Personnels = new List<GameObject>();
    [SerializeField] List<GameObject> SecurityCameras = new List<GameObject>();

    public void AllFloorPersonnelsCatchThePlayer()
    {
        foreach (var _personel in Personnels)
        {
            PersonnelBehaviour currentBehaviour = _personel.GetComponent<PersonnelBehaviour>();
            currentBehaviour.PlayerTargetDistance = 100f;
            currentBehaviour._agent.speed = 20f;
            PlayerManager.instance.player.transform.LookAt(currentBehaviour.transform.position);
        }
    }
}
