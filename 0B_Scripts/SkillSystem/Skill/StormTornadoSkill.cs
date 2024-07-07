using UnityEngine;

public class StormTornadoSkill : Skill
{
    public float hitDelay = 1.5f;
    public int maxCount = 15;

    [SerializeField] private float _impulsePower = 0.13f;

    [SerializeField] private StormTornado _prefab;

    public override bool UseSkill() {
        if(base.UseSkill()) {
            CameraManager.Instance.Shake(_impulsePower);

            StormTornado obj = Instantiate(_prefab, PlayerManager.Instance.CastingStartPosition, Quaternion.identity);
            obj.Initialize(this);

            return true;
        }

        return false;
    }
}
