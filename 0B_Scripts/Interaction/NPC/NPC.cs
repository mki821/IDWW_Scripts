using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] protected string _name;
    [SerializeField] private float _offset;

    [SerializeField] private string[] _scripts;

    private int _index = 0;

    private Player _player;
    private PlayerInteraction _playerInteract;

    private void Start() {
        _player = PlayerManager.Instance.Player;
    }

    public void Interact(PlayerInteraction player)  {
        UIManager.Instance.ShowChatBox(false);
        UIManager.Instance.TogglePlayAndTalk();
        _playerInteract = player;
        _player.moveable = false;
        _player.MovementCompo.StopImmediately();

        CameraManager.Instance.NpcCam(true);
        _index = 0;
        HandleConversation();
        UIManager.Instance.SetConversationFunc(HandleConversation);
    }

    public void HandleConversation() {
        if(_index < _scripts.Length) {
            UIManager.Instance.ChangeConversationContent(_name, _scripts[_index++]);
        }
        else {
            EndInteract();
        }
    }

    private void EndInteract() {
        UIManager.Instance.RemoveConversationFunc(HandleConversation);
        CameraManager.Instance.NpcCam(false);
        _player.moveable = true;
        UIManager.Instance.TogglePlayAndTalk();
        _playerInteract.EndInteract();
    }

    public void UpdateInteract() {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        screenPos.x += _offset;
        UIManager.Instance.MoveChatBox(screenPos);
    }
}
