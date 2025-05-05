using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxTravelDistance = 5;
    private Vector2 dir;
    public Action<Projectile> OnDead;


    private void Awake()
    {
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)dir * speed * Time.deltaTime;


        if (transform.position.magnitude > maxTravelDistance)
        {
            OnDead?.Invoke(this);
        }


    }
    public void Fire(Vector2 dir)
    {
        this.dir = dir;
        OnDead = null;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Monster"))
        {
            DumbMonster monster = collision.GetComponent<DumbMonster>();

            if(monster != null )
            {
                monster.OnHit();
            }

            OnDead?.Invoke(this);
        }
    }

}
