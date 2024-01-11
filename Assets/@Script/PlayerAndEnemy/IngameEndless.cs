using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameEndless : MonoBehaviour
{
    public static IngameEndless Instance;
    public float spawnDelayMin,spawnDelayMax,reloadTime;
    public GameObject[] ammo;
    public Transform player;
    public Transform[] anchorEnemySpawner;
    public EnemyController enemyPrefabs;
    public bool playerDie;
    public GameObject achievement;
    public TextMeshProUGUI t_timer, bestWPM, bestEPM,bestAccuracy;
    private float curStopwatch;
    public Transform parentEnemy;
    public AudioClip reloadClip;

    private EnemyController enemyChache;
    private float currentDelay,curReload;
    private int falseCounter;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject[] posisiSpawner;
    private void Start()
    {
        //for (int i = 0; i < posisiSpawner.Length; i++)
        //{
        //    SpawnerZombie.instance.SpawnZombie(posisiSpawner[i].transform.position);
        //}
    }
    private void OnEnable()
    {
        playerDie = false;
        WordManager.hitCount = 0;
        WordManager.wordTypedCount = 0;
        WordManager.Instance.WordInit();
        EventManager.onFalse += EventManager_onFalse;
         
        GameManager.Instance.envi.SetActive(true);
    }
    private void OnDisable()
    {
        achievement.SetActive(false);
        WordManager.hitCount = 0;
        WordManager.wordTypedCount = 0;
        WordManager.Instance.words.Clear();
        EventManager.onFalse -= EventManager_onFalse;
        if (parentEnemy.childCount <= 0) return;
        for (int i = 0; i < parentEnemy.childCount; i++)
        {
            Destroy(parentEnemy.GetChild(i).gameObject);
        }
    }
    private void EventManager_onFalse()
    {
        if (curReload > 0) return;
        ammo[falseCounter].SetActive(false);
        falseCounter++;
        if(falseCounter>= ammo.Length)
        {
            curReload = reloadTime;
            falseCounter = 0;
        }
    }

    public bool isZombieKilled;
    public bool isReload;
    private void Update()
    {
        if (playerDie) return;
        if (curReload > 0)
        {
            curReload -= Time.deltaTime;
            if (curReload <= 0)
            {
                isReload = true;
                for (int i = 0; i < ammo.Length; i++)
                {
                    ammo[i].SetActive(true);
                }
                falseCounter = 0;
                SoundManager.Instance.PlaySFX(reloadClip);
            }
            return;
        }
        if (!isReload)
        {
            foreach (char letter in Input.inputString)
            {
                if (Input.anyKeyDown)
                {
                    WordManager.hitCount++;
                    WordManager.Instance.TypeLetter(letter.ToString().ToLower()[0]);
                }
            }
        }


        // SPAWN ===============================================
        if (currentDelay < 0)
        {
            enemyChache = Instantiate(enemyPrefabs, anchorEnemySpawner[Random.Range(0, anchorEnemySpawner.Length - 1)].position, Quaternion.identity);
            enemyChache.Initialize(player);
            enemyChache.transform.parent = parentEnemy;
            currentDelay = Random.Range(spawnDelayMin, spawnDelayMax);
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
        //if (isZombieKilled)
        //{
        //    Debug.Log("Spawn");
        //    int randomIndex = Random.Range(0, posisiSpawner.Length);
        //    SpawnerZombie.instance.SpawnZombie(posisiSpawner[randomIndex].transform.position);

        //    isZombieKilled = false;
        //}

        curStopwatch += Time.deltaTime;
        float minutes = Mathf.FloorToInt(curStopwatch / 60);
        float seconds = Mathf.FloorToInt(curStopwatch % 60);

        t_timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void ShowEnd()
    {
        for (int i = 0; i < parentEnemy.childCount; i++)
        {
            Destroy(parentEnemy.GetChild(i).gameObject);
        }
        achievement.SetActive(true);
        bestWPM.text = WordManager.Instance.GetBestWPM(curStopwatch).ToString("00");
        bestEPM.text = WordManager.Instance.GetBestEPM(curStopwatch).ToString("00");
        bestAccuracy.text = WordManager.Instance.GetAccuracy().ToString("00");
    }
}
