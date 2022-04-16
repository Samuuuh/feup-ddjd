using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState {
    public PlayerWalkState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        _context.TargetSpeed = _context.PlayerSettings.MoveSpeed;
        _context.Animator.SetBool("Walk", true);
    }

    public override void ExitState() {
        _context.TargetSpeed = 0f;
        _context.Animator.SetBool("Walk", false);
    }

    public override void UpdateState() {
        CheckSwitchState();

        _context.Move();
    }

	public override void CheckSwitchState() {
        if (_context.PlayerInput.move == Vector2.zero) {
            SwitchState(_factory.Idle());
        } else if (_context.PlayerInput.meleeAttack) {
            SwitchState(_factory.Attack());
        }
    }
}