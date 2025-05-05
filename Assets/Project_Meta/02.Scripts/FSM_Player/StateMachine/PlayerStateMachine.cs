using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Dictionary<EPLAYERSTATE, PlayerBaseState> States = new Dictionary<EPLAYERSTATE, PlayerBaseState>();
    public Rigidbody2D rb { get; private set; } 
    [field: SerializeField] public InputReader InputReader { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }

    [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

    public bool CanAttack => Time.time >= lastAttackTime + AttackCooldown;

    public float AttackCooldown = 0.5f;

    public float lastAttackTime = -999f;
    public bool IsFacingRight { get; private set; } = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        States.Add(EPLAYERSTATE.IDLE,new PlayerIdleState(this));
        States.Add(EPLAYERSTATE.MOVE, new PlayerMoveState(this));
        States.Add(EPLAYERSTATE.JUMP, new PlayerJumpState(this));
        States.Add(EPLAYERSTATE.ATTACK, new PlayerAttackState(this));

    }


    private void Start()
    {
        SwitchState(States[EPLAYERSTATE.IDLE]);
    }


    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
       
    }
}
