using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CommonEnemyState
{
    Idle,
    Battle,
    Attack,
    Hurt,
    Dead
}

public class EnemyZombie : Enemy
{
    public EnemyStateMachine<CommonEnemyState> StateMachine { get; private set; }

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
        if(PoolingType == ObjectPooling.PoolingType.Zombie)
            SoundManager.Instance.PlaySFX($"zombie{Random.Range(1, 4)}");
        else SoundManager.Instance.PlaySFX("zombieAttackSwish");
        
        damageCasterCompo.CastDamage();
    }

    public override void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }
    public override void AnimationMoveTrigger()
    {
        StateMachine.CurrentState.AnimationMoveTrigger();
    }
    public override void AnimationStopTrigger()
    {
        StateMachine.CurrentState.AnimationStopTrigger();
    }

    public override void Hurt()
    {
        (MovementCompo as EnemyMovement).DisableMovementAgent();
        StateMachine.ChangeState(CommonEnemyState.Hurt);
    }

    public override void SetDead()
    {
        WaveManager.Instance.CurrentEnemyCount--;
        (MovementCompo as EnemyMovement).DisableMovementAgent();
        isDead = true;
        colliderCompo.enabled = false;
        StateMachine.ChangeState(CommonEnemyState.Dead);
        CanStateChangeable = false;
    }

    public override void ResetItem()
    {
        base.ResetItem();
        
        isDead = false;
        CanStateChangeable = true;
        colliderCompo.enabled = true;
        (MovementCompo as EnemyMovement).EnableMovementAgent();
        StateMachine.Initialize(CommonEnemyState.Idle, this);
    }
}
