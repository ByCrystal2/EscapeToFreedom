using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectPanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TxtCollect;
    private CollectType _currentCollectableObject;
    private Collectable _currentCollectableBehaviour;
    public static CollectPanelController instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void SetCurrentCollectable(Collectable _collectable)
    {
        _currentCollectableObject = _collectable.GetCollectType();
        _currentCollectableBehaviour = _collectable;
        TxtCollect.text = "Topla (" + _collectable.GetCollectType()+ ")";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !PlayerManager.instance.player.IsBusy)
        {            
            _currentCollectableBehaviour.Collect();           
        }
    }    
}
