using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float flapForce = 6f;
    public float forwardWpeed = 3f;
    private float deatCoolDown = 0f;

    bool isFlap = false;
    bool isDead = false;
    public bool godMode = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();   

        if(animator == null)
            Debug.LogError("Not Founded Animator");

        if(rb == null)
            Debug.LogError("Not Founded Rigidbody2D");

    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            if(deatCoolDown <= 0)
            {

            }
            else
            {
                deatCoolDown -= Time.deltaTime;
            }
        }
        else
        {
            //Input.GetMouseButtonDown(0) : 스마트폰 터치까지
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = rb.velocity;
        velocity.x = forwardWpeed;

        if(isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        rb.velocity = velocity;

        float angle = Mathf.Clamp((rb.velocity.y * 10), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;

        if (isDead) return;

        isDead = true;
        deatCoolDown = 1f;

        animator.SetInteger("IsDie", 1);
    }
}
