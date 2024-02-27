using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteraction : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] LayerMask _interactableLayers;

    void FixedUpdate()
    {
        // Raycast'i oluþtur
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);

        RaycastHit hitInfo;
        float maxDistance = 2f; // Iþýnýn maksimum menzili
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
            // Iþýn belirli bir nesneye çarptý
            // Burada çarpýþan nesneyle ilgili iþlemleri yapabilirsiniz
            GameObject hitObject = hitInfo.collider.gameObject;
            
                // Etkileþim iþlemleri
               
            Debug.Log("Etkilesime girilebilir. \"Carpilan nesne:\" =>" + hitObject.name);
            hitObject.GetComponent<Interactable>().Interact();
        }
    }
}
