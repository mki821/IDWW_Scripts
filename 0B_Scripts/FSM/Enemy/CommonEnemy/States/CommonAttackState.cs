using UnityEngine;

public class CommonAttackState : EnemyState<CommonEnemyState>

{
    public CommonAttackState(Enemy enemyBase, EnemyStateMachine<CommonEnemyState> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //if(_enemyBase.PoolingType == ObjectPooling.PoolingType.Archer)
        //    SoundManager.Instance.PlaySFX("archerBowLoading");

        if (_enemyBase is EnemyArcher enemyArcher)
        {
            enemyArcher.prevPos = _enemyBase.targetTrm.position;
        }

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
            _stateMachine.ChangeState(CommonEnemyState.Battle);
        }
        //���� �ִϸ��̼� ��� ����Ǿ��ٸ� Battle���·� ������ ��.
    }
}
