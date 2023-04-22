using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using System;

public class BossStateBase : StateBase
{
    protected BossBase boss;

    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        boss = (BossBase)objs[0];
    }
}

public class BossStateInit : BossStateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        boss.StartInitAnimation();
    }

}

public class BossStateWalk : BossStateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        boss.GoToRandomPoint(OnArrive);
    }

    private void OnArrive()
    {
        boss.SwitchState(BossAction.ATTACK);
    }
    
    public override void OnStateExit()
    {
        base.OnStateExit();
        boss.StopAllCoroutines();
    }
}

public class BossStateAttack : BossStateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        boss.StartAttack(EndAttacks);
    }

    private void EndAttacks()
    {
        boss.SwitchState(BossAction.WALK);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        Debug.Log("Exit Attack");
        boss.StopAllCoroutines();
    }

    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            Debug.Log("Enter Death");
            boss.transform.localScale = Vector3.one * .2f;
        }
    }

}