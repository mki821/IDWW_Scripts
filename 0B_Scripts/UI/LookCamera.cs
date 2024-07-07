using UnityEngine;

public class LookCamera : MonoBehaviour
{
    private Transform _camTrm;

    private void Start() {
        _camTrm = Camera.main.transform;
    }

    private void LateUpdate() {
        Vector3 lookDirection = (transform.position - _camTrm.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
