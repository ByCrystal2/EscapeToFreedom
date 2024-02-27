using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteraction : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] LayerMask _interactableLayers;

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
        if (Physics.Raycast(ray, out hitInfo, maxDistance, _interactableLayers))
        {
            // I��n belirli bir nesneye �arpt�
            // Burada �arp��an nesneyle ilgili i�lemleri yapabilirsiniz
            GameObject hitObject = hitInfo.collider.gameObject;
            
                // Etkile�im i�lemleri
               
            Debug.Log("Etkilesime girilebilir. \"Carpilan nesne:\" =>" + hitObject.name);
            hitObject.GetComponent<Interactable>().Interact();
        }
    }
}
