using UnityEngine;
using ObjectPooling;

public class Scarecrow : MonoBehaviour, IDamageable
{
    private ActionData _actionData;

    private void Awake() {
        _actionData = new ActionData();
    }

    public void ApplyDamage(float damage, Vector3 hitPoint, Vector3 normal, float knockbackPower, Agent dealer, DamageType type) {
        Vector3 textPosition = hitPoint + new Vector3(0, 1f, 0);
        PopUpText text = PoolManager.Instance.Pop(PoolingType.PopUpText)
            as PopUpText;

        if (_actionData.isLastHitCritical)
        {
            text.StartPopUp(((int)damage).ToString(), textPosition, Mathf.Clamp(Mathf.CeilToInt(0.8f * damage), 7, 10), Color.yellow);
        }
        else
        {
            text.StartPopUp(((int)damage).ToString(), textPosition, Mathf.Clamp(Mathf.CeilToInt(0.8f * damage), 7, 10), Color.white);
        }
    }
}
