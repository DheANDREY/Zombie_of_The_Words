using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameFlow : MonoBehaviour
{
    public GameObject[] posisiSpawner;
    public GameObject cdIcon;
    //public TextMeshProUGUI skorZombie;
    public bool isZombieKilled;

    public Image hpFill;
    public Image cdFill;

    float hpRemaining;
    public float cdCountdown;
    public bool isCd;
    public int skor;

    public float cdReload;
    public float maxHP;

    public ZombieMove ZM;
    public static GameFlow instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        hpRemaining = maxHP;
        cdCountdown = cdReload;
        for (int i = 0; i < posisiSpawner.Length; i++)
        {
            SpawnerZombie.instance.SpawnZombie(posisiSpawner[i].transform.position);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isZombieKilled)
        {
            int randomIndex = Random.Range(0, posisiSpawner.Length);
            SpawnerZombie.instance.SpawnZombie(posisiSpawner[randomIndex].transform.position);
            isZombieKilled = false;
        }

        if (hpRemaining > 0) // HP Bar
        {           
            if (Input.GetKeyDown(KeyCode.P))
            {
                //hpRemaining -= 1;
                Debug.Log(hpRemaining);
            }            
        }
        else
        {
            hpRemaining = 0;
        }

        if (isCd) // CD Reload Shotgun
        {
            if (cdCountdown > 0)
            {
                cdIcon.SetActive(true);
                cdCountdown -= Time.deltaTime;
                if (cdCountdown < 0)
                {
                    cdCountdown = 0;
                    isCd = false;
                }
                
            }
        }
        else
        {
            cdIcon.SetActive(false);
            cdCountdown = 2;
        }


        hpFill.fillAmount = hpRemaining / maxHP;
        cdFill.fillAmount = cdCountdown / cdReload;
        //skorZombie.text = skor.ToString(); // Skor UI
    }
    public void TakeDamage(int dmg)
    {
        hpRemaining -= dmg;
    }


}
