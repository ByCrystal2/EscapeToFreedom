using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChild : MonoBehaviour
{
    [SerializeField] int _targetPuzzleID;

    public int GetTargetPuzzleID()
    {
        return _targetPuzzleID;
    }
}
