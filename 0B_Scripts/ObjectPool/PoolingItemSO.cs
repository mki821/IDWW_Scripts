using UnityEngine;

namespace ObjectPooling
{
    [CreateAssetMenu(menuName = "SO/Pool/PoolItem")]
    public class PoolingItemSO : ScriptableObject
    {
        public string enumName;
        public string poolingName;
        public string description;
        public int poolCount;
        public MonoBehaviour _prefab;

    #if UNITY_EDITOR
        private void OnValidate()
        {
            if (_prefab.TryGetComponent(out IPoolable prefab))
            {

                if (prefab.PoolingType.ToString() != enumName)
                {
                    Debug.LogWarning("Check Prefab type");
                    _prefab = null;
                }
            }
        }
    #endif
    }
}
