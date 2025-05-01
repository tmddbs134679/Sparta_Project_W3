using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    public abstract void Enter(); 

    public abstract void Tick(float deltaTime);

    public abstract void Exit();

    public virtual void OnJump() { }
    public virtual void OnAttack() { }

    public virtual bool CanJump => false;
    public virtual bool CanAttack => false;

    public virtual bool IsFinished => false;
}
