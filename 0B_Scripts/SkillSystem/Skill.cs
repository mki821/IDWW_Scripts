using UnityEngine;

public delegate void CooldownInfoEvent(float current, float total);

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected float _cooldown;
    [SerializeField] protected PlayerSkill _skillType;

    [HideInInspector] public Player player;
    protected float _cooldownTimer;
    public event CooldownInfoEvent OnCooldownEvent;

    public LayerMask whatIsEnemy;

    protected virtual void Start() {
        player = PlayerManager.Instance.Player;
    }

    protected virtual void Update() {
        if(_cooldownTimer > 0) {
            _cooldownTimer -= Time.deltaTime;

            if(_cooldownTimer < 0) {
                _cooldownTimer = 0;
            }
            OnCooldownEvent?.Invoke(_cooldownTimer, _cooldown);
        }
    }

    public virtual bool UseSkill() {
        if(_cooldownTimer > 0) return false;

        _cooldownTimer = _cooldown;
        SkillManager.Instance.StartSkillCooltime(SkillManager.Instance.GetSkillSO(_skillType).icon, _cooldown);
        
        return true;
    }
}
