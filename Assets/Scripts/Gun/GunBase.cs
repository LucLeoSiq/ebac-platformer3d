using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;

    public float timeBetweenShoot = .3f;

    private Coroutine _currentCoroutine;

    /// <summary>
    /// Waits for a set amount of time before instantiating a new projectile.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    /// <summary>
    /// Instantiate a projectile from a specific point.
    /// </summary>
    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile); 
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }
}
