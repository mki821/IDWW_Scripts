using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    private Transform _playerTrm;

    private void Start() {
        _playerTrm = PlayerManager.Instance.Player.transform;
    }

    private void LateUpdate() {
        transform.position = _playerTrm.position + _offset;
    }
}
