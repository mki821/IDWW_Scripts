using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private void Start() {
        Destroy(gameObject, _lifeTime);
    }
}
