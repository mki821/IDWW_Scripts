using UnityEngine;

public class TitleMagicCircle : MonoBehaviour
{
    public float angularSpeed;
    public bool isRight;

    private float _currentAngle = 0f;
    private Vector3 _startRotation;

    private void Start() {
        _startRotation = transform.rotation.eulerAngles;
    }

    private void Update() {
        _currentAngle += Time.deltaTime * angularSpeed * (isRight ? 1 : -1);

        transform.localRotation = Quaternion.Euler(_startRotation.x, _startRotation.y, _currentAngle);
    }
}
