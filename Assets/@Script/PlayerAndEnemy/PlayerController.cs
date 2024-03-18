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
    private float curentHP;
    private void Start()
    {
        //SM = GameObject.FindAnyObjectByType<SoundManager>();
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

    public GameObject target, shotEffect, missEffect, damagedEffect;   private GameObject instantiatedEffect;
    public Animator animator;   public Transform posShotEffect;
    private void EventManager_onType()
    {
        GunToTarget rotateToTarget = GameObject.FindAnyObjectByType<GunToTarget>();
        //PlayerMove rotateChar = GetComponent<PlayerMove>();
        Transform targetTransform = target.transform;

        //Vector3 direction = targetTransform.position - rotateToTarget.transform.position;

        //// Menghitung rotasi yang diinginkan dalam bentuk quaternion
        //Quaternion desiredRotation = Quaternion.LookRotation(direction);

        //// Mengonversi rotasi ke dalam Euler angle (derajat) untuk membatasi nilai rotasi hanya antara 10 hingga 70 derajat
        //Vector3 euler = desiredRotation.eulerAngles;
        //euler.x = Mathf.Clamp(euler.x, -40f, -65f);

        //// Mengonversi kembali ke quaternion
        //desiredRotation = Quaternion.Euler(euler);

        //rotateToTarget.transform.LookAt(targetTransform);

        //SoundManager.Instance.PlaySFX(shotClip);
        soundManager.PlaySound(SoundEnum.shot);
        animator.SetTrigger("isShoot");

        instantiatedEffect = Instantiate(shotEffect, posShotEffect.position, Quaternion.identity);

        //rotateToTarget.transform.rotation = desiredRotation;

        //transform.LookAt(currentEnemy.transform);
        //Bullet go = Instantiate(bulletPrefabs, weaponTip.transform.position, Quaternion.identity);
        //go.InitializeBullet(currentEnemy.transform);
    }

    public Transform[] posDamaged;
    public void Attacked(float _damage)
    {
        //SoundManager.PlaySFX(hittedClip);
        curentHP -= _damage;
        soundManager.PlaySound(SoundEnum.hitted);
        instantiatedEffect = Instantiate(damagedEffect, posDamaged[Random.Range(0, posDamaged.Length - 1)].position, Quaternion.identity);
            //curentHP -= _damage;
            s_HP.value = curentHP;
            if (curentHP <= 0)
            {
                IngameEndless.Instance.playerDie = true;
                Time.timeScale = 0f;
                IngameEndless.Instance.ShowEnd();
            }        
    }

    public Transform posMissFX;
    public void SpawnEffectMiss()
    {
        instantiatedEffect = Instantiate(missEffect, posShotEffect.position, Quaternion.identity);
        instantiatedEffect.transform.SetParent(posMissFX);
    }

    public static bool isReloadAnim;
    private void ReloadAnim()
    {
        animator.SetBool("isReload", false);
        isReloadAnim = false;
    }
}
