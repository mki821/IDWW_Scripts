using System;
using UnityEngine;

public class EnemyArcher : Enemy
{
    public EnemyStateMachine<CommonEnemyState> StateMachine { get; private set; }

    public Vector3 prevPos, nowPos;
    public Transform arrowSpawnTrm;
    public Arrow arrowPrefab;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<CommonEnemyState>();
        
        // foreach (CommonEnemyState stateEnum in Enum.GetValues(typeof(CommonEnemyState)))
        // {
        //     string typeName = stateEnum.ToString();
        //     Type t = Type.GetType($"Common{typeName}State");

        //     try
        //     {
        //         EnemyState<CommonEnemyState> state =
        //             Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<CommonEnemyState>;
        //         StateMachine.AddState(stateEnum, state);
        //     }
        //     catch (Exception ex)
        //     {
        //         Debug.LogError($"Enemy Hammer : no State found [ {typeName} ] - {ex.Message}");
        //     }
        // }

        StateMachine.AddState(CommonEnemyState.Idle, new CommonIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(CommonEnemyState.Battle, new CommonBattleState(this, StateMachine, "Battle"));
        StateMachine.AddState(CommonEnemyState.Attack, new CommonAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(CommonEnemyState.Hurt, new CommonHurtState(this, StateMachine, "Hurt"));
        StateMachine.AddState(CommonEnemyState.Dead, new CommonDeadState(this, StateMachine, "Dead"));
    }

    protected void Start()
    {
        StateMachine.Initialize(CommonEnemyState.Idle, this);
        AnimatorCompo.speed = Stat.attackSpeed.GetValue();
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void Attack()
    {
        Arrow arrow = PoolManager.Instance.Pop(ObjectPooling.PoolingType.Arrow) as Arrow;
        Vector3 dir = (prevPos - transform.position).normalized;
        float damage = Stat.GetDamage();
        arrow.Initialize(dir, damage, _whatIsPlayer, this);
        arrow.ResetItem();
        //if (Physics.Raycast(transform.position, 
        //    (prevPos - transform.position).normalized,
        //    out RaycastHit hit, damageCasterCompo.GetCastRadius(), _whatIsPlayer))
        //{
        //    if (hit.transform.TryGetComponent(out Health health))
        //    {
        //        Vector3 zero = Vector3.zero;
        //        health.ApplyDamage(damage , targetTrm.position, zero, 0 , this, DamageType.Range);
        //    }
        //}
    }

    public override void Hurt()
    {
        (MovementCompo as EnemyMovement).DisableMovementAgent();
        StateMachine.ChangeState(CommonEnemyState.Hurt);
    }

    public override void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    public override void AnimationMoveTrigger()
    {
    }

    public override void AnimationStopTrigger()
    {
    }

    public override void SetDead()
    {
        isDead = true;
        WaveManager.Instance.CurrentEnemyCount--;
        (MovementCompo as EnemyMovement).DisableMovementAgent();
        colliderCompo.enabled = false;
        StateMachine.ChangeState(CommonEnemyState.Dead);
        CanStateChangeable = false;
    }

    public override void ResetItem()
    {
        base.ResetItem();
        
        isDead = false;
        (MovementCompo as EnemyMovement).EnableMovementAgent();
        CanStateChangeable = true;
        colliderCompo.enabled = true;
        StateMachine.Initialize(CommonEnemyState.Idle, this);
    }
}
