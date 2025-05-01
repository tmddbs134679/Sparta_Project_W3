using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
 

 

    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
    }

    void OnMove(InputValue iunputValue)
    {
        movementDir = iunputValue.Get<Vector2>();
        movementDir = movementDir.normalized;
    }

    void OnLook(InputValue iunputValue)
    {

        Vector2 mousePos = iunputValue.Get<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePos);
        lookDir = worldPos - (Vector2)transform.position;

        if (lookDir.magnitude < 0.9f)
        {
            lookDir = Vector2.zero;
        }
        else
        {
            lookDir = lookDir.normalized;
        }

    }

    void OnFire(InputValue inputValue)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        isAttacking = inputValue.isPressed;
    }
}
