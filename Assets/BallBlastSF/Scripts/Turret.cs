using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float firerate;
    [SerializeField] private int damage;

    [Header("Projectile")]
    [SerializeField] private int projectileAmount;
    [SerializeField] private float projectileInterval;

    public int Damage => damage;
    public int ProjectileAmount => projectileAmount;
    public float FireRate => firerate;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

    }

    public void Fire()
    {
        if (timer >= firerate)
        {
            SpawnProjectile();
            timer = 0;
        }
    }

    private void SpawnProjectile()
    {
        float startPosX = transform.position.x - projectileInterval * (projectileAmount - 1) * 0.5f;

        for (int i = 0; i < projectileAmount; i++)
        {
            Projectile projectile = Instantiate(projectilePrefab, new Vector3(startPosX + i * projectileInterval, shotPoint.position.y, shotPoint.position.z), transform.rotation);
            projectile.SetDamage(damage);
        }
    }

    public void AddDamage( int damage)
    {
        this.damage += damage;
    }

    public void RemoveFireRate(float rate)
    {      
        if(firerate == 0.2f) return;
        firerate -= rate;   
        
        if(firerate < 0.2f) firerate = 0.2f;
    }

    public void AddProjectile(int amount)
    {
        if (projectileAmount >= 5) return;
        projectileAmount += amount;
    }

    public void SetDamage(int damage)
    {
        if(damage <= 0) return;
        this.damage = damage;
    }

    public void SetFireRate(float fireRate)
    {
        if (this.firerate == 0.2f) return ;
        this.firerate = fireRate;
    }

    public void SetProjectileAmount(int projectileAmount)
    {
        if (projectileAmount == 1) return;
       this.projectileAmount = projectileAmount;
    }
}
