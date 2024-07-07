using UnityEngine;
using UnityEngine.Events;

public abstract class DamageCaster : MonoBehaviour
{
    public LayerMask targetLayer;
    public DamageType type;
    public UnityEvent OnCastDamageEvent;

    protected Agent _onwer;

    public void InitCaster(Agent agent) {
        _onwer = agent;
    }

    public abstract bool CastDamage();
}
