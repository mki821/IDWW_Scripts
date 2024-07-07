using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrokRunSmashState : EnemyState<WarrokStateEnum>
{
    public WarrokRunSmashState(Enemy enemyBase, EnemyStateMachine<WarrokStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    Vector3 toward;
    Vector3 targetpOs;
    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        if (_enemyBase.targetTrm == null)
            _enemyBase.targetTrm = PlayerManager.Instance.Player.transform;
        toward = (PlayerManager.Instance.Player.transform.position - _enemyBase.transform.position).normalized;
        toward.y = 0;
        targetpOs = _enemyBase.targetTrm.position;
        _enemyBase.transform.position = targetpOs;
        _enemyBase.transform.position += -toward * 22f;
        var obj = Object.Instantiate((_enemyBase as EnemyWarrok).circlePrefab, _enemyBase.targetTrm.position + toward * 3, Quaternion.Euler(90, 0, 0));
        obj.GetComponent<WarningCircle>().SetValueAndStart(_enemyBase, 9f, 1.9f, DamageType.Melee, false);
        _enemyBase.transform.rotation
            = Quaternion.LookRotation(toward);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemyBase.transform.position -= toward * Time.deltaTime * 0.2f;
        if (_endTriggerCalled)
        {
            _enemyBase.transform.position = targetpOs;
            (_enemyBase.MovementCompo as EnemyMovement).navAgnet.Warp(_enemyBase.transform.position);
            _stateMachine.ChangeState(WarrokStateEnum.Battle);
        }
    }

}
