using UnityEngine;

public class CommonBattleState : EnemyState<CommonEnemyState>
{
    private Vector3 _targetDestination; //��ǥ����

    public CommonBattleState(Enemy enemyBase, EnemyStateMachine<CommonEnemyState> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetDestination(_enemyBase.targetTrm.position);
    }

    public override void UpdateState()
    {
        //_enemyBase.AnimatorCompo.speed = _enemyBase.Stat.moveSpeed.GetValue();
        //float distance = (_targetDestination - _enemyBase.targetTrm.position).magnitude;

        if (_enemyBase.useMoveTrigger == false) 
            SetDestination(_enemyBase.targetTrm.position);
        else
        {
            if (_moveTriggerCalled)
            {
                Debug.LogWarning("StopTriggerCalled");
                SetDestination(_enemyBase.targetTrm.position);
            }

            if (_stopTriggerCalled)
            {
                Debug.LogWarning("StopTriggerCalled");
                (_enemyBase.MovementCompo as EnemyMovement).StopImmediately();
            }
        }



        //���� ���� ���� �Ÿ��� �� �����̶�� AttackState�� ����
        //�ٸ� ����� ��Ÿ���� �Ű澲������ 
        float targetDistance = Vector3.Distance(
            _enemyBase.targetTrm.position, _enemyBase.transform.position);

        if (targetDistance <= _enemyBase.attackDistance)
        {
            if (_enemyBase.CanAttack())
                _stateMachine.ChangeState(CommonEnemyState.Attack);
            else
                _stateMachine.ChangeState(CommonEnemyState.Idle);
        }

    }

    private void SetDestination(Vector3 position)
    {
        _targetDestination = position;
        _enemyBase.MovementCompo.SetDestination(position);
    }
}
