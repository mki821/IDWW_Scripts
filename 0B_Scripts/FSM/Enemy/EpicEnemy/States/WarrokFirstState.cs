using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class WarrokFirstState : EnemyState<WarrokStateEnum>
{
    public WarrokFirstState(Enemy enemyBase, EnemyStateMachine<WarrokStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
            _stateMachine.ChangeState(WarrokStateEnum.Idle);
    }
}
