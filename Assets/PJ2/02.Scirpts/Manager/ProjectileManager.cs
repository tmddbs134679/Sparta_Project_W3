using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
     private static ProjectileManager instance;

     public static ProjectileManager Instance {  get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;

    private void Awake()
    {
        instance = this; 
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPos, Vector2 dir)
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIdx];
        GameObject obj = Instantiate(origin, startPos, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(dir, rangeWeaponHandler);
    }

}
