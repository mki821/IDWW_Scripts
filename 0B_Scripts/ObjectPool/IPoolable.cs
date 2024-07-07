using ObjectPooling;
using UnityEngine;

public interface IPoolable
{
    PoolingType PoolingType { get; set; }
    GameObject GameObject { get; }
    IPoolable Poolable { get; }
    public void ResetItem();
}
