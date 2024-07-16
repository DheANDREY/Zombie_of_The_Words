using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using TMPro;

public class SpawnerZombie : MonoBehaviour
{
    public EnemyStats[] zombie;
    public EnemyController[] enemyPrefab;
    private Dictionary<EnemyStats, float> initialZombieSpeeds = new Dictionary<EnemyStats, float>();
    private Dictionary<EnemyStats, float> initialZombieDamages = new Dictionary<EnemyStats, float>();
    private Vector3 posisi;
    public GameObject posisiSpawn, warningZombieW;
    public int lvlDisplay; private bool isLvlUP;
    public TextMeshProUGUI lvlText1, lvlText2;
    private const int maxFase = 15; // Jumlah fase maksimum
    private const float maxMultiplier = 4.0f; // Multiplier maksimum
    public float numIntoMaxLvl=15; public float[] phaseMultiplierSpd; public float[] initialSpeed; public float[] initialDamage;
    public float[] phaseMultiplierDmg;
    public static int correctWordCounter;
    public int initialPoolSize;
    private IObjectPool<EnemyController>[] enemyPool;
    private List<EnemyController> spawnedEnemies = new List<EnemyController>();

    private CharMoveController CMC;
    public static SpawnerZombie instance;
    private void Awake()
    {
        instance = this;
        //enemyPool = new ObjectPool<EnemyController>(SpawnEnemyPool, OnGet, OnRelease);
        enemyPool = new IObjectPool<EnemyController>[enemyPrefab.Length];
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            int index = i; // Untuk menghindari masalah dengan lambda closure
            enemyPool[i] = new ObjectPool<EnemyController>(
                () => SpawnEnemyPool(index), OnGet, OnRelease);
        }
    }
    private void OnGet(EnemyController enemy)
    {
        int randomAnchorIndex = Random.Range(0, anchorEnemySpawner.Length);
        enemy.transform.position =  anchorEnemySpawner[randomAnchorIndex].position;
        enemy.isInPool = false;
        enemy.gameObject.transform.SetParent(posisiSpawn.transform);
        enemy.gameObject.SetActive(true);
        enemy.Initialize2(player);
        enemy.ResetStatFromPool();
    }
    private void OnRelease(EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
    }
    private EnemyController SpawnEnemyPool(int index)
    {
        //int randomEnemyIndex = Random.Range(0, enemyPrefab.Length);
        
        EnemyController enemy = Instantiate(enemyPrefab[index]);
        enemy.SetPool(enemyPool[index]);
        return enemy;
    }

    private void OnEnable()
    {
        lvlDisplay = 1;
        zombieCounter = 0;
        zombiesPerWave = Random.Range(1, 3);
        zombieCounter = zombiesPerWave;
        //lvlDisplay = 1;
        CharMoveController.instance.isDamaged = false;
        isSpawnZombie = true;    isWaveStop = false;
        //isWaveActive = true;                
    }
    private void OnDisable()
    {
        isSpawnZombie = false;
        zombieCounter = 0;
        ResetZombieStats();
    }
    private void Start()
    {
        CMC = GameObject.FindObjectOfType<CharMoveController>();        
        lvlDisplay = 1;
        initialSpeed[0] = zombie[0].speed; initialSpeed[1] = zombie[1].speed; initialSpeed[2] = zombie[2].speed;
        initialDamage[0] = zombie[0].damage; initialDamage[1] = zombie[1].damage; initialDamage[2] = zombie[2].damage;
    }

    private GameObject instantiatedObj;
    private EnemyController enemyChache;
    public bool isSpawnZombie;
    public void SpawnZombie()
    {
        if (isSpawnZombie)
        {
            // Mendapatkan pool secara acak
            int randomEnemyIndex = Random.Range(0, zombie.Length);
            //int randomAnchorIndex = Random.Range(0, anchorEnemySpawner.Length);

            //EnemyController enemy = enemyPools[randomEnemyIndex].Get();
            //enemy.transform.position = anchorEnemySpawner[randomAnchorIndex].position;
            //enemy.transform.rotation = Quaternion.identity;
            //enemy.Initialize2(player);
            //enemy.transform.parent = parentEnemy;
            //GameObject enemyObject = ObjectPoolManager.SpawnObject(zombie[randomEnemyIndex].gameObject, anchorEnemySpawner[randomAnchorIndex].position, Quaternion.identity, ObjectPoolManager.PoolType.GameObject);
            EnemyController enemy = enemyPool[randomEnemyIndex].Get();
            spawnedEnemies.Add(enemy);
            //EnemyController enemy = enemy.Initialize2(player);
            //if (enemyObject != null)
            //{
            //    EnemyController enemy = enemyObject.GetComponent<EnemyController>();
            //    if (enemy != null)
            //    {
            //        enemy.Initialize2(player);
            //    }
            //    else
            //    {
            //        Debug.LogWarning("Spawned object does not have an EnemyController component.");
            //    }
            //}

            zombiesRemaining--;
        }
        else
        {
            Debug.Log("Stop Spawn");
        }
    }
    //public void ReturnZombieToPool(EnemyController enemy)
    //{
    //    ObjectPoolManager.ReturnObjectToPool(enemy.gameObject);
    //}
    public void ReturnAllZombiesToPool()
    {
        foreach (var enemy in spawnedEnemies)
        {
            enemy.BackToPool();
        }
        spawnedEnemies.Clear();
    }

    private void Update()
    {
        //Debug.Log(isWaveStop);
        lvlText1.text = lvlDisplay.ToString(); lvlText2.text = lvlDisplay.ToString();
        //Debug.Log("Zombie Wave= " + zombiesPerWave); Debug.Log("Zombie Die Counter= " + zombieCounter);
        if (!isWaveStop)
        {
            //Debug.Log("Run Wave");
            Invoke("SpawnWaveZombie", 2f);

        }
        else
        {            
            //Debug.Log("Kill Last Zombie!");
            if (zombieCounter <= 0 && !isLvlUP)
            {
                lvlDisplay++;   
                spdSpawn+=20f;       //Debug.Log(spdSpawn);

                phaseMultiplierSpd = new float[zombie.Length];
                int a = Random.Range(0, 2); //Debug.Log(a);
                for (int i = 0; i < zombie.Length; i++)
                {
                    // count Multiple
                    phaseMultiplierSpd[i] = (((initialSpeed[i] * 4) - initialSpeed[i]) / numIntoMaxLvl)*2;
                    phaseMultiplierDmg[i] = (((initialDamage[i] * 4) - initialDamage[i]) / numIntoMaxLvl)*2;

                    if (!initialZombieSpeeds.ContainsKey(zombie[i]) && !initialZombieDamages.ContainsKey(zombie[i]))
                    {
                        initialZombieSpeeds.Add(zombie[i], zombie[i].speed);
                        initialZombieDamages.Add(zombie[i], zombie[i].damage);
                    }                    
                    if(a == 0)
                    {
                        if (zombie[i].speed > initialSpeed[i] * 4)
                        {
                            zombie[i].speed = initialSpeed[i] * 4;
                        }
                        else
                        {
                            zombie[i].speed += phaseMultiplierSpd[i];
                        }
                    }
                    else
                    {
                        if (zombie[i].damage > initialDamage[i] * 4)
                        {
                            zombie[i].damage = initialDamage[i] * 4;
                        }
                        else
                        {
                            zombie[i].damage += phaseMultiplierDmg[i];
                        }
                    }
                }
                isLvlUP = true;                       
                warningZombieW.SetActive(true);
                isRandomizeNum = true;
                Invoke("DisableWarning", 8);                                                
            }            
            isWaveStop = false;         
        }
    }

    private bool isRandomizeNum;
    public void DisableWarning()
    {
        warningZombieW.SetActive(false);
        if (isRandomizeNum)
        {
            if (lvlDisplay < 4)
            {
                zombiesPerWave = Random.Range(5, 8);
            }
            else if (lvlDisplay > 3 && lvlDisplay < 9 )
            {
                zombiesPerWave = Random.Range(9, 12);
            }
            else
            {
                int maxZom = Random.Range(15, 20);
                zombiesPerWave = Random.Range(13, maxZom);
            }   //Debug.Log("Zpmbie Wave= " + zombiesPerWave);
            zombieCounter = zombiesPerWave;
            isLvlUP = false;
            isRandomizeNum = false;
        }        
    }

    
    public Transform[] anchorEnemySpawner; public Transform player;
    private int zombiesRemaining,zombiesPerWave; float delaySpawns ,spdDelay = 400, spdSpawn=100;
    private bool isWaveStop;
    public int zombieCounter;
    private void SpawnWaveZombie()
    {
        if (delaySpawns <= 0 && zombiesPerWave>0)
        {
            SpawnZombie(); 
            zombiesPerWave--;           
            delaySpawns = spdDelay/spdSpawn; //Debug.Log("Delay Spawn: "+delaySpawns);
            //delaySpawns = 0;
        }
        else
        {
            delaySpawns -= Time.deltaTime;
            if (zombiesPerWave <= 0)
            {
                zombiesPerWave = 0;                      
                isWaveStop = true;
               // Debug.Log("Wave Finished:" + zombiesPerWave);
            }
        }
    }

    private void ResetZombieStats()
    {
        foreach (var spd in initialZombieSpeeds)
        {
            spd.Key.speed = spd.Value;
        }
        foreach (var dmg in initialZombieDamages)
        {
            dmg.Key.damage = dmg.Value;
        }

        // Reset dictionary
        initialZombieSpeeds.Clear();    initialZombieDamages.Clear();
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