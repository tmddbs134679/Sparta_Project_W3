using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
   
    Vector2 mousePos = Vector2.zero;

    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Attack");
    
        mousePos = CheckMousePos();
        ProjectileFactoryManager.Instance.SpawnProjectile(stateMachine.transform.position, mousePos);

        stateMachine.lastAttackTime = Time.time;
        stateMachine.SwitchState(stateMachine.States[EPLAYERSTATE.IDLE]);
    }

    public override void Tick(float deltaTime)
    {
      
    }

    public override void Exit()
    {
       
    }

    private Vector2 CheckMousePos()
    {
        
        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        Vector2 dir = (mouseWorldPos - (Vector2)stateMachine.transform.position).normalized;

        return dir;
    }

}
