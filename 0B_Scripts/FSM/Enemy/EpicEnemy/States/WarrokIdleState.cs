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

        if (target == null) return; //���� ����

        Vector3 direction = target.transform.position - _enemyBase.transform.position;
        direction.y = 0;

        //�÷��̾� �߰��߰� �÷��̿� �� ���̿� ��ֹ��� ����.
        if (_enemyBase.IsObstacleInLine(direction.magnitude, direction.normalized) == false)
        {
            _enemyBase.targetTrm = target.transform;
            if (_enemyBase.CanAttack())
                _stateMachine.ChangeState(WarrokStateEnum.Battle);
        }
    }
}
