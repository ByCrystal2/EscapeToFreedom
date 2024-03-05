using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolClassManager : MonoBehaviour
{
    [Header("NormalClass")]
    [SerializeField] List<GameObject> Teachers = new List<GameObject>();
    [SerializeField] List<GameObject> Students = new List<GameObject>();
    [Header("MouseRoom")]
    [SerializeField] List<GameObject> Mouses = new List<GameObject>();
    [SerializeField] Transform _WayContent;
    static List<Transform> Ways = new List<Transform>();
    Coroutine C_ActiveLookNPCs;
    private void Awake()
    {
        if (_WayContent != null)
        {
            int length = _WayContent.childCount;
            for (int i = 0; i < length; i++)
            {
                Ways.Add(_WayContent.GetChild(i));
            }
        }        
        MousesActive(false);
    }
    public void AllLookPlayer()
    {
        if (C_ActiveLookNPCs != null)
        {
            StopCoroutine(C_ActiveLookNPCs);
        }
        int length = Teachers.Count;
        for (int i = 0; i < length; i++)
        {
            Teachers[i].GetComponentInChildren<FollowPlayer>().SetLook(true);
        }
        int length1 = Students.Count;
        for (int i = 0; i < length1; i++)
        {
            Students[i].GetComponentInChildren<FollowPlayer>().SetLook(true);
        }
        C_ActiveLookNPCs = StartCoroutine(SetActiveLookNPCs(false,5));
    }
    public void AllNotLookPlayer()
    {
        if (C_ActiveLookNPCs != null)
        {
            StopCoroutine(C_ActiveLookNPCs);
        }
        int length = Teachers.Count;
        for (int i = 0; i < length; i++)
        {
            Teachers[i].GetComponentInChildren<FollowPlayer>().SetLook(false);
        }
        int length1 = Students.Count;
        for (int i = 0; i < length1; i++)
        {
            Students[i].GetComponentInChildren<FollowPlayer>().SetLook(false);
        }
        C_ActiveLookNPCs = StartCoroutine(SetActiveLookNPCs(true,1));
    }
    IEnumerator SetActiveLookNPCs(bool _active, int _duration)
    {
        yield return new WaitForSeconds(_duration);
        foreach (var npc in Teachers)
        {
            npc.SetActive(_active);
        }
        foreach (var npc in Students)
        {
            npc.SetActive(_active);
        }
    }
    public void MousesActive(bool _active)
    {
        int length = Mouses.Count;
        for (int i = 0; i < length; i++)
        {
            Mouses[i].GetComponent<MouseBehaviour>().enabled = _active;
        }
    }
    public static Transform GetRandomWay()
    {
        int index =  Random.Range(0, Ways.Count);
        Debug.Log("Ways[index] => " + Ways[index]);
        return Ways[index];
    }
}