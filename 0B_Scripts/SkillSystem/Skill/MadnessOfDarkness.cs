using System.Collections.Generic;
using UnityEngine;

public class MadnessOfDarkness : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5f;

    private float _timer = 0f;
    private float _damage;
    private float _damageDelay;
    private Dictionary<IDamageable, Transform> _hitDictionary = new Dictionary<IDamageable, Transform>();

    private MadnessOfDarknessSkill _owner;

    public void Init(MadnessOfDarknessSkill owner) {
        _owner = owner;
        _damage = (_owner.player.Stat as PlayerStat).GetQWERDamage(4, 1, 1, 4) * 0.15f;
        _damageDelay = _owner.damageDelay;
        
        SoundManager.Instance.PlaySFX("slash4");

        Destroy(gameObject, _lifeTime);
    }

    private void Update() {
        _timer += Time.deltaTime;

        if(_timer >= _damageDelay) {
            _timer = 0f;

            CameraManager.Instance.Shake(0.25f);
            SoundManager.Instance.PlaySFX($"slash{Random.Range(1, 5)}");

            foreach(var d in _hitDictionary) {
                Vector3 normal = d.Value.position - transform.position;
                normal.y = 0;

                d.Key.ApplyDamage(_damage, d.Value.position, normal, 1f, _owner.player, DamageType.Magic);
            }
            _hitDictionary.Clear();
        }
    }

    private void OnTriggerStay(Collider other) {
        if((1 << other.gameObject.layer) != _owner.whatIsEnemy.value) return;

        if(other.TryGetComponent(out IDamageable health) && !_hitDictionary.TryGetValue(health, out Transform trm)) {
            _hitDictionary.Add(health, other.transform);
        }
    }

    private void OnTriggerExit(Collider other) {
        if((1 << other.gameObject.layer) != _owner.whatIsEnemy.value) return;

        if(other.TryGetComponent(out IDamageable health) && _hitDictionary.TryGetValue(health, out Transform trm)) {
            _hitDictionary.Remove(health);
        }
    }
}
