using DG.Tweening;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		bool _isInventoryActive = false;
		bool _isMainPanelActive = false;
		
        private void Update()
        {
			if (Input.GetKeyDown(KeyCode.I) && !PlayerManager.instance.playerInventory._isBusy)//envateri acar kapar
			{
				_isInventoryActive = !_isInventoryActive;
				if (_isInventoryActive)
				{
					if (!PuzzleManager.instance.MissionComplateController.GetIsInventoryComplate())
					{
						PuzzleManager.instance.MissionComplateController.InventoryMissionComplate();

                    }
					ItemManager.instance.CurrentActiveInventoryPanel = ActiveInventoryPanel.PlayerInventory;
					PlayerManager.instance.PlayerLock();
					SetCursorState(false);
					cursorLocked = false;
                }
				else
				{
                    ItemManager.instance.CurrentActiveInventoryPanel = ActiveInventoryPanel.None;
                    PlayerManager.instance.PlayerUnlock();
                    SetCursorState(true);
                    cursorLocked = true;
                }
                UIManager.instance.SetActivationInventoryPanel(_isInventoryActive);
            }
			if (Input.GetKeyDown(KeyCode.U)) //maintstory panelini acar kapar
			{
				_isMainPanelActive = !_isMainPanelActive;
				//Tween startTween;
                if (_isMainPanelActive)
				{
					UIManager.instance.StartDOMoveMissionPanel(true);
                    if (!PuzzleManager.instance.MissionComplateController.GetIsMainStoryKeyPressComplate())
                    {
                        PuzzleManager.instance.MissionComplateController.MainStoryPanelKeyPressMissionComplate();
                    }
                }
				else
				{
                   UIManager.instance.EndDOMoveMissionPanel(true);
                }
				
				
			}
        }		
#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}