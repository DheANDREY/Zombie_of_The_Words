using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    //public float speed;  
    public AudioClip shotClip,hittedClip;
    public Transform weaponTip;
    public Bullet bulletPrefabs;
    public float maxHP;
    public Slider s_HP;

    private EnemyController currentEnemy;
    private float curentHP;
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

    private void OnDisable()
    {
        EventManager.onType -= EventManager_onType;
        EventManager.onAddTarget -= EventManager_onAddTarget;
    }

    public GameObject target;
    public Animator animator;
    private void EventManager_onType()
    {
        PlayerMove characterController = GetComponent<PlayerMove>();
        Transform targetTransform = target.transform;
        characterController.transform.LookAt(targetTransform);
        SoundManager.Instance.PlaySFX(shotClip);
        animator.SetTrigger("isShoot");
        transform.LookAt(currentEnemy.transform);
        Bullet go = Instantiate(bulletPrefabs, weaponTip.transform.position, Quaternion.identity);
        go.InitializeBullet(currentEnemy.transform);
    }
    
    public void Attacked(float _damage)
    {
        SoundManager.Instance.PlaySFX(hittedClip);
        curentHP -= _damage;
        s_HP.value = curentHP;
        if(curentHP<=0)
        {
            IngameEndless.Instance.playerDie = true;
            IngameEndless.Instance.ShowEnd();
        }
    }
}
