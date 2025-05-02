using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{

    private readonly int JumpSpeedHas = Animator.StringToHash("Jump");
    private const float CrossFadeDuration = 0.1f;
    private float jumpDuration = 1f;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Jump");
        stateMachine.Animator.CrossFadeInFixedTime(JumpSpeedHas, CrossFadeDuration);

    }

    private void OnJumpInput()
    {
       
    }

    public override void Tick(float deltaTime)
    {
        Move();

        if(deltaTime <= jumpDuration)
        {
            stateMachine.SwitchState(stateMachine.States[EPLAYERSTATE.IDLE]);
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.OnJumpEvent -= OnJumpInput;
    }

  
}
