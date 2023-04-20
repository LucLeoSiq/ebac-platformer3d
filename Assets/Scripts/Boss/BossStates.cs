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
        boss.GoToRandomPoint();
    }
}
public class BossStateAttack : BossStateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        boss.StartAttack();
    }
}