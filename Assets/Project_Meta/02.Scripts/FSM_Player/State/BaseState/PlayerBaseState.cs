using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
    protected bool IsFaceing = false;


    public PlayerBaseState(PlayerStateMachine statMachine)
    {
        this.stateMachine = statMachine;
    }

    protected void Move()
    {
        Vector2 movement = CalculateMovement();
        FlipX(movement);
        stateMachine.rb.velocity = movement * stateMachine.MovementSpeed;
    }

    protected Vector2 CalculateMovement()
    {
        Vector2 movement = new Vector2();

        movement.x = stateMachine.InputReader.MovementValue.x;
        movement.y = stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private void FlipX(Vector2 movement)
    {

        if (movement.x == 0)
            return;

        //가독성때매 true : false 추가함
        stateMachine.SpriteRenderer.flipX = movement.x < 0 ? true : false;
    }
    protected void OnAttackInput()
    {
        if (!stateMachine.CanAttack)
            return;

        if (MainGameManager.Instance.CurrentState == EGAMESTATE.LOBBY)
            return;

        stateMachine.SwitchState(stateMachine.States[EPLAYERSTATE.ATTACK]);
    }

}
