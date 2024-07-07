using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FeedbackPain : MonoBehaviour
{
    private Health _playerHealth;
    private Volume _volume;
    private Vignette _vig;

    private void Awake() {
        _playerHealth = PlayerManager.Instance.Player.HealthCompo;

        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _vig);
    }

    public void Pain() {
        if(_playerHealth.currentHealth < 50)
            _vig.intensity.value = 0.5f - (_playerHealth.currentHealth / 100f);
        else _vig.intensity.value = 0f;
    }
}
