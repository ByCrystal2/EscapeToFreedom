using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteraction : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] LayerMask _interactableLayers;
    [SerializeField] LayerMask _collectableLayers;
    [SerializeField] LayerMask _speakableLayers;
    void FixedUpdate()
    {
        
        // Raycast'i olu�tur
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);

        RaycastHit hitInfo;
        float maxDistance = 2f; // I��n�n maksimum menzili
        if (UIManager.instance.GetInteractPanelActive())
        {
            UIManager.instance.InteractPanelActivation(false);
        }
        if (UIManager.instance.GetLockedInteractPanelActive())
        {
            UIManager.instance.LockedInteractPanelActivation(false);
        }
        if (UIManager.instance.GetCollectPanelActive())
        {
            UIManager.instance.CollectPanelActivation(false);
        }
        if (PlayerManager.instance.player.IsBusy) return;
        if (Physics.Raycast(ray, out hitInfo, maxDistance, _interactableLayers))
        {
            // I��n belirli bir nesneye �arpt�
            // Burada �arp��an nesneyle ilgili i�lemleri yapabilirsiniz
            GameObject hitObject = hitInfo.collider.gameObject;

            // Etkile�im i�lemleri            
            hitObject.GetComponent<Interactable>().Interact();
        }
        if (Physics.Raycast(ray, out hitInfo, maxDistance, _collectableLayers))
        {
            Debug.Log("Collectable Object Hit => " + hitInfo.collider.gameObject);
            // I��n belirli bir nesneye �arpt�
            // Burada �arp��an nesneyle ilgili i�lemleri yapabilirsiniz
            GameObject hitObject = hitInfo.collider.gameObject;
            Collectable currentCollectable = hitObject.GetComponent<Collectable>();
            // Toplama islemleri
            if (!currentCollectable.GetIsCollected())
            {
                UIManager.instance.CollectPanelActivation(true);
                CollectPanelController.instance.SetCurrentCollectable(currentCollectable);
            }            
        }
        if (Physics.Raycast(ray, out hitInfo, maxDistance, _speakableLayers))
        {
            Debug.Log("Speakable Object Hit => " + hitInfo.collider.gameObject);
            // I��n belirli bir nesneye �arpt�
            // Burada �arp��an nesneyle ilgili i�lemleri yapabilirsiniz
            GameObject hitObject = hitInfo.collider.gameObject;
            Speakable currentSpeakable = hitObject.GetComponent<Speakable>();
            // Toplama islemleri
            if (currentSpeakable.GetIsSpeak())
            {
                UIManager.instance.SetActivationSpeakingPanel(true);
            }
        }
    }
}
