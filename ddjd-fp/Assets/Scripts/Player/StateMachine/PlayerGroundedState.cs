using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState {
    public PlayerGroundedState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        _isRootState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        Debug.Log("Player is grounded yayy");
    }

    public override void ExitState() {
        Debug.Log("Player left ground yayy");
    }

    public override void UpdateState() {
         _context.FallTimeoutDelta = _context.FallTimeout;
        if (_context.VerticalVelocity < 0.0f) {
            _context.VerticalVelocity = -2f;
        }

        if (_context.JumpTimeoutDelta >= 0.0f) {
            _context.JumpTimeoutDelta -= Time.deltaTime;
        } 

        if (_context.VerticalVelocity < _context.TerminalVelocity) {
            _context.VerticalVelocity += _context.Gravity * Time.deltaTime;
        }

        CheckSwitchState();
    }

	public override void CheckSwitchState() {
         if (_context.PlayerInput.jump  && _context.JumpTimeoutDelta <= 0.0f)  {
            SwitchState(_factory.Jump());
        }
    }

	protected override void InitializeSubState() {
        if (_context.PlayerInput.move != Vector2.zero) {
            SetSubState(_factory.Walk());
        } else {
            SetSubState(_factory.Idle());
        }
    }
}