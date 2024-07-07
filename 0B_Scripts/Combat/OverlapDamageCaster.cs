using System;
using UnityEngine;

public class OverlapDamageCaster : DamageCaster
{
    [SerializeField] protected float _castRadius;
    [SerializeField] protected int _maxColliderCount = 1;
    private Collider[] _collider;

    private void Awake() {
        _collider = new Collider[_maxColliderCount];
    }

    public override bool CastDamage() {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _castRadius, _collider, targetLayer);

        for(int i = 0; i < count; ++i) {
            Transform target = _collider[i].transform;
            if(target.TryGetComponent(out IDamageable health)) {
                float damage = _onwer.Stat.GetDamage();
                float knockBackPower = 3f;

                Vector3 direction = target.position - transform.position;

                bool rayHit = Physics.Raycast(transform.position, direction, out RaycastHit hit, direction.magnitude, targetLayer);
                if(rayHit) {
                    health.ApplyDamage(damage, hit.point, hit.normal, knockBackPower, _onwer, type);
                }
                else {
                    health.ApplyDamage(damage, target.position, -direction.normalized, knockBackPower, _onwer, type);
                }
            }
        }
        OnCastDamageEvent?.Invoke();
        return count > 0;
    }

    public bool CastDamageByValue(Vector3 position, float castRadius, bool useRay = true)
    {
        int count = Physics.OverlapSphereNonAlloc(position, castRadius, _collider, targetLayer);

        for (int i = 0; i < count; ++i)
        {
            Transform target = _collider[i].transform;
            if (target.TryGetComponent(out IDamageable health))
            {
                float damage = _onwer.Stat.GetDamage();
                float knockBackPower = 3f;

                Vector3 direction = target.position - transform.position;

                if (useRay)
                {
                    bool rayHit = Physics.Raycast(transform.position, direction, out RaycastHit hit, direction.magnitude, targetLayer);
                    if (rayHit)
                    {
                        health.ApplyDamage(damage, hit.point, hit.normal, knockBackPower, _onwer, type);
                    }
                    else
                    {
                        health.ApplyDamage(damage, target.position, -direction.normalized, knockBackPower, _onwer, type);
                    }
                }
                else
                    health.ApplyDamage(damage, target.position, -direction.normalized, knockBackPower, _onwer, type);
            }
        }
        OnCastDamageEvent?.Invoke();
        return count > 0;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _castRadius);
        Gizmos.color = Color.white;
    }
#endif

    public float GetCastRadius() => _castRadius;
}
