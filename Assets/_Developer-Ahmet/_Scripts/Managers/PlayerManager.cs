using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }
    public FirstPersonController player;
    public StarterAssetsInputs assetsInputs;
    private float _baseMoveSpeed; 
    private float _baseSprintSpeed; 
    private float _baseJumpHeight; 
    private float _baseRotationSpeed; 
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        assetsInputs = player.GetComponent<StarterAssetsInputs>();
        SavePlayerBaseOptions();
        UIManager.instance.SetActivationMenuPanel(true);
        PlayerLock();
        GameManager.instance.SetCursorLockMode(CursorLockMode.Confined);
    }
    private void SavePlayerBaseOptions()
    {
        _baseMoveSpeed = player.MoveSpeed;
        _baseSprintSpeed = player.SprintSpeed;
        _baseJumpHeight = player.JumpHeight;
        _baseRotationSpeed = player.RotationSpeed;
    }
    public void PlayerLock()
    {
        player.MoveSpeed = 0;
        player.SprintSpeed = 0;
        player.JumpHeight = 0;
        player.RotationSpeed = 0;
    }
    public void PlayerUnlock()
    {
        player.MoveSpeed = _baseMoveSpeed;
        player.SprintSpeed = _baseSprintSpeed;
        player.JumpHeight = _baseJumpHeight;
        player.RotationSpeed = _baseRotationSpeed;
    }
}
