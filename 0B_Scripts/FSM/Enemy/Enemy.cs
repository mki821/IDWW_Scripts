using UnityEngine;
using ObjectPooling;
using System;
using UnityEngine.Experimental.Rendering;

public abstract class Enemy : Agent, IPoolable
{
    [Header("Common Settings")]
    public float battleTime; //������ �ɸ� �ð�
    public bool isActive;
    public bool useMoveTrigger = false; 
    public float moveSpeed;

    protected float _defaultMoveSpeed;

    [SerializeField] protected LayerMask _whatIsPlayer;
    [SerializeField] protected LayerMask _whatIsObstacle;

    [Header("Attack Settings")]
    public float runAwayDistance; //��׷ΰ� ������ ������ �Ÿ�
    public float attackDistance;
    public float attackCooldown;
    [SerializeField] protected int _maxCheckEnemy = 1;
    [HideInInspector] public float lastAttackTime;
    [HideInInspector] public Transform targetTrm;
    [HideInInspector] public SkinnedMeshRenderer meshRenderer;
    [HideInInspector] public CapsuleCollider colliderCompo;
    [HideInInspector] public OverlapDamageCaster damageCasterCompo;

    [SerializeField] private HealthBar _healthBar;

    protected Collider[] _enemyCheckCollider;

    [SerializeField] private PoolingType _poolingType;
    public PoolingType PoolingType 
    { get => _poolingType; set => _poolingType = value; }

    public GameObject GameObject => gameObject;

    public IPoolable Poolable => this;

    protected override void Awake()
    {
        base.Awake();
        _defaultMoveSpeed = moveSpeed; //�̵��ӵ� ����
        _enemyCheckCollider = new Collider[_maxCheckEnemy];

        damageCasterCompo = transform.Find("DamageCaster").GetComponent<OverlapDamageCaster>();
        damageCasterCompo.InitCaster(this);
        meshRenderer = transform.Find("Visual").GetComponentInChildren<SkinnedMeshRenderer>();
        colliderCompo = GetComponent<CapsuleCollider>();
    }

    public virtual void Attack()
    {

    }

    public virtual void Hurt()
    {

    }

    public virtual Collider IsPlayerDetected()
    {
        int cnt = Physics.OverlapSphereNonAlloc(transform.position, runAwayDistance, _enemyCheckCollider, _whatIsPlayer);
        return cnt == 1 ? _enemyCheckCollider[0] : null;
    }

    public virtual bool IsInAttackRange(float distance)
    {
        return Vector3.Distance(transform.position, targetTrm.position) < distance;
    }

    public virtual bool IsObstacleInLine(float distance, Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, distance, _whatIsObstacle);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, runAwayDistance); //�� �����Ÿ�
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.white;
    }

    public abstract void AnimationEndTrigger();
    public abstract void AnimationStopTrigger();
    public abstract void AnimationMoveTrigger();
    public virtual void ResetItem()
    {
        _healthBar.ResetItem();
        transform.Find("Symbol").gameObject.SetActive(true);
    }

    public bool CanAttack() => Time.time - lastAttackTime > attackCooldown;
}
