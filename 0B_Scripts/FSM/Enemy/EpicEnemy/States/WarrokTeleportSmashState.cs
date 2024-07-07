using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrokTeleportSmashState : EnemyState<WarrokStateEnum>
{
    public WarrokTeleportSmashState(Enemy enemyBase, EnemyStateMachine<WarrokStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
        (_enemyBase as EnemyWarrok).currentDir = toward;
        var obj = Object.Instantiate((_enemyBase as EnemyWarrok).circlePrefab, _enemyBase.targetTrm.position, Quaternion.Euler(90,0,0));
        obj.GetComponent<WarningCircle>().SetValueAndStart(_enemyBase,5f, 1.5f, DamageType.Melee,true);
        _enemyBase.transform.position = _enemyBase.targetTrm.position;
        _enemyBase.transform.position += -toward.normalized * 12f;
        _enemyBase.transform.rotation
            = Quaternion.LookRotation(toward);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_endTriggerCalled)
        {
            (_enemyBase.MovementCompo as EnemyMovement).navAgnet.Warp(_enemyBase.transform.position);
            _stateMachine.ChangeState(WarrokStateEnum.Battle);
        }
    }

}
