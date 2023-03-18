using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public Transform playerSideReverse;
    public AudioSource gunShootSound;

    public float timeBetweenShoot = .3f;

    private Coroutine _currentCoroutine;

    private void Awake()
    {
        playerSideReverse = GameObject.FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }
    }

    /// <summary>
    /// Waits for a set amount of time before instantiating a new projectile.
    /// </summary>
    /// <returns></returns>
    IEnumerator StartShoot()
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
        if (gunShootSound != null) gunShootSound.Play();

        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        //projectile.side = playerSideReverse.transform.localScale.x; 
        projectile.side = GameObject.Find("Player").transform.localScale.x;
    }
}
