using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public float angularSpeed;
    public bool isRight;

    private float _currentAngle = 0f;
    private Vector3 _startRotation;

    private float _timer = 0f;
    private Player _player;

    private void Start() {
        _startRotation = transform.rotation.eulerAngles;

        _player = PlayerManager.Instance.Player;
    }

    private void Update() {
        _currentAngle += Time.deltaTime * angularSpeed * (isRight ? 1 : -1);
        _timer += Time.deltaTime;

        if(_timer >= 1f) {
            _player.stamina.CurrentStamina -= 3;
            _timer = 0f;
        }

        transform.localRotation = Quaternion.Euler(_startRotation.x, _startRotation.y, _currentAngle);
    }

    public void Break() {

    }
}
