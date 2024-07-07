using System;
using System.Collections;
using UnityEngine;

public enum PlayerStateEnum {
    Idle, Run, Cast, Dead
}

public class Player : Agent
{
    [HideInInspector] public bool moveable = true;

    [SerializeField] private LayerMask _whatIsGround;
    [HideInInspector] public Vector3 destination;

    [HideInInspector] public Stamina stamina;

    [Header("Input Setting")]
    [SerializeField] private InputReader _inputReader;

    private Coroutine _moveCoroutine;

    public PlayerStateMachine StateMachine { get; private set; }

    protected override void Awake() {
        base.Awake();

        destination = transform.position;

        stamina = GetComponent<Stamina>();

        StateMachine = new PlayerStateMachine();

        // foreach(PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum))) {
        //     string typeName = stateEnum.ToString();

        //     try {
        //         Type t = Type.GetType($"Player{typeName}State");
        //         PlayerState state = Activator.CreateInstance(t, this, StateMachine, typeName) as PlayerState;

        //         StateMachine.AddState(stateEnum, state);
        //     }
        //     catch(Exception ex) {
        //         Debug.LogError($"[PlayerState] {typeName} is loading error");
        //         Debug.LogError(ex.Message);
        //     }
        // }

        StateMachine.AddState(PlayerStateEnum.Idle, new PlayerIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(PlayerStateEnum.Run, new PlayerRunState(this, StateMachine, "Run"));
        StateMachine.AddState(PlayerStateEnum.Cast, new PlayerCastState(this, StateMachine, "Cast"));
        StateMachine.AddState(PlayerStateEnum.Dead, new PlayerDeadState(this, StateMachine, "Dead"));

        PlayerManager.Instance.SetPlayer(this);

        _inputReader.MoveAction += HandleMove;
        _inputReader.StopAction += MovementCompo.StopImmediately;
    }

    private void OnDisable() {
        _inputReader.MoveAction -= HandleMove;
        _inputReader.StopAction -= MovementCompo.StopImmediately;
    }

    private void Start() {
        StateMachine.Initialize(PlayerStateEnum.Idle, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    private void HandleMove(bool flag) {
        if(flag) {
            if(_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _moveCoroutine = StartCoroutine(MoveCoroutine());
        }
        else if(_moveCoroutine != null) StopCoroutine(_moveCoroutine);
    }

    private IEnumerator MoveCoroutine() {
        while(true) {
            Move();
            yield return null;
        }
    }

    private void Move() {
        if(!moveable) return;

        Ray ray = Camera.main.ScreenPointToRay(_inputReader.AimPosition);
        if(Physics.Raycast(ray, out RaycastHit hit, 30f, _whatIsGround)) {
            destination = hit.point;
            MovementCompo.SetDestination(destination);
        }
    }

    public void VisibleVisual(bool flag) {
        transform.Find("Visual").gameObject.SetActive(flag);
    }

    public override void SetDead() {
        StateMachine.ChangeState(PlayerStateEnum.Dead);
        isDead = true;
        UIManager.Instance.ShowDead();
    }
}
