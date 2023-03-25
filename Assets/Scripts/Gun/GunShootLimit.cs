using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIGunUpdater> UIGunUpdaters;

    public float maxShoot = 5f;
    public float timeToRecharge = .3f;

    private float _currentShoots;
    private bool _recharging = false;

    // When instanced, get all UIs.
    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_recharging) yield break;
        
        // While number of current shoots is lower than limit, allow player to shoot
        while(true)
        {
            if(_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentShoots >= maxShoot)
        {
            StopShoot(); 
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine()); 
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while(time < timeToRecharge) 
        {
            time += Time.deltaTime;
            Debug.Log("Recharging: " + time);
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _recharging = false;
    }

    
    private void UpdateUI()
    // Fills the gun UI based on how much ammo is left.
    {
        UIGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    private void GetAllUIs()
    {
        UIGunUpdaters = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
    }
}
