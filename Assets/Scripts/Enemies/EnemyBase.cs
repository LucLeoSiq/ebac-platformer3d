using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float startLife = 10f;

    [SerializeField] private float _currentLife;

    [Header("Start Animation")]
    public float startAnimationDuration = .2f;
    public Ease startAnimationEase = Ease.OutBack;
    public bool startWithBornAnimation = true;

    protected void Awake()
    {
        Init();
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Init()
    {
        ResetLife();

        if(startWithBornAnimation)
            BornAnimation();
    }

    protected virtual void Kill()
    {
        OnKill();
    }

    protected virtual void OnKill() 
    { 
        Destroy(gameObject);
    }

    public void OnDamage(float f)
    {
        _currentLife -= f;
        
        if(_currentLife <= 0)
        {
            Kill();
        }
    }

    private void BornAnimation()
    {
        transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
    }

    // Debug
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnDamage(5f);
        }
    }
}