using UnityEngine;
using DG.Tweening;

public class WarningCircle : MonoBehaviour
{
    public LayerMask targetLayer;

    private Enemy _owner;
    private float _radius;
    private float _attackTime;
    private Collider[] _collider;
    private DamageType _type;
    private SpriteRenderer _spriteRenderer;
    private GameObject _effectPrefab;

    public void SetValueAndStart(Enemy _enemy, float radius, float attackTime, DamageType type, bool isTeleport)
    {
        _owner = _enemy;
        _radius = radius;
        _attackTime = attackTime;
        _type = type;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = new Collider[1];
        gameObject.transform.DOScale(_radius * 2, _attackTime).SetEase(Ease.Linear).OnComplete(Cast);
        _effectPrefab = isTeleport ? (_owner as EnemyWarrok).teleportSmashEffectPrefab : (_owner as EnemyWarrok).runJumpSmashEffectPrefab;
    }

    public void Cast()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _collider, targetLayer);

        print(count);

        for (int i = 0; i < count; ++i)
        {
            Transform target = _collider[i].transform;
            if (target.TryGetComponent(out IDamageable health))
            {
                float damage = _owner.Stat.GetDamage();
                float knockBackPower = 3f;

                Vector3 direction = target.position - transform.position;

                bool rayHit = Physics.Raycast(transform.position, direction, out RaycastHit hit, direction.magnitude, targetLayer);
                if (rayHit)
                {
                    health.ApplyDamage(damage, hit.point, hit.normal, knockBackPower, _owner, _type);
                }
                else
                {
                    health.ApplyDamage(damage, target.position, -direction.normalized, knockBackPower, _owner, _type);
                }
            }
        }

        GameObject go = Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, .1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
