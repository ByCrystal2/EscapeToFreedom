using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotObj : MonoBehaviour, IPointerClickHandler
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
            transform.GetChild(0).GetComponent<SlotItem>().ItemClick(contentType);
        }       
    }
    
}