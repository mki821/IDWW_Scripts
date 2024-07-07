using System.Collections.Generic;
using UnityEngine;

public class StormTornado : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private LayerMask _whatIsEnemy;

    [SerializeField] private GameObject _prefab;

    private float _timer = 0f;
    private float _damage;

    private StormTornadoSkill _owner;
    private Dictionary<IDamageable, Transform> _hitDictionary = new Dictionary<IDamageable, Transform>();

    public void Initialize(StormTornadoSkill owner) {
        _owner = owner;
        _damage = (_owner.player.Stat as PlayerStat).GetQWERDamage(2, 1 ,1, 1);

        SoundManager.Instance.PlaySFX("QQEWR_Storm", _lifeTime);
        
        Destroy(gameObject, _lifeTime);
    }

    private void Update() {
        _timer += Time.deltaTime;
        if(_timer >= _owner.hitDelay) {
            _timer = 0f;
            Damage();
        }
    }

    private void OnTriggerStay(Collider other) {
        if(_hitDictionary.Count > _owner.maxCount) return;

        if((1 << other.gameObject.layer) == _whatIsEnemy.value) {
            if(other.TryGetComponent(out IDamageable health) && !_hitDictionary.TryGetValue(health, out Transform trm)) {
                _hitDictionary.Add(health, other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if((1 << other.gameObject.layer) == _whatIsEnemy.value) {
            if(other.TryGetComponent(out IDamageable health) && _hitDictionary.TryGetValue(health, out Transform trm)) {
                _hitDictionary.Remove(health);
            }
        }
    }

    private void Damage() {
        Instantiate(_prefab, transform.position, Quaternion.identity, transform);

        SoundManager.Instance.PlaySFX("QQEWR_Thunder");

        if(_hitDictionary.Count == 0) return;
        
        CameraManager.Instance.Shake(0.2f);
        GameManager.Instance.StopTime(0.007f);

        foreach(var d in _hitDictionary) {
            Vector3 normal = (d.Value.position - transform.position).normalized;
            normal.y = 0f;
            d.Key.ApplyDamage(_damage, d.Value.position, normal, 5f, _owner.player, DamageType.Magic);
        }
        _hitDictionary.Clear();
    }
}
