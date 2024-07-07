using UnityEngine;

public class MadnessOfDarknessSkill : Skill
{
    public int damage;
    public float damageDelay = 0.2f;

    [SerializeField] private float _impulsePower = 0.13f;

    [SerializeField] private MadnessOfDarkness _prefab;

    public override bool UseSkill() {
        if(base.UseSkill()) {
            CameraManager.Instance.Shake(_impulsePower);

            Instantiate(_prefab, PlayerManager.Instance.Player.transform.position, Quaternion.identity).Init(this);

            return true;
        }
        return false;
    }
}
