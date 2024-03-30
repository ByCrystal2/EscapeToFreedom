using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class MainToiletBehaviour : MonoBehaviour
{        
    [SerializeField] bool _speakingEnd = true;

    private bool isBusy;
    public void InteractCloset()
    {
        if (isBusy)
        {
            if (UIManager.instance.GetInteractPanelActive())
            {
                UIManager.instance.InteractPanelActivation(false);
            }
            return;
        }
        InteractPanelController.instance.SetCurrentInteractionCloset(this);
    }
    public void StartInteract()
    {
        UIManager.instance.SetActivationSpeakingPanel(true);
        SetIsBusy(true);
    }
    public void SetIsSpeaking(bool _speak)
    {
        _speakingEnd = _speak;
    }
    public bool GetIsSpeaking()
    {
        return _speakingEnd;
    }
    public void SetIsBusy(bool _isBusy)
    {
        isBusy = _isBusy;
    }
    public bool GetIsBusy()
    {
        return isBusy;
    }
}