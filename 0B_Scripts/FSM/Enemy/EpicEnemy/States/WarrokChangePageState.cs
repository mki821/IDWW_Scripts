using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrokChangePageState : EnemyState<WarrokStateEnum>
{
    public WarrokChangePageState(Enemy enemyBase, EnemyStateMachine<WarrokStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    //EnemyWarrok _warrok;

    public override void Enter()
    {
        base.Enter();
        //_warrok = _enemyBase as EnemyWarrok;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
            _stateMachine.ChangeState(WarrokStateEnum.EquipWeapon);
    }
}
