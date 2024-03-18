using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnerZombie : MonoBehaviour
{
    public EnemyController[] zombie;
    private Dictionary<EnemyController, float> initialZombieSpeeds = new Dictionary<EnemyController, float>();
    private Dictionary<EnemyController, float> initialZombieDamages = new Dictionary<EnemyController, float>();
    private Vector3 posisi;
    public GameObject posisiSpawn, warningZombieW;
    public int lvlDisplay; private bool isLvlUP;
    public TextMeshProUGUI lvlText1, lvlText2;
    private const int maxFase = 15; // Jumlah fase maksimum
    private const float maxMultiplier = 4.0f; // Multiplier maksimum
    public float numIntoMaxLvl=15; public float[] phaseMultiplierSpd; public float[] initialSpeed; public float[] initialDamage;
    public float[] phaseMultiplierDmg;
    public static int correctWordCounter;

    private CharMoveController CMC;
    public static SpawnerZombie instance;
    private void Awake()
    {
        instance = this;        
    }

    private void OnEnable()
    {
        lvlDisplay = 1;
        zombieCounter = 0;
        zombiesPerWave = Random.Range(1, 3);
        zombieCounter = zombiesPerWave;
        //lvlDisplay = 1;
        CharMoveController.instance.isDamaged = false;
        isWaveStop = false;
        //isWaveActive = true;                
    }
    private void OnDisable()
    {
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
    public void SpawnZombie()
    {        
        enemyChache = Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Length)], anchorEnemySpawner[Random.Range(0, anchorEnemySpawner.Length - 1)].position, Quaternion.identity);
        enemyChache.Initialize(player);
        enemyChache.transform.parent = parentEnemy;
        zombiesRemaining--;
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

    public EnemyController[] enemyPrefab;
    public Transform[] anchorEnemySpawner; public Transform player, parentEnemy;
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
