using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotObj : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool _isFull;
    private ItemType type;
    [SerializeField] SlotContentType contentType;
    public void SetIsFull(bool _full)
    {
        _isFull = _full;
    }
    public bool GetIsFull()
    {
        return _isFull;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isFull && eventData.button == PointerEventData.InputButton.Right)
        {
            SlotItem currentItem = transform.GetChild(0).GetComponent<SlotItem>();
            currentItem.ItemClick(contentType);            
        }       
    }  

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            if (transform.GetChild(0).TryGetComponent(out SlotItem _myItem))
            {
                if (_myItem == SlotInfoPanelController.instance.CurrentItem)
                {
                    SlotInfoPanelController.instance.CurrentItem = null;
                }
            }
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetIsFull())
        {
            SlotItem currentItem = transform.GetChild(0).GetComponent<SlotItem>();
            UIManager.instance.SetActivationSlotItemInfoPanel(true, transform.position, currentItem.GetItemType().ToString(), currentItem.GetMySprite(), currentItem.GetMyData().Name, currentItem.GetMyData().Description);
            SlotInfoPanelController.instance.CurrentItem = currentItem;
        }
    }
}