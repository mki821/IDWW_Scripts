using UnityEngine;

public class CommonHurtState : EnemyState<CommonEnemyState>
{
    public CommonHurtState(Enemy enemyBase, EnemyStateMachine<CommonEnemyState> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        _enemyBase.MovementCompo.StopImmediately();
        if(_enemyBase.targetTrm == null)
            _enemyBase.targetTrm = PlayerManager.Instance.Player.transform;
        Vector3 toward = PlayerManager.Instance.Player.transform.position - _enemyBase.transform.position;

        toward.y = 0;
        _enemyBase.transform.rotation
            = Quaternion.LookRotation(toward);
    }

    public override void UpdateState()
    {
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(CommonEnemyState.Battle);
        }
        //���� �ִϸ��̼� ��� ����Ǿ��ٸ� Battle���·� ������ ��.
    }

    public override void Exit()
    {
        if(_enemyBase.isDead)
            (_enemyBase.MovementCompo as EnemyMovement).DisableMovementAgent();
        else
            (_enemyBase.MovementCompo as EnemyMovement).EnableMovementAgent();
            
        base.Exit();
    }

}