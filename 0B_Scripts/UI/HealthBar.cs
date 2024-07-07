using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Agent _owner;
    [SerializeField] private Image _fillImage;

    private CanvasGroup _canvasGroup;

    private void Awake() {
        if(_owner is Player) return;
        
        _canvasGroup = GetComponent<CanvasGroup>();
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void Start() {
        _owner.HealthCompo.OnHitEvent.AddListener(HandleHitEvent);

        if(_owner is Enemy)
            _owner.HealthCompo.OnDeadEvent.AddListener(HandleDieEvent);
    }

    private void HandleHitEvent() {
        float fillAmount = _owner.HealthCompo.GetNormalizedHealth();
        _fillImage.fillAmount = fillAmount;
    }

    private void HandleDieEvent() {
        if(_canvasGroup)
            _canvasGroup.DOFade(0, 1f);
    }

    public void ResetItem() {
        if(_canvasGroup)
            _canvasGroup.alpha = 1f;

        _fillImage.fillAmount = 1f;
    }
}
