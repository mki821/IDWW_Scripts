using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrokUnarmedScratchState : EnemyState<WarrokStateEnum>
{
    public WarrokUnarmedScratchState(Enemy enemyBase, EnemyStateMachine<WarrokStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        if (_enemyBase.targetTrm == null)
            _enemyBase.targetTrm = PlayerManager.Instance.Player.transform;
        Vector3 toward = PlayerManager.Instance.Player.transform.position - _enemyBase.transform.position;

        toward.y = 0;
        _enemyBase.transform.rotation
            = Quaternion.LookRotation(toward);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
            _stateMachine.ChangeState(WarrokStateEnum.Battle);
    }
}
