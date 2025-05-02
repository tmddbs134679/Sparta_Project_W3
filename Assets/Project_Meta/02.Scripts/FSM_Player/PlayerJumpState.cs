using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    float timer = 0f;
    float jumpDuration = 0.1f;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Jump");
        timer = 0f;
    }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
    }

    public override void Exit()
    {
      
    }

    public override bool IsFinished => timer >= jumpDuration;
}
