using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

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
        boss.SwitchSate(BossAction.ATTACK);
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
        boss.SwitchSate(BossAction.WALK);
    }

    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
        }
    }

}