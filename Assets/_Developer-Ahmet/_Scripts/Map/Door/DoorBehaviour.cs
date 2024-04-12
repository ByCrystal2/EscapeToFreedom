using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class DoorBehavior : MonoBehaviour
{
    [SerializeField] DoorType _doorType = DoorType.Single;
    [SerializeField] Direction _doorDirection = Direction.North;
    [SerializeField] bool _isClassroomDoor = false;
    [SerializeField] bool _isMouseRoomDoor = false;
    public bool _isOpen = false;
    private bool _isBusy = false;
    Coroutine _coroutine;
    Quaternion _startRotation;
    Quaternion _endRotation;
    DoorBehavior _otherDoor;

    [SerializeField] public bool isEndGameDoor = false;
    void Start()
    {
        _startRotation = transform.rotation;
        _endRotation = _startRotation * Quaternion.Euler(0, 90, 0); // Kapýnýn 90 derece açýlmasý

        if (_doorType == DoorType.Double)
        {
            int length = transform.parent.childCount;
            DoorBehavior _myBehaviour = GetComponent<DoorBehavior>();
            for (int i = 0; i < length; i++)
            {
                DoorBehavior _childDoor = transform.parent.GetChild(i).GetComponent<DoorBehavior>();
                if (_childDoor != _myBehaviour)
                {
                    _otherDoor = _childDoor;
                    break;
                }
            }
        }
        
    }

    // Update is called once per frame
    public void InteractDoor()
    {
        if (_isBusy) 
        {
            if (UIManager.instance.GetInteractPanelActive())
            {
                UIManager.instance.InteractPanelActivation(false);
            }
            return;
        }
        UIManager.instance.InteractPanelActivation(true);
        InteractPanelController.instance.SetCurrentInteractionDoor(this);

    }
    public void StartInteract()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        UIManager.instance.InteractPanelActivation(true);
        if (_isOpen)
        {
            _coroutine = StartCoroutine(CloseDoor());
        }
        else
        {
            _coroutine = StartCoroutine(OpenDoor());
        }
    }
    IEnumerator OpenDoor()
    {
        if (_isMouseRoomDoor)
        {
            GetComponentInParent<SchoolClassManager>().MousesActive(true);
        }
        _isBusy = true;
        if (_otherDoor != null)
        {
            _otherDoor._isBusy = true;
        }
        float openDuration = 1f; // Kapýnýn açýlma süresi
        float elapsedTime = 0f;

        Quaternion _otherStartRotation = new Quaternion();
        Quaternion _otherEndRotation = new Quaternion();
        if (_doorType == DoorType.Double)
        {
            _otherStartRotation = _otherDoor._startRotation;
            _otherEndRotation = _otherDoor._endRotation;
        }
        
        while (elapsedTime < openDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / openDuration); // Açýlma oraný
            transform.rotation = Quaternion.Slerp(_startRotation, _endRotation, t);
            if (_doorType == DoorType.Double)
            {
                if (_doorDirection == Direction.North || _doorDirection == Direction.South)
                {
                    _otherDoor.transform.rotation = Quaternion.Slerp(_otherStartRotation, new Quaternion(_otherEndRotation.x, -_otherEndRotation.y, _otherEndRotation.z, _otherEndRotation.w), t);
                    //_otherDoor.transform.rotation = Quaternion.Slerp(_startRotation, _endRotation, t);
                }
                else if(_doorDirection == Direction.East || _doorDirection == Direction.West)
                {
                    _otherDoor.transform.rotation = Quaternion.Slerp(_otherStartRotation, new Quaternion(_otherEndRotation.x, -_otherEndRotation.y, _otherEndRotation.z, _otherEndRotation.w), t);
                    //_otherDoor.transform.rotation = Quaternion.Slerp(_startRotation, _endRotation, t);
                }
                
            }
            yield return null;
        }

        _isOpen = true;
        _isBusy = false;
        if (_otherDoor != null)
        {
            _otherDoor._isOpen = true;
            _otherDoor._isBusy = false;
        }
        if (_isClassroomDoor)
        {
            GetComponentInParent<SchoolClassManager>().AllLookPlayer();
        }
        
    }
    IEnumerator CloseDoor()
    {
        _isBusy = true;
        if (_otherDoor != null)
        {
            _otherDoor._isBusy = true;
        }
        float closeDuration = 1f; // Kapýnýn kapanma süresi
        float elapsedTime = 0f;

        while (elapsedTime < closeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / closeDuration); // Kapanma oraný
            transform.rotation = Quaternion.Slerp(_endRotation, _startRotation, t);
            if (_doorType == DoorType.Double)
            {
                if (_doorDirection == Direction.North || _doorDirection == Direction.South)
                {
                    _otherDoor.transform.rotation = Quaternion.Slerp(new Quaternion(_endRotation.x, -_endRotation.y, _endRotation.z, _endRotation.w), _startRotation, t);
                }
                else
                {
                    _otherDoor.transform.rotation = Quaternion.Slerp(_endRotation, _startRotation, t);
                }
            }
            yield return null;
        }
        _isOpen = false;
        _isBusy = false;
        if (_otherDoor != null)
        {
            _otherDoor._isOpen = false;
            _otherDoor._isBusy = false;
        }
        if (_isClassroomDoor)
        {
            GetComponentInParent<SchoolClassManager>().AllNotLookPlayer();
        }
        if (_isMouseRoomDoor)
        {
            GetComponentInParent<SchoolClassManager>().MousesActive(false);
        }
    }
}
public enum DoorType
{
    Single,
    Double
}
