using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactoryManager : MonoBehaviour
{
    public static ProjectileFactoryManager Instance;
    [SerializeField] private Transform poolObjects;
    private PoolFactory<Projectile> projectilePool;
    [SerializeField] private Projectile projectilePrefab;
    private readonly int PROJECTILECOUNT = 10;

    private void Awake()
    {
        Instance = this;

        if (poolObjects == null)
        {
            poolObjects = new GameObject("ProjectilePoolObjests").transform;
        }
        projectilePool = new PoolFactory<Projectile>(projectilePrefab.gameObject, poolObjects, PROJECTILECOUNT);
    }

    public Projectile SpawnProjectile(Vector2 pos, Vector2 dir)
    {
        var projectile = projectilePool.Get(pos);
        projectile.Fire(dir);
        projectile.OnDead += (projectile) => projectilePool.Return(projectile);

        return projectile;
    }



}
