using System.Collections.Generic;
using UnityEngine;

public class OneHandSmash : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 2f;

    private float _damage;
    private OneHandSmashSkill _owner;
    private List<IDamageable> _hitList = new List<IDamageable>();

    public void Initialize(OneHandSmashSkill owner) {
        _owner = owner;
        _damage = (PlayerManager.Instance.Player.Stat as PlayerStat).GetQWERDamage(1, 1, 1, 2);
    }

    private void Start() {
        SoundManager.Instance.PlaySFX("QRWER");

        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other) {
        if((1 << other.gameObject.layer) != _owner.whatIsEnemy) return;

        if(other.TryGetComponent(out IDamageable health) && !_hitList.Contains(health)) {
            _hitList.Add(health);
            Vector3 normal = other.transform.position - transform.position;
            normal.y = 0;

            health.ApplyDamage(_damage, other.transform.position, normal, 5f, _owner.player, DamageType.Magic);
        }
    }
}
