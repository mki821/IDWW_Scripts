using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class WarrokDeadState : EnemyState<WarrokStateEnum>
{
    public WarrokDeadState(Enemy enemyBase, EnemyStateMachine<WarrokStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _enemyBase.MovementCompo.StopImmediately();
        _enemyBase.StartCoroutine(DeadRoutine());
    }

    private IEnumerator DeadRoutine()
    {
        yield return new WaitUntil(() => _endTriggerCalled);
        
        UIManager.Instance.SetClear();

        PoolManager.Instance.Push(_enemyBase);
    }
}
