using UnityEngine;
using TMPro;

public class StatWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthStat;
    [SerializeField] private TextMeshProUGUI _damageStat;

    private Player _player;

    private void Start() {
        _player = PlayerManager.Instance.Player;

        _healthStat.text = _player.Stat.maxHealth.GetValue().ToString();
        _damageStat.text = _player.Stat.damage.GetValue().ToString();
    }

    public void HealthIncrease() {
        _player.Stat.maxHealth.AddModifier(1);
        _healthStat.text = _player.Stat.maxHealth.GetValue().ToString();
    }

    public void DamageIncrease() {
        _player.Stat.damage.AddModifier(1);
        _damageStat.text = _player.Stat.damage.GetValue().ToString();
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}
