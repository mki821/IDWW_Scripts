using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifeTime = 2f;

    private float _damage;

    private FireShotSkill _owner;
    private List<IDamageable> _hitList = new List<IDamageable>();

    public void Initialize(FireShotSkill owner) {
        _owner = owner;
        _damage = (_owner.player.Stat as PlayerStat).GetQWERDamage(2, 0, 1, 0) * 1.8f;

        SoundManager.Instance.PlaySFX("RER");
    }

    private void Start() {
        Destroy(gameObject, _lifeTime);
    }

    private void Update() {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if(_hitList.Count > _owner.maxCount) return;
        
        if((1 << other.gameObject.layer) == _owner.whatIsEnemy.value) {
            if(other.TryGetComponent(out IDamageable health) && !_hitList.Contains(health)) {
                _hitList.Add(health);
                Vector3 normal = (other.transform.position - transform.position).normalized;
                normal.y = 0;
                health.ApplyDamage(_damage, other.transform.position, normal, 5f, _owner.player, DamageType.Magic);
        
                CameraManager.Instance.Shake(0.11f, 0.1f);
                GameManager.Instance.StopTime(0.005f);
            }
        }
    }
}
