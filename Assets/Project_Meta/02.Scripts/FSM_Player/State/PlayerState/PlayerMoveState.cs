using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private readonly int MoveSpeedHas = Animator.StringToHash("Move");
    private const float CrossFadeDuration = 0.1f;
    public override bool CanJump => true;
    public override bool CanAttack => true;
    public override bool IsFinished => stateMachine.InputReader.MovementValue == Vector2.zero;
    public PlayerMoveState(PlayerStateMachine statMachine) : base(statMachine){ }

    public override void Enter()
    {
        Debug.Log("Move");
        stateMachine.Animator.CrossFadeInFixedTime(MoveSpeedHas, CrossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {
        Move();

        if(stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.SwitchState(stateMachine.States[EPLAYERSTATE.IDLE]);
        }
    }


    public override void Exit()
    {
        
    }




}
