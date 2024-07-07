using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private Player _player;

    public Player Player => _player;
    [HideInInspector] public Vector3 CastingStartPosition = Vector3.zero;

    public void SetPlayer(Player player) {
        _player = player;
    }
}
