using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<UIGunUpdater> uIGunUpdaters;

    public GunBase gunBase;
    public Transform gunPosition;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBase, gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        Debug.Log("Cancel Shoot");
        _currentGun.StopShoot();
    }

}
