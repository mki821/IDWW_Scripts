using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDeadState : EnemyState<CommonEnemyState>
{
    //private readonly int _dissolveHash = Shader.PropertyToID("_DissolveHeight");
    //private float _dissolveTime = 1f;
    //private bool _isDissolving = false; // ���������?�� ?
    public CommonDeadState(Enemy enemyBase, EnemyStateMachine<CommonEnemyState> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //_isDissolving = false;
        //
        //int gold = _enemyBase.dropTable.GetDropGold();
        //int exp = _enemyBase.dropTable.dropExp;
        //
        //PlayerManager.Instance.AddExp(exp);

        //for(int i = 0; i < gold ; i++)
        //{
        //    Item coin = PoolManager.Instance.Pop(ObjectPooling.PoolingType.CoinItem) as Item;
        //    direction = Quaternion.Euler(0, Random.Range(-30f, 30f), 0) * direction;
        //    coin.DropItem(_enemyBase.transform.position, direction);
        //}
        _enemyBase.MovementCompo.StopImmediately();
    }

    public override void UpdateState() {
        if(_endTriggerCalled)
            PoolManager.Instance.Push(_enemyBase);
    }
}
