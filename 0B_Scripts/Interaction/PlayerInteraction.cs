using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _whatIsInteractable;

    private Collider[] _collider;
    private IInteractable _selectObj = null;

    private Casting _casting;

    [Header("Input Setting")]
    [SerializeField] private InputReader _inputReader;

    private bool _isInteracting = false;

    private void Awake() {
        _casting = transform.parent.GetComponent<Casting>();
    }

    private void Start() {
        _collider = new Collider[1];
    }

    private void OnEnable() {
        _inputReader.InteractAction += Interact;
    }

    private void OnDestroy() {
        _inputReader.InteractAction -= Interact;
    }

    private void Update() {
        if(_isInteracting) return;

        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _collider, _whatIsInteractable);

        if(count == 1) {
            if(_selectObj == null)
                UIManager.Instance.ShowChatBox(true);
                
            if(_collider[0].TryGetComponent(out IInteractable interact)) {
                _selectObj = interact;
                _selectObj.UpdateInteract();
                return;
            }
        }

        if(_selectObj != null)
            UIManager.Instance.ShowChatBox(false);

        _selectObj = null;
    }

    private void Interact() {
        if(_selectObj == null || _isInteracting) return;

        _isInteracting = true;
        _casting.castable = false;
        _selectObj.Interact(this);
    }

    public void EndInteract() {
        _casting.castable = true;
        _isInteracting = false;
    }
}
