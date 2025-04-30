using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
     private static ProjectileManager instance;

     public static ProjectileManager Instance {  get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private ParticleSystem impactParticleSystem;
    private void Awake()
    {
        instance = this; 
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPos, Vector2 dir)
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIdx];
        GameObject obj = Instantiate(origin, startPos, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(dir, rangeWeaponHandler, this);
    }

    public void CreateImpactParticlesAtPos(Vector3 pos, RangeWeaponHandler weaponHandler)
    {
        impactParticleSystem.transform.position = pos;
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));

        ParticleSystem.MainModule mainModule = impactParticleSystem.main;
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;

        impactParticleSystem.Play();
    }

}
