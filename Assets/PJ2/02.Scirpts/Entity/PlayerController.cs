using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera camera;
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        camera = Camera.main;
    }
    protected override void Start()
    {
        base.Start();
      
    }
  
    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movementDir = new Vector2(horizontal, vertical).normalized;

        Vector2 mousePos = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePos);
        lookDir = worldPos - (Vector2)transform.position;

        if(lookDir.magnitude < 0.9f)
        {
            lookDir = Vector2.zero;
        }
        else
        {
            lookDir = lookDir.normalized;
        }

        isAttacking = Input.GetMouseButton(0);
    }


}
