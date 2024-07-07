using UnityEngine;

public class CommonIdleState : EnemyState<CommonEnemyState>
{
    public CommonIdleState(Enemy enemyBase, EnemyStateMachine<CommonEnemyState> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {

        Collider target = _enemyBase.IsPlayerDetected();

        if (target == null) return; //���� ����

        Vector3 direction = target.transform.position - _enemyBase.transform.position;
        direction.y = 0;


        
        //�÷��̾� �߰��߰� �÷��̿� �� ���̿� ��ֹ��� ����.
        if (_enemyBase.IsObstacleInLine(direction.magnitude, direction.normalized) == false)
        {
            _enemyBase.targetTrm = target.transform;
            if (_enemyBase.PoolingType == ObjectPooling.PoolingType.Archer)
            {
                if (_enemyBase.IsInAttackRange((_enemyBase as EnemyArcher).attackDistance))
                {
                    (_enemyBase as EnemyArcher).MovementCompo.StopImmediately();
                }
            }
            if (_enemyBase.CanAttack())
            {
                _stateMachine.ChangeState(CommonEnemyState.Battle);
            }
        }
    }
}
