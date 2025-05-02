using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbMonster : MonoBehaviour
{
    private Vector3 moveDirection;
    public float speed = 3f;

    public void Init(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
