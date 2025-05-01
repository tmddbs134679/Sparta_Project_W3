using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{

    public override bool CanJump => true;
    public override bool CanAttack => true;
    public PlayerIdleState(PlayerStateMachine statMachine) : base(statMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.rb.velocity = Vector2.zero;

    }


    public override void Tick(float deltaTime)
    {
       
    }


    public override void Exit()
    {
    
    }


}
