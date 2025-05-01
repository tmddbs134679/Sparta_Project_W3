using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;

    [SerializeField] private float followRange = 15f;

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }


    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    protected override void HandleAction()
    {
        base.HandleAction();
        
        if(weaponHandler == null || target == null ) 
        {
            if(!movementDir.Equals(Vector2.zero))
            {
                movementDir = Vector2.zero;
                return;
            }
        }

        float dis = DistanceToTarget();
        Vector2 dir = DirectionToTarget();

        isAttacking = false;

        if(dis <= followRange)
        {
            lookDir = dir;

            if(dis < weaponHandler.attackRange)
            {
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                                     dir,
                                                     weaponHandler.attackRange * 1.5f,
                                                     (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                if(hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }

                movementDir = Vector2.zero;
                return;
            }

            movementDir = dir;
        }
    }

    public override void Death()
    {
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
    }
}
