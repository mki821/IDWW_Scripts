using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public void AnimationEnd()
    {
        _enemy.AnimationEndTrigger();
    }

    public void AttackTrigger()
    {
        _enemy.Attack();
    }

    public void MoveTrigger()
    {
        _enemy.AnimationMoveTrigger();
    }

    public void StopTrigger()
    {
        _enemy.AnimationStopTrigger();
    }

    public void ShakeCamera(string param)
    {
        var paramArray = param.Split(',');
        float power = float.Parse(paramArray[0]);
        float shakeTime = paramArray.Length > 1 && !string.IsNullOrEmpty(paramArray[1]) ? float.Parse(paramArray[1]):0.2f;
        CameraManager.Instance.Shake(power, shakeTime);
    }

    private Transform warrokAvatar;
    private Transform warrokModel;

    public void EnableShowin()
    {
        if (warrokAvatar == null && warrokModel == null)
        {
            warrokAvatar = _enemy.AnimatorCompo.transform.GetChild(0);
            warrokModel = _enemy.AnimatorCompo.transform.GetChild(1);
        }

        warrokAvatar.gameObject.SetActive(true);
        warrokModel.gameObject.SetActive(true);
    }

    public void DisableShowin()
    {
        if (warrokAvatar == null && warrokModel == null)
        {
            warrokAvatar = _enemy.AnimatorCompo.transform.GetChild(0);
            warrokModel = _enemy.AnimatorCompo.transform.GetChild(1);
        }

        warrokAvatar.gameObject.SetActive(false);
        warrokModel.gameObject.SetActive(false);
    }

    public void SendPosition()
    {
        _enemy.transform.position = warrokAvatar.position;
    }

    public void EnableTeleportCollider()
    {
        (_enemy as EnemyWarrok).baseCollider.enabled = false;
        (_enemy as EnemyWarrok).teleportCollider.enabled = true;
    }

    public void DisableTeleportCollider()
    {
        (_enemy as EnemyWarrok).baseCollider.enabled = true;
        (_enemy as EnemyWarrok).teleportCollider.enabled = false;
    }

    public void DisappearEffect()
    {
        GameObject go = Instantiate((_enemy as EnemyWarrok).disappearEffectPrefab, transform.position, Quaternion.identity);
        go.transform.position += Vector3.down * 0.5f;
    }

    public void ShowinEffect()
    {
        GameObject go = Instantiate((_enemy as EnemyWarrok).showinEffectPrefab, transform.position, Quaternion.identity);
        go.transform.position += Vector3.down * 0.5f;
    }
    public void EquipAxe()
    {
        (_enemy as EnemyWarrok).StateMachine.CurrentState.WeaponEquipTrigger();
    }
}