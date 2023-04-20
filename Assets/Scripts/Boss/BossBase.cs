using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;
using JetBrains.Annotations;

public enum BossAction
{
    INIT,
    IDLE, 
    WALK, 
    ATTACK
}

public class BossBase : MonoBehaviour
{
    [Header("Animation")]
    public float startAnimationDuration = .5f;
    public Ease startAnimationEase = Ease.OutBack;

    [Header("Attack")]
    public int attackAmount = 5;
    public float timeBetweenAttacks;

    public float speed = 5f;
    public List<Transform> waypoints;

    private StateMachine<BossAction> stateMachine;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        stateMachine = new StateMachine<BossAction>();
        stateMachine.Init();

        stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
        stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
        stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
    }

    public void StartAttack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        int attacks = 0;
        while(attacks < attackAmount)
        {
            attacks++;
            transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
            yield return new WaitForEndOfFrame();
        }
    }

    public void GoToRandomPoint()
    {
        StartCoroutine(GoToPointCoroutine(waypoints[Random.Range(0, waypoints.Count)]));
    }

    IEnumerator GoToPointCoroutine(Transform t)
    {
        while (Vector3.Distance(transform.position, t.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartInitAnimation()
    {
        transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
    }

    [NaughtyAttributes.Button]
    private void SwitchInit()
    {
        SwitchSate(BossAction.INIT);
    }

    [NaughtyAttributes.Button]
    private void SwitchWalk()
    {
        SwitchSate(BossAction.WALK);
    }

    [NaughtyAttributes.Button]
    private void SwitchAttack()
    {
        SwitchSate(BossAction.ATTACK);
    }

    public void SwitchSate(BossAction state)
    {
        stateMachine.SwitchState(state, this);
    }
}
