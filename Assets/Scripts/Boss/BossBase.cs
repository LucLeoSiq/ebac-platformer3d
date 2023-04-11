using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public enum BossAction
{
    INIT,
    IDLE, 
    WALK, 
    ATTACK
}

public class BossBase : MonoBehaviour
{
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
                                                         
    }

    [NaughtyAttributes.Button]
    private void SwitchInit()
    {
        SwitchSate(BossAction.INIT);
    }

    public void SwitchSate(BossAction state)
    {
        stateMachine.SwitchState(state, this);
    }
}
