using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState {
    public PlayerWalkState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        _context.TargetSpeed = _context.PlayerSettings.MoveSpeed;
    }

    public override void ExitState() {}

    public override void UpdateState() {
        _context.Move();
        CheckSwitchState();
    }

	public override void CheckSwitchState() {
        if (_context.PlayerInput.move == Vector2.zero) {
            SwitchState(_factory.Idle());
        } 
    }
}