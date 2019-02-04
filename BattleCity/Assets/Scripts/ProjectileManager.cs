using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField]
    private Projectile projectilePF;
    [SerializeField]
    private int maxProjectileCount = 3;
    private int curProjectileCount = 0;

    private void IncrementProjectileCount() 
    {
        ++curProjectileCount;
    }

    public void DecrementProjectileCount()
    {
        --curProjectileCount;
    }

    private bool AbleToShoot() 
    {
        return curProjectileCount < maxProjectileCount;
    }

    public void Shoot()
    {
        if (AbleToShoot()) 
        {
            IncrementProjectileCount();
            var projectile = Instantiate(projectilePF, transform.position, transform.rotation);
            projectile.SetMyManager(this);
        }
    }
}
