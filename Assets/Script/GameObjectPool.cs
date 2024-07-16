using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    private readonly GameObject prefab;
    private readonly Queue<GameObject> pool = new Queue<GameObject>();
    private readonly Transform parentTransform;

    public GameObjectPool(GameObject prefab, int initialSize, GameObject poolParent = null)
    {
        this.prefab = prefab;
        this.parentTransform = poolParent != null ? poolParent.transform : null;

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Object.Instantiate(prefab);
            if (parentTransform != null)
            {
                obj.transform.SetParent(parentTransform);
            }
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Object.Instantiate(prefab);
            if (parentTransform != null)
            {
                obj.transform.SetParent(parentTransform);
            }
            return obj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    public int PoolCount
    {
        get { return pool.Count; }
    }
}
