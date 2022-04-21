using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerState {
    public PlayerGroundState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() { 
        base.EnterState();

        _context.PlayerInput.PlayerDash.performed += OnDash;
        _context.PlayerInput.PlayerAim.performed += OnAim;
        _context.PlayerInput.PlayerJump.performed += OnJump;
        _context.PlayerInput.PlayerMeleeAttack.performed += OnMeleeAttack;
    }

    public override void ExitState() {
        base.ExitState();

        _context.PlayerInput.PlayerDash.performed -= OnDash;
        _context.PlayerInput.PlayerAim.performed -= OnAim;
        _context.PlayerInput.PlayerJump.performed -= OnJump;
        _context.PlayerInput.PlayerMeleeAttack.performed -= OnMeleeAttack;
     }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (!GroundedCheck()) {
            _stateMachine.ChangeState(_factory.FallingState);
        }

        _context.Data.VerticalVelocity = -2f;
    }

    protected virtual void OnDash(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.DashState);
    }

    protected virtual void OnAim(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.AimState);
    }

    protected virtual void OnJump(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.JumpState);
    }

    protected virtual void OnMeleeAttack(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.AttackState);
    }
}
