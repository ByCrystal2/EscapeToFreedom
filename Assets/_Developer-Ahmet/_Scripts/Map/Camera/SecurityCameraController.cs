using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraController : MonoBehaviour
{
    [SerializeField] bool isRotate = true;
    [SerializeField] Transform RotationObject;
    public float RotationTime = 5;
    public float PlayerRotationTime = 3;
    public Vector3 StartPos;
    public Vector3 EndPos;
    private bool isCameraCatch = false;
    private Tweener currentRotationTween;
    void Start()
    {
        if (!isRotate) return;
        GoRotatingEnd();
    }
    private void GoRotatingEnd()
    {
        // Eðer halihazýrda bir dönme animasyonu varsa, iptal et
        if (currentRotationTween != null && currentRotationTween.IsActive())
        {
            currentRotationTween.Kill();
        }
        currentRotationTween = RotationObject.DORotate(EndPos, RotationTime).OnUpdate(() =>
        {
            if (isCameraCatch)
            {
                // Player yakalandý, dönme animasyonunu iptal et
                currentRotationTween.Kill();
            }
        }).OnComplete(() => GoRotatingStart());
    }

    private void GoRotatingStart()
    {
        // Eðer halihazýrda bir dönme animasyonu varsa, iptal et
        if (currentRotationTween != null && currentRotationTween.IsActive())
        {
            currentRotationTween.Kill();
        }

        // Yeni dönme animasyonunu baþlat
        currentRotationTween = RotationObject.DORotate(StartPos, RotationTime).OnUpdate(() =>
        {
            if (isCameraCatch)
            {
                // Player yakalandý, dönme animasyonunu iptal et
                currentRotationTween.Kill();
            }
        }).OnComplete(() => GoRotatingEnd());
    }
    public void CameraCatchPlayer(bool _isCatch)
    {
        isCameraCatch = _isCatch;
    }
    private void Update()
    {
        if (isCameraCatch)
        {
            Debug.Log("isCameraCatch => " + isCameraCatch);
            RotationObject.LookAt(PlayerManager.instance.player.transform.position);
        }
        else
        {
            if (!currentRotationTween.IsActive())
            {
                GoRotatingEnd();
            }            
        }
    }
}
