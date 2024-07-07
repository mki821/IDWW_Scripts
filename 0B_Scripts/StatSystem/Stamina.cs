using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField] private int _maxStamina = 100;

    [SerializeField] private int _currentStamina;

    [SerializeField] private Image _staminaBar;

    private float _lastTime;
    private Casting _casting;

    public int CurrentStamina {
        get => _currentStamina;
        set {
            if(value < 0)
                _casting.Completion();
                
            _currentStamina = Mathf.Clamp(value, 0, _maxStamina);
            _staminaBar.fillAmount = (float)_currentStamina / _maxStamina;
        }
    }

    private void Awake() {
        _casting = GetComponent<Casting>();
    }

    private void Start() {
        _currentStamina = _maxStamina;
        _lastTime = Time.time;
    }

    private void Update() {
        if(_lastTime + 1 <= Time.time) {
            CurrentStamina += 5;
            _lastTime = Time.time;
        }
    }
}
