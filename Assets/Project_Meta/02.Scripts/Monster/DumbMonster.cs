using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbMonster : MonoBehaviour
{
    private Vector3 moveDirection;
    public float speed = 3f;
    public event Action<DumbMonster> OnDead;
    public int Point { get; private set; } = 1;

    public void Init(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;

        if (transform.position.magnitude > 20f)
        {
            OnDead?.Invoke(this);
        }

    }

    public void OnHit()
    {
        OnDead?.Invoke(this);
    }
}
