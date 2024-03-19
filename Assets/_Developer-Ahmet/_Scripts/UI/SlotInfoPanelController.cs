using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotInfoPanelController : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] TextMeshProUGUI txtItemName;
    [SerializeField] Image imgItemSprite;
    [SerializeField] TextMeshProUGUI txtItemHeader;
    [SerializeField] TextMeshProUGUI txtItemDescription;

    public SlotItem CurrentItem;
    public static SlotInfoPanelController instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void FixedUpdate()
    {
        if (CurrentItem == null)
        {
            UIManager.instance.SetActivationSlotItemInfoPanel(false, transform.position);
        }
        
    }
    public void SetSlotInfoPanelUIs(string _itemName, Sprite _itemSprite, string _itemHeader, string _itemDescription, Vector3 _itemCenterPos)
    {
        pivot.transform.position = _itemCenterPos;
        txtItemName.text = _itemName;
        imgItemSprite.sprite = _itemSprite;
        txtItemHeader.text = _itemHeader;
        txtItemDescription.text = _itemDescription;
    }
}
