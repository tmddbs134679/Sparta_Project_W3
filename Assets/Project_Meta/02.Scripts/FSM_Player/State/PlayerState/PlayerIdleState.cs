using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private readonly int IdleSpeedHas = Animator.StringToHash("Idle");
    private const float CrossFadeDuration = 0.1f;
    public PlayerIdleState(PlayerStateMachine statMachine) : base(statMachine)
    {
     
    }

    public override void Enter()
    {
        Debug.Log("Idle");
        stateMachine.Animator.CrossFadeInFixedTime(IdleSpeedHas, CrossFadeDuration);
        stateMachine.InputReader.OnJumpEvent += OnJumpInput;
        stateMachine.InputReader.OnAttackEvent += OnAttackInput;
    }



    private void OnJumpInput()
    {
        stateMachine.SwitchState(stateMachine.States[EPLAYERSTATE.JUMP]);
    }

    public override void Tick(float deltaTime)
    {
      
       if (stateMachine.InputReader.MovementValue != Vector2.zero)
        {
            stateMachine.SwitchState(stateMachine.States[EPLAYERSTATE.MOVE]);
        }
    }


    public override void Exit()
    {
        stateMachine.InputReader.OnJumpEvent -= OnJumpInput;
        stateMachine.InputReader.OnAttackEvent -= OnAttackInput;
    }


}
