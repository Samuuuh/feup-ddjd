using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {
    private InputController _inputAction;

    #region Player Input Actions
    private InputAction _playerMovement;
    private InputAction _playerRun;
    private InputAction _playerDash;
    private InputAction _playerAim;
    private InputAction _playerLook;
    private InputAction _playerJump;
    private InputAction _playerMeleeAttack;
    private InputAction _playerInteract;
    private InputAction _playerUseItem;

    public InputAction PlayerMovement { get { return _playerMovement; } set { _playerMovement = value; } }
    public InputAction PlayerRun { get { return _playerRun; } set { _playerRun = value; } }
    public InputAction PlayerDash { get { return _playerDash; } set { _playerDash = value; } }
    public InputAction PlayerAim { get { return _playerAim; } set { _playerAim = value; } }
    public InputAction PlayerLook { get { return _playerLook; } set { _playerLook = value; } }
    public InputAction PlayerJump { get { return _playerJump; } set { _playerJump = value; } }
    public InputAction PlayerMeleeAttack { get { return _playerMeleeAttack; } set { _playerMeleeAttack = value; } }
    public InputAction PlayerInteract { get { return _playerInteract; } set { _playerInteract = value; } }

    public InputAction PlayerUseItem { get { return _playerUseItem; } set { _playerUseItem = value; } }
    #endregion

    #region UI Input Actions
    #endregion

    #region Input Values
    public Vector2 Movement {get; set;}
    public Vector2 Look {get; set;}
    public bool Interact {get; set;}
    public bool UseItem {get; set;}
    #endregion
    
    private void Awake() {
        _inputAction = new InputController();
        
        _playerMovement = _inputAction.Player.Move;
        _playerRun = _inputAction.Player.Run;
        _playerDash = _inputAction.Player.Dash;
        _playerAim = _inputAction.Player.Aim;
        _playerLook = _inputAction.Player.Look;
        _playerJump = _inputAction.Player.Jump;
        _playerMeleeAttack = _inputAction.Player.MeleeAttack;
        _playerInteract = _inputAction.Player.Interact;
        _playerUseItem = _inputAction.Player.UseItem;

        EnablePlayerInput();
    }

    private void EnablePlayerInput() {
        _playerMovement.performed += OnMovement;
        _playerMovement.canceled += OnMovement;
        _playerLook.performed += OnLook;
        _playerLook.canceled += OnLook;
        _playerInteract.performed += OnInteract;
        _playerUseItem.performed += OnUseItem;

        _inputAction.Player.Enable();
    }

    private void DisablePlayerInput() {
        _inputAction.Player.Disable();
    }

    private void Rebind() {
        // _playerJump.rebind("Gamepad/X");
    }

    private void OnMovement(InputAction.CallbackContext context) {
        Movement = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context) {
        Look = context.ReadValue<Vector2>();
	}

    private void OnInteract(InputAction.CallbackContext context){
        Interact = true;
    }

    private void OnUseItem(InputAction.CallbackContext context)
    {
        UseItem = true;
    }

    private void OnApplicationFocus(bool hasFocus) {
        Cursor.lockState =  CursorLockMode.Locked;
    }
}
