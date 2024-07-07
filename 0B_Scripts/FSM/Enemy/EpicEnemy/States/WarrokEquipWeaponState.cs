using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrokEquipWeaponState : EnemyState<WarrokStateEnum>
{
    EnemyWarrok _warrok;
    public WarrokEquipWeaponState(Enemy enemyBase, EnemyStateMachine<WarrokStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _warrok = _enemyBase as EnemyWarrok;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_equipWeaponCalled)
        {
            Transform axe = _warrok._weaponBackTrm.Find("Axe");
            axe.transform.parent = _warrok._rightHandTrm;
            axe.localPosition = new Vector3(0,0,0);
            axe.localRotation = Quaternion.Euler(0,0,0);

            _stateMachine.ChangeState(WarrokStateEnum.Roar);
        }
    }
}
