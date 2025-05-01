using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{

    public override bool CanJump => true;
    public override bool CanAttack => true;
    public override bool IsFinished => stateMachine.InputReader.MovementValue == Vector2.zero;
    public PlayerMoveState(PlayerStateMachine statMachine) : base(statMachine){ }


    private void Start()
    {
        
    }


    public override void Enter()
    {
        Debug.Log("Move");
    }


    public override void Tick(float deltaTime)
    {
        Vector2 movement =  CalculateMovement();
        stateMachine.rb.velocity = movement * stateMachine.MovementSpeed;

    }


    public override void Exit()
    {  

    }
    private Vector2 CalculateMovement()
    {
        Vector2 movement = new Vector2();

        movement.x = stateMachine.InputReader.MovementValue.x;
        movement.y = stateMachine.InputReader.MovementValue.y;

        return movement;
    }


}
