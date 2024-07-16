using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameEndless : MonoBehaviour
{
    public static IngameEndless Instance;
    public float reloadTime;
    public GameObject[] ammo;   public int missBullet;
    public Transform player;
    
    public EnemyController enemyPrefabs;
    public bool playerDie;
    public GameObject achievement;
    public TextMeshProUGUI t_timer, bestWPM, bestEPM,bestAccuracy;
    public TextMeshProUGUI zomCounter, wordCounter;
    private float minutes, seconds; //private float initialTime = 0;
    private float curStopwatch;
    public Transform parentEnemy;
    public AudioClip reloadClip;

    public PlayerMove playerMove;
    private EnemyController enemyChache;
    //[SerializeField]private VFXPooling vfxPooling;
    private float delaySpawn = 4;
    private int falseCounter;
    private void Awake()
    {
        Instance = this;
    }

    public SoundManager soundManager;
    private void Start()
    {
        //vfxPooling = GameObject.FindGameObjectWithTag("Player").GetComponent<VFXPooling>();
    }

    private void OnEnable()
    {
        //seconds = 0;  minutes = 0;
        SpawnerZombie.instance.DisableWarning();
        curStopwatch = 0;
        playerDie = false;
        WordManager.hitCount = 0;
        WordManager.wordTypedCount = 0;
        WordManager.Instance.WordInit();
        EventManager.onFalse += EventManager_onFalse;
        ResetAmmo(); falseCounter = 0;
        GameManager.Instance.envi.SetActive(true);
    }
    private void OnDisable()
    {
        achievement.SetActive(false); //seconds = 0; minutes = 0;
        WordManager.hitCount = 0;
        WordManager.wordTypedCount = 0;
        WordManager.Instance.words.Clear();
        EventManager.onFalse -= EventManager_onFalse;
        //if (parentEnemy.childCount <= 0) return;
        //for (int i = 0; i < parentEnemy.childCount; i++)
        //{
        //    Destroy(parentEnemy.GetChild(i).gameObject);
        //}
    }
    [SerializeField] private Transform posVFXMiss;
    public GameObject wrongType;
    private void EventManager_onFalse()
    {
        //if (curReload <= 0) return;
        if (!PlayerController.isReloadAnim)
        {
            ammo[falseCounter].SetActive(false);
            falseCounter++; 
            missBullet--;    //Debug.Log(missBullet);
            soundManager.PlaySound(SoundEnum.miss);
            //vfxPooling.SpawnVFX(0,posVFXMiss.position, Quaternion.identity);
            ObjectPoolManager.SpawnObject(wrongType, posVFXMiss.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
            //PlayerController.instance.SpawnEM();
            if (falseCounter >= ammo.Length)
            {
                //curReload = missBullet;
                falseCounter = 0;
            }
        }        
    }

    public bool isZombieKilled;
    
    public GameObject cdIcon;
    public Image cdFill;
    private float cdTime = 3f;    public static bool isCDreload;

    private void Update()
    {
        if (playerDie)
        {
            SpawnerZombie.instance.ReturnAllZombiesToPool();
            Time.timeScale = 0f;
            ShowEnd();
            return;
        }

        cdFill.fillAmount = (cdTime/2.25f) / 1;     //Debug.Log("Waktu Reload: "+ cdFill.fillAmount);
        if (isCDreload)
        {
            ReloadIconOn(true);
            cdTime -= Time.deltaTime;
            if (cdTime < 0)
            {
                cdTime = 0;
                isCDreload = false;
            }
        }
        else
        {
            ReloadIconOn(false);
            cdTime = 3f;
        }

        if (missBullet <= 0)
        {
            missBullet = 0;
            isCDreload = true;
            PlayerController.isReloadAnim = true;    isReloadTime = true;             
            StartCoroutine(ReloadCoroutine(0.5f));
            
        }        

        if (!isReloadTime)
        {
            foreach (char letter in Input.inputString)
            {
                if (!isReloadTime)
                {
                    if (Input.anyKeyDown)
                    {
                        //Debug.Log("playerMove.isReload = " + playerMove.isReload);
                        WordManager.hitCount++;
                        WordManager.Instance.TypeLetter(letter.ToString().ToLower()[0]);
                    }
                }
            }
        }     

        curStopwatch += Time.deltaTime;
        minutes = Mathf.FloorToInt(curStopwatch / 60);
        seconds = Mathf.FloorToInt(curStopwatch % 60);

        t_timer.text = string.Format("{0:00} {1:00}", minutes, seconds);
        wordCounter.text = WordManager.correctHitCount.ToString();
        zomCounter.text = WordDisplay.zomDeadCounter.ToString(); 
    }

    public bool isReloadTime;
    private IEnumerator ReloadCoroutine(float delay)
    {
        if (isReloadTime)
        {
            for (int i = 0; i < ammo.Length; i++)
            {
                //Debug.Log("T() playerMove.isReload = " + isReloadTime);
                ammo[i].SetActive(true);
                missBullet++;    //Debug.Log(missBullet);

                soundManager.PlaySound(SoundEnum.reload);

                yield return new WaitForSeconds(delay);
                
            }
            
            falseCounter = 0; isReloadTime = false;
        }
    }
    private void ResetAmmo()
    {
        missBullet = 7;
        // Setiap kali ulang game, Anda mungkin juga perlu memastikan bahwa semua bullet diaktifkan kembali
        for (int i = 0; i < ammo.Length; i++)
        {
            ammo[i].SetActive(true);
        }
    }


    //public int zombiesPerWave; // Jumlah zombie per gelombang
    //private int zombiesRemaining, zombieMax; // Jumlah zombie yang tersisa dalam gelombang saat ini
    //private bool isWaveActive = true;
    //private void StartNewWave()
    //{
    //    //zombiesPerWave = Random.Range(4, 5);        
    //    //if (isWaveActive && zombiesPerWave > 0)
    //    //{
    //        if (delaySpawn <= 0)
    //        {
    //            //SpawnZombie();  zombiesPerWave--;
    //            delaySpawn = 4;
    //        }
    //        else
    //        {
    //            delaySpawn -= Time.deltaTime;
    //            if (zombiesPerWave <= 0)
    //            {
    //                zombiesPerWave = 0;
    //                isWaveActive = false;
    //                Debug.Log("Wave Finished" + zombiesPerWave);
    //            }
    //        }
    //    //}
    //}

    //void SpawnZombie()
    //{
    //    // Instansi zombie baru
    //    enemyChache = Instantiate(enemyPrefabs, anchorEnemySpawner[Random.Range(0, anchorEnemySpawner.Length - 1)].position, Quaternion.identity);
    //    enemyChache.Initialize(player);
    //    enemyChache.transform.parent = parentEnemy;
    //    zombiesRemaining--;

    //    // Cek apakah semua zombie dalam gelombang telah dispawn
    //    if (zombiesRemaining == 0)
    //    {
    //        // Menunggu hingga semua zombie terkalahkan sebelum memulai gelombang baru
    //        zombiesRemaining = 0;
    //        isWaveActive = false;
    //    }
    //}

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

    public void ReloadIconOn(bool isOn)
    {
        if (isOn)
        {
            cdIcon.SetActive(true);
        }
        else
        {
            cdIcon.SetActive(false);
        }
    }
}
