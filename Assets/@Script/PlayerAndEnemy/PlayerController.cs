using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    //public float speed;  
    public AudioClip shotClip,hittedClip;
    public SoundManager soundManager;
    public Transform weaponTip;
    public Bullet bulletPrefabs;
    public float maxHP;
    public Slider s_HP;

    private EnemyController currentEnemy;
    //[SerializeField] private VFXPooling vfxPooling;
    private float curentHP;
    //public int initialPoolSize = 15;
    //public GameObject poolParent;
    //private GameObjectPool vfxPool;
    private void Start()
    {
        //SM = GameObject.FindAnyObjectByType<SoundManager>();
        //vfxPool = new GameObjectPool(missEffect, initialPoolSize, poolParent);
    }
    private void Update()
    {
        if (isReloadAnim)
        {
            //Debug.Log("Animasi Reload");
            IngameEndless.Instance.ReloadIconOn(true);
            animator.SetBool("isReload", true);

            Invoke("ReloadAnim", 0.75f);

        }
    }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        EventManager.onType += EventManager_onType;
        EventManager.onAddTarget += EventManager_onAddTarget;
        curentHP = maxHP;
        s_HP.maxValue = curentHP;
        s_HP.value = curentHP;
    }

    private void EventManager_onAddTarget(EnemyController _enemy)
    {
        currentEnemy = _enemy;
    }

    public PlayerMove playerMove;
    private void OnDisable()
    {
        if (!PlayerController.isReloadAnim)  //&& !IngameEndless.Instance.isReloadTime
        {
            EventManager.onType -= EventManager_onType;
            EventManager.onAddTarget -= EventManager_onAddTarget;
        }
        else
        {
            Debug.Log("Please Wait, Reloading");
        }
    }

    public GameObject target, damagedEffect, shotEffect;   private GameObject instantiatedEffect;
    public Animator animator;   public Transform posShotEffect;
    private void EventManager_onType()
    {
        GunToTarget rotateToTarget = GameObject.FindAnyObjectByType<GunToTarget>();
        //PlayerMove rotateChar = GetComponent<PlayerMove>();
        Transform targetTransform = target.transform;

        soundManager.PlaySound(SoundEnum.shot);
        animator.SetTrigger("isShoot");

        //instantiatedEffect = Instantiate(shotEffect, posShotEffect.position, Quaternion.identity);
        //vfxPooling.SpawnVFX(1, posShotEffect.position, Quaternion.identity);
        ObjectPoolManager.SpawnObject(shotEffect, posShotEffect.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
    }

    public Transform[] posDamaged;  
    public void Attacked(float _damage)
    {
        //SoundManager.PlaySFX(hittedClip);
        curentHP -= _damage;
        soundManager.PlaySound(SoundEnum.hitted);
        //instantiatedEffect = Instantiate(damagedEffect, posDamaged[Random.Range(0, posDamaged.Length - 1)].position, Quaternion.identity);
        //vfxPooling.SpawnVFX(2, posShotEffect.position, Quaternion.identity);
        ObjectPoolManager.SpawnObject(damagedEffect, posDamaged[Random.Range(0, posDamaged.Length - 1)].position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
            //curentHP -= _damage;
            s_HP.value = curentHP;
            if (curentHP <= 0)
            {
                IngameEndless.Instance.playerDie = true;
                //SpawnerZombie.instance.ReturnAllZombiesToPool();
                //Time.timeScale = 0f;
                //IngameEndless.Instance.ShowEnd();
            }        
    }

    public static bool isReloadAnim;
    private void ReloadAnim()
    {
        animator.SetBool("isReload", false);
        isReloadAnim = false;
    }
}
