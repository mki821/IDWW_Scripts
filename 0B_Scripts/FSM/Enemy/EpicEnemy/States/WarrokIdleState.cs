using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrokIdleState : EnemyState<WarrokStateEnum>
{
    public WarrokIdleState(Enemy enemyBase, EnemyStateMachine<WarrokStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        base.UpdateState();

        Collider target = _enemyBase.IsPlayerDetected();

        if (target == null) return; //적이 없어

        Vector3 direction = target.transform.position - _enemyBase.transform.position;
        direction.y = 0;

        //플레이어 발견했고 플레이와 나 사이에 장애물도 없다.
        if (_enemyBase.IsObstacleInLine(direction.magnitude, direction.normalized) == false)
        {
            _enemyBase.targetTrm = target.transform;
            if (_enemyBase.CanAttack())
                _stateMachine.ChangeState(WarrokStateEnum.Battle);
        }
    }
}
