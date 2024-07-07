using UnityEngine;

public enum DamageType
{
    Magic, Melee, Range
}

public interface IDamageable
{
    public void ApplyDamage(float damage, Vector3 hitPoint, Vector3 normal,
                            float knockbackPower, Agent dealer, DamageType type);
}
