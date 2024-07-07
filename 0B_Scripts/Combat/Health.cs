using UnityEngine;
using UnityEngine.Events;
using ObjectPooling;

public class Health : MonoBehaviour, IDamageable
{
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeadEvent;

    public ActionData actionData;

    private Agent _owner;

    public int currentHealth;
    private bool _isInvincible = false;

    public void SetInvincible(bool value)
    {
        _isInvincible = value;
    }

    public void Initialize(Agent agent)
    {
        _owner = agent;
        actionData = new ActionData();
        currentHealth = Mathf.CeilToInt(_owner.Stat.maxHealth.GetValue());
    }

    public void ApplyDamage(float damage, Vector3 hitPoint, Vector3 normal, float knockbackPower, Agent dealer, DamageType type)
    {
        if (_owner.isDead) return;

        Vector3 textPosition = hitPoint + new Vector3(0, 1f, 0);
        PopUpText text = PoolManager.Instance.Pop(PoolingType.PopUpText)
            as PopUpText;

        if (_isInvincible)
        {
            text.StartPopUp("����!", textPosition, 7, Color.white);
            return;
        }

        if (_owner.Stat.CanEvasion())
        {
            text.StartPopUp("ȸ����!", textPosition, 7, Color.white);
            return;
        }


        actionData.hitPoint = hitPoint;
        actionData.hitNormal = normal;
        actionData.type = type;

        if (knockbackPower > Mathf.Epsilon)
        {
            ApplyKnockBack(normal * -knockbackPower);
        }

        actionData.isLastHitCritical = dealer.Stat.IsCritical(ref damage);
        //������ ���� ������ �� ����� ���ؼ� �ʿ��ϴ�.
        damage = _owner.Stat.ArmoredDamage(damage);


        if (actionData.isLastHitCritical)
        {
            text.StartPopUp(((int)damage).ToString(), textPosition, Mathf.Clamp(Mathf.CeilToInt(0.8f * damage), 7, 10), Color.yellow);
        }
        else
        {
            text.StartPopUp(((int)damage).ToString(), textPosition, Mathf.Clamp(Mathf.CeilToInt(0.8f * damage), 7, 10), Color.white);
        }

        currentHealth = Mathf.CeilToInt(Mathf.Clamp(currentHealth - damage, 0, _owner.Stat.maxHealth.GetValue()));
        OnHitEvent?.Invoke();
        if (currentHealth <= 0)
        {
            OnDeadEvent?.Invoke();
        }
    }

    private void ApplyKnockBack(Vector3 force)
    {
        _owner.MovementCompo.GetKnockback(force);
    }

    public float GetNormalizedHealth()
    {
        return currentHealth / _owner.Stat.maxHealth.GetValue();
    }
} 
