using System.Collections.Generic;
using UnityEngine;

public class DeathRevolution : MonoBehaviour
{
    [SerializeField] private float _damageCooltime = 0.2f;
    [SerializeField] private float _lifeTime = 3f;

    private float _damage;
    private DeathRevolutionSkill _skill;

    private List<IDamageable> _deletableList = new List<IDamageable>();
    private Dictionary<IDamageable, float> _enemyTimeDictionary = new Dictionary<IDamageable, float>();

    public void Init(DeathRevolutionSkill skill) {
        _skill = skill;
        _damage = (_skill.player.Stat as PlayerStat).GetQWERDamage(0, 1, 2, 2);

        SoundManager.Instance.PlaySFX("ERWER");

        Destroy(gameObject, _lifeTime);
    }

    private void Update() {
        foreach(var e in _enemyTimeDictionary) {
            if(Time.time - e.Value >= _damageCooltime) {
                _deletableList.Add(e.Key);
            }
        }

        foreach(IDamageable h in _deletableList) {
            _enemyTimeDictionary.Remove(h);
        }
        _deletableList.Clear();
    }

    private void OnTriggerStay(Collider other) {
        if((1 << other.gameObject.layer) != _skill.whatIsEnemy.value) return;

        if(other.TryGetComponent(out IDamageable health)) {
            if(!_enemyTimeDictionary.ContainsKey(health)) {
                CameraManager.Instance.Shake(0.05f);

                _enemyTimeDictionary.Add(health, Time.time);

                Vector3 normal = other.transform.position - transform.position;
                normal.y = 0;
                health.ApplyDamage(_damage, other.transform.position, normal, 3f, PlayerManager.Instance.Player, DamageType.Magic);
            }
        }
    }
}
