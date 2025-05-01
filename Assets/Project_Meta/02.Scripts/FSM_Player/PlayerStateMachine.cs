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

    [field: SerializeField] public float MovementSpeed { get; private set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        States.Add(EPLAYERSTATE.IDLE,new PlayerIdleState(this));
        States.Add(EPLAYERSTATE.MOVE, new PlayerMoveState(this));
        States.Add(EPLAYERSTATE.JUMP, new PlayerJumpState(this));
        States.Add(EPLAYERSTATE.ATTACK, new PlayerAttackState(this));

        InputReader.OnJumpEvent += OnJumpInput;
        InputReader.OnAttackEvent += OnAttackInput;

    }

    private void OnJumpInput()
    {
        if (currentState != null && currentState.CanJump)
            SwitchState(States[EPLAYERSTATE.JUMP]);
    }

    private void OnAttackInput()
    {
        if (currentState != null && currentState.CanAttack)
            SwitchState(States[EPLAYERSTATE.ATTACK]);
    }


    private void Start()
    {
        SwitchState(States[EPLAYERSTATE.IDLE]);
    }


    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
        CheckTransitions();
    }

    private void CheckTransitions()
    {
        if (currentState == States[EPLAYERSTATE.JUMP] && currentState.IsFinished)
            SwitchState(States[EPLAYERSTATE.IDLE]);

        if (currentState == States[EPLAYERSTATE.IDLE] && InputReader.MovementValue != Vector2.zero)
        {
            SwitchState(States[EPLAYERSTATE.MOVE]);
            return;
        }

        if(currentState == States[EPLAYERSTATE.MOVE] && InputReader.MovementValue == Vector2.zero)
        {
            SwitchState(States[EPLAYERSTATE.IDLE]);
            return;
        }

        if (currentState == States[EPLAYERSTATE.ATTACK] && currentState.IsFinished)
            SwitchState(States[EPLAYERSTATE.IDLE]);
    }
}
