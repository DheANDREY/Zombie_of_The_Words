using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerZombie : MonoBehaviour
{
    public EnemyController zombie;
    private Vector3 posisi;
    public GameObject posisiSpawn;

    public static SpawnerZombie instance;
    private void Awake()
    {
        instance = this;
    }

    private GameObject instantiatedObj;
    private EnemyController enemyChache;
    public void SpawnZombie(Vector3 spawnPosition)
    {
        enemyChache =  Instantiate(zombie, spawnPosition, Quaternion.identity);
        enemyChache.Initialize(transform);
        //instantiatedObj.transform.SetParent(transform);
        //Invoke("KilledZombie", 25);
    }

    public void Death()
    {
        KilledZombie();
        GameFlow.instance.isZombieKilled = true;
    }
    private void KilledZombie()
    {
        Destroy(instantiatedObj);
    }
}
