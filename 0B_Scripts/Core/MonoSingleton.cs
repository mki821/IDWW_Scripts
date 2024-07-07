using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static bool _isDestroyed = false;

    public bool dontDestroyOnLoad = true;
    
    public static T Instance {
        get {
            if(_isDestroyed) _instance = null;

            if(_instance == null) {
                _instance = GameObject.FindObjectOfType<T>();

                if(_instance == null)
                    Debug.LogError($"{typeof(T).Name} singleton is not exist!");
            }

            return _instance;
        }
    }

    protected virtual void Awake() {
        if(Instance == this) {
            if(dontDestroyOnLoad) DontDestroyOnLoad(transform.root.gameObject);
        }
        else Destroy(transform.root.gameObject);
    }

    private void OnDestroy() {
        _isDestroyed = true;
    }
}
