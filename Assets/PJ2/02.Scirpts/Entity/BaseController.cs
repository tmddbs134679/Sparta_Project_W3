using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D rb;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDir = Vector2.zero;

    public Vector2 MovementDir { get { return movementDir; } }
    protected Vector2 lookDir = Vector2.zero;
    public Vector2 LookDir { get { return lookDir; } }

    private Vector2 knockback = Vector2.zero;

    private float knockbackDuration = 0.0f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        HandleAction();
        Rotate(LookDir);
    }



    protected virtual void FixedUpdate()
    {
        Movement(movementDir);
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }


    protected virtual void HandleAction()
    {

    }


    private void Movement(Vector2 dir)
    {
        dir *= 5;
        if(knockbackDuration > 0f)
        {
            dir.y *= 0.2f;
            dir += knockback;
        }

        rb.velocity = dir;
    }

    private void Rotate(Vector2 dir)
    {
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;

        if(weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }
}
