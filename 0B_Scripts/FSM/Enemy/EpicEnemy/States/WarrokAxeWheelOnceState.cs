using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrokAxeWheelOnceState : EnemyState<WarrokStateEnum>
{
    public WarrokAxeWheelOnceState(Enemy enemyBase, EnemyStateMachine<WarrokStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();

        Vector3 toward = _enemyBase.targetTrm.position - _enemyBase.transform.position;

        toward.y = 0;
        _enemyBase.transform.rotation
            = Quaternion.LookRotation(toward);
    }

    public override void Exit()
    {
        _enemyBase.lastAttackTime = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(WarrokStateEnum.Battle);
        }
    }
}
