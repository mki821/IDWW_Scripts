using UnityEngine;

public struct ActionData
{
    public Vector3 hitPoint;
    public Vector3 hitNormal;
    public bool isLastHitCritical;
    public DamageType type;

    public float knockBackPower;
}
