using UnityEngine;

public class OneHandSmashSkill : Skill
{
    public int damage = 5;
    public int maxCount = 10;

    [SerializeField] private float _impulsePower = 0.13f;

    [SerializeField] private OneHandSmash _prefab;
    [SerializeField] private float _offset = 5f;

    public override bool UseSkill() {
        if(base.UseSkill()) {
            CameraManager.Instance.Shake(_impulsePower);

            Vector3 direction = PlayerManager.Instance.CastingStartPosition - player.transform.position;
            direction.y = 0;
            direction.Normalize();

            Vector3 position = player.transform.position + direction * _offset;

            Quaternion rotation = Quaternion.LookRotation(direction);

            OneHandSmash obj = Instantiate(_prefab, position, rotation);
            obj.Initialize(this);

            return true;
        }

        return false;
    }
}
