using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/PlayerStat")]
public class PlayerStat : AgentStat
{
    public Stat qDamage;
    public Stat wDamage;
    public Stat eDamage;
    public Stat rDamage;

    public float GetQWERDamage(int q, int w, int e, int r) {
        return qDamage.GetValue() * q + wDamage.GetValue() * w + eDamage.GetValue() * e + rDamage.GetValue() * r;
    }
}
