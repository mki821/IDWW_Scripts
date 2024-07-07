using UnityEngine;

public class DeathRevolutionSkill : Skill
{
    [SerializeField] private DeathRevolution _prefab;

    public override bool UseSkill() {
        if(base.UseSkill()) {
            Instantiate(_prefab, PlayerManager.Instance.CastingStartPosition, Quaternion.identity).Init(this);

            return true;
        }
        return false;
    }
}
