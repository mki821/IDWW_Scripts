using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrokBattleState : EnemyState<WarrokStateEnum>
{
    private Vector3 _targetDestination;
    private float followTime;
    private float inRangeTime;

    public WarrokBattleState(Enemy enemyBase, EnemyStateMachine<WarrokStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }



    public override void Enter()
    {
        base.Enter();
        followTime = Time.time;
        SetDestination(_enemyBase.targetTrm.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        float distance = (_targetDestination - _enemyBase.targetTrm.position).magnitude;

        if (Time.time - followTime > 3f)
        {
            if ((_enemyBase as EnemyWarrok).isPhaseTwo)
            {
                _stateMachine.ChangeState(WarrokStateEnum.RunSmash);
            }
            else
            {
                _stateMachine.ChangeState(WarrokStateEnum.TeleportSmash);
            }
        }

        if (_enemyBase.useMoveTrigger == false)
            SetDestination(_enemyBase.targetTrm.position);
        else
        {
            if (_moveTriggerCalled)
            {
                SetDestination(_enemyBase.targetTrm.position);
            }

            if (_stopTriggerCalled)
            {
                (_enemyBase.MovementCompo as EnemyMovement).StopImmediately();
            }
        }

        float targetDistance = Vector3.Distance(
            _enemyBase.targetTrm.position, _enemyBase.transform.position);

        if (targetDistance <= _enemyBase.attackDistance)
        {
            if (Time.time - inRangeTime < 0.2f) return;
            if (_enemyBase.CanAttack())
                if((_enemyBase as EnemyWarrok).isPhaseTwo)
                _stateMachine.ChangeState(WarrokStateEnum.AxeWheelOnce);
            else
                _stateMachine.ChangeState(WarrokStateEnum.UnarmedScratch);
        }
        else
        {
            inRangeTime = Time.time;
        }

    }

    private void SetDestination(Vector3 position)
    {
        _targetDestination = position;
        _enemyBase.MovementCompo.SetDestination(position);
    }
}
