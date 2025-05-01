using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    float timer = 0f;
    float attackDuration = 0.3f;

    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        timer = 0f;
        Debug.Log("Attack Start");
    }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
    }

    public override void Exit()
    {
       
    }

    public override bool IsFinished => timer >= attackDuration;
}
