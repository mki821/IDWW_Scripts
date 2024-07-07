using Cinemachine;
using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IPoolable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    private Enemy _owner;
    private LayerMask _whatIsPlayer;

    public Arrow(Vector3 dir, float damage, LayerMask targetLayer, Enemy enemy)
    {
        transform.right = dir;
        _damage = damage;
        _whatIsPlayer = targetLayer;
        _owner = enemy;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    [SerializeField] private PoolingType _poolingType;
    public PoolingType PoolingType
    { get => _poolingType; set => _poolingType = value; }

    public GameObject GameObject => gameObject;

    public IPoolable Poolable => this;

    public void Initialize(Vector3 dir, float damage, LayerMask targetLayer, Enemy enemy)
    {
        transform.right = dir;
        _damage = damage;
        _whatIsPlayer = targetLayer;
        _owner = enemy;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }


    private void Update()
    {
        transform.position += _speed * Time.deltaTime * transform.right;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == _whatIsPlayer)
        {
            if (other.TryGetComponent(out IDamageable health))
            {

                health.ApplyDamage(_damage, other.transform.position, Vector3.zero, 0, _owner, DamageType.Range);
                PoolManager.Instance.Push(this);
            }
        }

    }

    public void ResetItem()
    {
        if(_owner != null)
            transform.position = (_owner as EnemyArcher).arrowSpawnTrm.position;
    }
}
