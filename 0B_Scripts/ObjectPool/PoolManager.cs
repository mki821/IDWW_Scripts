using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<PoolingType, Pool<IPoolable>> _poolDictionary
        = new();

    public PoolList poolListSO;

    protected override void Awake()
    {
        base.Awake();

        foreach (PoolingItemSO item in poolListSO.GetList())
        {
            CreatePool(item);
        }
    }

    private void CreatePool(PoolingItemSO item)
    {
        if (!item._prefab.TryGetComponent(out IPoolable poolingItem))
        {
            Debug.LogError("Poolable�̾����ϴ�");
            return;
        }
        try
        {
            Pool<IPoolable> pool = new Pool<IPoolable>(
            item._prefab as IPoolable, poolingItem.PoolingType, transform, item.poolCount);
            _poolDictionary.Add(poolingItem.PoolingType, pool);
            Debug.Log($"success pooling {item.name}");
        }
        catch
        {
            print(item);
            print(item._prefab);
            print(item._prefab as IPoolable);
            print(poolingItem);
            print(poolingItem.PoolingType);
            print(item.poolCount);
        }
        
    }

    public IPoolable Pop(PoolingType type)
    {
        if (_poolDictionary.ContainsKey(type) == false)
        {
            Debug.LogError($"Prefab does not exist on pool : {type.ToString()}");
            return null;
        }

        IPoolable item = _poolDictionary[type].Pop();
        item.ResetItem();
        return item;
    }

    public void Push(IPoolable obj, bool resetParent = false)
    {
        if (resetParent)
            obj.GameObject.transform.parent = transform;
        _poolDictionary[obj.PoolingType].Push(obj);
    }

}
