using System;
using UnityEngine;

public enum WarrokStateEnum
{
    First, //WakeUp 
    Idle, 
    Battle, 
    UnarmedScratch,
    TeleportSmash, 
    ChangePage, 
    Roar,
    EquipWeapon,
    RunSmash, 
    AxeWheelOnce,
    Dead
}

public class EnemyWarrok : Enemy
{
    public EnemyStateMachine<WarrokStateEnum> StateMachine { get; private set; }

    public Vector3 prevPos, nowPos;

    public Transform _rightHandTrm;
    public Transform _weaponBackTrm;

    public Collider baseCollider;
    public Collider teleportCollider;

    public GameObject disappearEffectPrefab;
    public GameObject showinEffectPrefab;
    public GameObject teleportSmashEffectPrefab;
    public GameObject runJumpSmashEffectPrefab;
    public Vector3 currentDir;
    public GameObject circlePrefab;

    public bool isPhaseTwo = false; 

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<WarrokStateEnum>();
        // foreach (WarrokStateEnum stateEnum in Enum.GetValues(typeof(WarrokStateEnum)))
        // {
        //     string typeName = stateEnum.ToString();
        //     Type t = Type.GetType($"Warrok{typeName}State");

        //     try
        //     {
        //         EnemyState<WarrokStateEnum> state =
        //             Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<WarrokStateEnum>;
        //         StateMachine.AddState(stateEnum, state);
        //     }
        //     catch (Exception ex)
        //     {
        //         Debug.LogError($"Enemy Hammer : no State found [ {typeName} ] - {ex.Message}");
        //     }
        // }
        StateMachine.AddState(WarrokStateEnum.First, new WarrokFirstState(this, StateMachine, "First"));
        StateMachine.AddState(WarrokStateEnum.Idle, new WarrokIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(WarrokStateEnum.Battle, new WarrokBattleState(this, StateMachine, "Battle"));
        StateMachine.AddState(WarrokStateEnum.UnarmedScratch, new WarrokUnarmedScratchState(this, StateMachine, "UnarmedScratch"));
        StateMachine.AddState(WarrokStateEnum.TeleportSmash, new WarrokTeleportSmashState(this, StateMachine, "TeleportSmash"));
        StateMachine.AddState(WarrokStateEnum.ChangePage, new WarrokChangePageState(this, StateMachine, "ChangePage"));
        StateMachine.AddState(WarrokStateEnum.Roar, new WarrokRoarState(this, StateMachine, "Roar"));
        StateMachine.AddState(WarrokStateEnum.EquipWeapon, new WarrokEquipWeaponState(this, StateMachine, "EquipWeapon"));
        StateMachine.AddState(WarrokStateEnum.RunSmash, new WarrokRunSmashState(this, StateMachine, "RunSmash"));
        StateMachine.AddState(WarrokStateEnum.AxeWheelOnce, new WarrokAxeWheelOnceState(this, StateMachine, "AxeWheelOnce"));
        StateMachine.AddState(WarrokStateEnum.Dead, new WarrokDeadState(this, StateMachine, "Dead"));
    }

    protected void Start()
    {
        StateMachine.Initialize(WarrokStateEnum.First, this);
        AnimatorCompo.speed = Stat.attackSpeed.GetValue();
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void Attack()
    {
        if (isPhaseTwo)
        {
            damageCasterCompo.CastDamageByValue(damageCasterCompo.transform.position, 2.5f, true);
        }
        else
        {
            damageCasterCompo.CastDamageByValue(damageCasterCompo.transform.position, 1.8f, true);
        }
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

    public override void Hurt()
    {
        //(MovementCompo as EnemyMovement).DisableMovementAgent();
        if (targetTrm == null)
            targetTrm = PlayerManager.Instance.Player.transform;
        if (HealthCompo.currentHealth < 800 && isPhaseTwo == false)
        {
            isPhaseTwo = true;
            attackDistance = 4;
            StateMachine.ChangeState(WarrokStateEnum.ChangePage);
        }
    }

    public override void SetDead()
    {
        WaveManager.Instance.CurrentEnemyCount--;
        //(MovementCompo as EnemyMovement).DisableMovementAgent();
        colliderCompo.enabled = false;
        StateMachine.ChangeState(WarrokStateEnum.Dead);
        isDead = true;
        CanStateChangeable = false;
    }

    public override void ResetItem()
    {
        base.ResetItem();

        isDead = false;
        CanStateChangeable = true;
        colliderCompo.enabled = true;
        (MovementCompo as EnemyMovement).EnableMovementAgent();
        StateMachine.Initialize(WarrokStateEnum.First, this);
    }
}
