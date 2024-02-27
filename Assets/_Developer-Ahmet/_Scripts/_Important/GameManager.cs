using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetCursorLockMode(CursorLockMode _lockMode)
    {
        Cursor.lockState = _lockMode;
        if (_lockMode == CursorLockMode.Locked)
            PlayerManager.instance.assetsInputs.cursorLocked = true;
        else
            PlayerManager.instance.assetsInputs.cursorLocked = false;
    }
}
