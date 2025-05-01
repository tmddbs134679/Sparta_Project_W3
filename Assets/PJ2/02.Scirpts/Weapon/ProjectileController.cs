using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;
    private RangeWeaponHandler rangeWeaponHandler;

    private float currentDuration;
    private Vector2 dir;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestroy = true;

    ProjectileManager projectileManager;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady) return;

        currentDuration += Time.deltaTime;

        if(currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        rb.velocity = dir * rangeWeaponHandler.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - dir * 0.2f, fxOnDestroy);
        }
        else if(rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {

            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-rangeWeaponHandler.Power);
                if(rangeWeaponHandler.IsKnockback)
                {
                    BaseController controller = collision.GetComponent<BaseController>();
                    if(controller != null )
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                    }
                }
            }

            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }

    }
    private void DestroyProjectile(Vector3 pos, bool createFx)
    {
        if(createFx)
        {
            projectileManager.CreateImpactParticlesAtPos(pos, rangeWeaponHandler);
        }

        Destroy(this.gameObject);
    }

    public void Init(Vector2 dir, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {
        this.projectileManager = projectileManager;
        rangeWeaponHandler = weaponHandler;
        this.dir = dir; 
        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        transform.right = this.dir;

        if (dir.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }
}
