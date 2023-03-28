using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    public Collider collider;
    public FlashColor flashColor;
    public ParticleSystem particleSystem;

    public float startLife = 10f;

    [SerializeField] private float _currentLife;

    [Header("Animation")]
    [SerializeField] private AnimationBase _animationBase;

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
        if(collider != null) collider.enabled = false;
        Destroy(gameObject, 3f);
        PlayAnimationByTrigger(AnimationType.DEATH);
    }

    public void OnDamage(float f)
    {
        if (flashColor != null) flashColor.Flash();
        if (particleSystem != null) particleSystem.Emit(15);

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

    public void PlayAnimationByTrigger(AnimationType animationType)
    {
        _animationBase.PlayAnimationTrigger(animationType);
    }

    // Debug
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnDamage(5f);
        }
    }

    public void Damage(float damage)
    {
        Debug.Log("Damage");
        OnDamage(damage);
    }
}
