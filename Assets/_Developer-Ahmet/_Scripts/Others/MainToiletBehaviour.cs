using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainToiletBehaviour : MonoBehaviour
{
    [SerializeField] Quaternion _startRotate;
    [SerializeField] Quaternion _endRotateRotate;
    [SerializeField] Transform _toiletdeksel;
    public bool SpeakingEnd;
    public void DOEndRotate()
    {
        _toiletdeksel.DORotateQuaternion(_endRotateRotate, 0.4f).OnComplete(() =>
        {
            if (!SpeakingEnd)
            {
                DOStartRotate();
            }
            else
            {
                return;
            }
        });
    }
    public void DOStartRotate()
    {
        _toiletdeksel.DORotateQuaternion(_startRotate, 0.2f).OnComplete(() =>
        {
            if (!SpeakingEnd)
            {
                DOEndRotate();
            }
            else
            {
                return;
            }
        });
    }
}
