using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    private GameObject _poolHolder;
    private static GameObject _particleSystemsEmpty;
    private static GameObject _gameObjectsEmpty;
    public enum PoolType
    {
        ParticleSystem,
        GameObject,
        None
    }
    private void Awake()
    {
        SetupEmpties();
    }
    public static PoolType PoolingType;
    private void SetupEmpties()
    {
        _poolHolder = new GameObject("Pooled Objects");
        _particleSystemsEmpty = new GameObject("Particle Effects");
        _particleSystemsEmpty.transform.SetParent(_poolHolder.transform);

        _gameObjectsEmpty = new GameObject("GameObjects");
        _gameObjectsEmpty.transform.SetParent(_poolHolder.transform);
    }

    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();
    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotatio, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);
        //PooledObjectInfo pool = null;
        //foreach(PooledObjectInfo p in ObjectPools)
        //{
        //    if(p.LookupString == objectToSpawn.name)
        //    {
        //        pool = p;
        //        break;
        //    }
        //}

        //If the pool doesn't exist, create it
        if(pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        //Check if there any inactive objects in the pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();
        //GameObject spawnableObj = null;
        //foreach(GameObject obj in pool.InactiveObjects)
        //{
        //    if (obj != null)
        //    {
        //        spawnableObj = obj;
        //        break;
        //    }
        //}

        if(spawnableObj == null)
        {
            //Find the parent of the empty object
            GameObject parentObject = SetParentObject(poolType);

            // If there are no inactive objects, create a new one
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotatio);

            if(parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            spawnableObj.transform.rotation = spawnRotatio;
            spawnableObj.transform.position = spawnPosition;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7);
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if(pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _particleSystemsEmpty;
            case PoolType.GameObject:
                return _gameObjectsEmpty;
            case PoolType.None:
                return null;
            default:
                return null;
        }
    }
}
public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
