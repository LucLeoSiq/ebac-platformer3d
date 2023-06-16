using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<UIFillUpdater> uIGuUpdaters;
    public SFXType sfxType;

    public GunBase gunBase;
    public Transform gunPosition;

    private GunBase _currentGun;
    public FlashColor _flashColor;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();
    }

    private void PlaySFX()
    {
        SFXPool.Instance.Play(sfxType);
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBase, gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        PlaySFX();
        _currentGun.StartShoot();
        _flashColor ?. Flash();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        Debug.Log("Cancel Shoot");
        _currentGun.StopShoot();
    }

}
