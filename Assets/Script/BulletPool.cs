using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 8;
    [SerializeField] private GameObject bulletPrefab;

    public static BulletPool instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
     for(int i=0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }   
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
