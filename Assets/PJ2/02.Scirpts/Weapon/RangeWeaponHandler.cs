using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPos;

    [SerializeField] private int bulletIdx;

    public int BulletIdx {  get { return bulletIdx; } }

    [SerializeField] private float bulletSize = 1f;
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;

    public float Duration { get { return duration; } }

    [SerializeField] private float spread;
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;

    public int NumberofProjectilesPerShot { get {  return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectileAngle;

    public float MultipleProjectileAngle { get {  return multipleProjectileAngle; } }

    [SerializeField] private Color projectileColor;

    public Color ProjectileColor { get {  return projectileColor; } }

    private ProjectileManager projectileManager;


    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }
    public override void Attack()
    {
        base.Attack();

        float projectileAngleSpace = multipleProjectileAngle;
        int numOfProjectilePerShot = numberofProjectilesPerShot;

        float minAngle = -(numOfProjectilePerShot / 2f) * projectileAngleSpace;

        for(int i =0; i < numberofProjectilesPerShot; i++)
        {
            float angle = minAngle + projectileAngleSpace * i;
            float ranSpread = Random.Range(-spread, spread);
            angle += ranSpread;
            CreateProjectile(Controller.LookDir, angle);
        }

    }

    private void CreateProjectile(Vector2 _lookDir, float angle)
    {
        projectileManager.ShootBullet
            (
              this,
              projectileSpawnPos.position,
              RotateVector2(_lookDir, angle)
            );
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }


}
