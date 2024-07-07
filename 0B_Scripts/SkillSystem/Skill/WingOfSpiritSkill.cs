using System.Collections;
using UnityEngine;
using DG.Tweening;

public class WingOfSpiritSkill : Skill
{
    [SerializeField] private float _dashLength = 10f;
    [SerializeField] private float _stiffenDuration = 0.5f;

    [SerializeField] private GameObject _prefab;

    public override bool UseSkill() {
        if(base.UseSkill()) {
            Dash();

            return true;
        }
        return false;
    }

    private void Dash() {
        SoundManager.Instance.PlaySFX("QER");

        float distance = Vector3.Distance(player.transform.position, PlayerManager.Instance.CastingStartPosition);
        bool isFull = distance > _dashLength;

        Vector3 direction = (PlayerManager.Instance.CastingStartPosition - player.transform.position).normalized;

        player.VisibleVisual(false);

        Instantiate(_prefab, player.transform.position + Vector3.up, Quaternion.identity);

        Vector3 endPos = player.transform.position + direction * (isFull ? _dashLength : distance);
        player.transform.DOMove(endPos, 0.3f).SetEase(Ease.InCirc).OnComplete(() => {
            player.MovementCompo.SetDestination(endPos);
            Instantiate(_prefab, endPos + Vector3.up, Quaternion.identity);
            player.VisibleVisual(true);
            StartCoroutine(StiffenCoroutine());
        });
    }

    private IEnumerator StiffenCoroutine() {
        player.moveable = false;
        yield return new WaitForSeconds(_stiffenDuration);
        player.moveable = true;
    }
}
