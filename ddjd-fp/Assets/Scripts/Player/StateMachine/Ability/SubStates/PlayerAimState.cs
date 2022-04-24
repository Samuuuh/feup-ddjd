using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimState : PlayerAbilityState {
    [Header("Throwing")]
    private float throwCooldown = 1f;
    private float throwForce = 5f;
    private float throwUpwardForce = 5f;
    private bool readyToThrow = true; 
    private GameObject companion = GameObject.Find("Companion");

    public PlayerAimState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        readyToThrow = true;
        _context.PlayerInput.PlayerMeleeAttack.performed += OnThrow;
        _context.PlayerInput.PlayerAim.canceled += OnAimCancelled;
        
    }  

    public override void ExitState() {
        base.ExitState();

        _context.PlayerInput.PlayerMeleeAttack.performed -= OnThrow;
        _context.PlayerInput.PlayerAim.canceled += OnAimCancelled;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // after x elapsed time ready to throw  = true

        // Some movement of the player, 

        // change player angle PlayerRotation on base
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    private void OnThrow(InputAction.CallbackContext contextInput) {
        if (readyToThrow) {
            GameObject projectile = _context.InstantiateObj(_context.ObjectToThrow,  companion.transform.position + new Vector3(0f, 0f, 0f), _context.Camera.MainCamera.transform.rotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            Vector3 forceToAdd = _context.Camera.MainCamera.transform.forward * throwForce + _context.transform.up * throwUpwardForce;

            projectileRb.AddForce(forceToAdd,ForceMode.Impulse);
        }
    }

    protected virtual void OnAimCancelled(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.IdleState);
    }

}