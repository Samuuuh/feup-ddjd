using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState {
    public PlayerJumpState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory)  : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        Events.OnFreeFall.Invoke();
        _context.Animator.SetBool("Jump", true);
        FMODUnity.RuntimeManager.PlayOneShot(_context.JumpSoundEvent);
        PerformJump();
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        _stateMachine.ChangeState(_factory.FallingState);
    }

    private void PerformJump() {
        _context.Data.VerticalVelocity = Mathf.Sqrt(_context.Data.JumpHeight * -2f * _context.Data.Gravity);
    }
}