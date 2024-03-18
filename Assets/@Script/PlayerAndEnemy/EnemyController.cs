using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed, damage;
    //public float phaseMultiplier, numIntoMaxLvl;
    public float attackRate;
    public WordDisplay wordDisplay;
    private Transform target;
    private float curRate;

    public bool isDead;
    private Animator animator;
    private CapsuleCollider collider;
    private CharMoveController CMC;
    private Transform charTransform;
    //private SoundManager soundManager;
    public static EnemyController instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //phaseMultiplier = ((speed * 4) - speed) / numIntoMaxLvl; Debug.Log(phaseMultiplier);
        // Cari game object dengan nama "charMain"
        GameObject charMainGameObject = GameObject.Find("CharMain");

        // Periksa apakah game object ditemukan
        if (charMainGameObject != null)
        {
            // Dapatkan komponen Transform dari game object yang ditemukan
            charTransform = charMainGameObject.transform;
        }
        else
        {
            Debug.LogError("Game object 'charMain' not found!");
        }

        collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        collider_player = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
        CMC = GameObject.FindObjectOfType<CharMoveController>();
    }

    public void Initialize(Transform _target)
    {
        target = _target;
        WordManager.Instance.AddWord(wordDisplay);    
    }

    private Vector3 currentPosition;
    public bool isIncrStat;
    void Update()
    {
        if (target == null)
        {
            isDead = true;
            //playerMove.DestroyBullet();
            speed = 0;
        }
        currentPosition = transform.position;     
        currentPosition.y = 0.15f;

        // Menetapkan posisi yang diperbarui
        transform.position = currentPosition;
        if (currentPosition.x < charTransform.position.x)
        {
            currentPosition.x = charTransform.position.x;
        }

        //if (speed > speed * 4)
        //{
        //    speed = speed * 4;
        //}
        //if (damage > damage * 4)
        //{
        //    damage = damage * 4;
        //}

        //if (Vector3.Distance(transform.position, charTransform.position) < 1.1f)
        //{
        if (isAttackZom && isTouchPlayer)
            {
            transform.position = lastCollisionPosition;
            if (curRate < 0) 
                {
                    if (!wordDisplay.isZomDead)
                    {
                    animator.SetBool("isAttack", true);
                    PlayerController.instance.Attacked(damage);
                    curRate = attackRate;
                        //PlayerController.instance.Attacked(damage);
                    }                        
                }
                curRate -= Time.deltaTime;
                return;
            }
            else
            {
                transform.LookAt(target);
                transform.position = Vector3.MoveTowards(transform.position, charTransform.position, speed * Time.deltaTime);
                isTouchPlayer = true;
            }


        //if (!isTouchPlayer)
        //{
        //    transform.LookAt(target);
        //    transform.position = Vector3.MoveTowards(transform.position, charTransform.position, speed * Time.deltaTime);
        //    isTouchPlayer = true;
        //}
    }

    private CapsuleCollider collider_player;    public bool isAttackZom;
    private Vector3 lastCollisionPosition;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == collider_player)
        {
            //Debug.Log("Kena Player"); Debug.Log(curRate);
            isTouchPlayer = true;
            speed = 0.025f;
            isAttackZom = true;
            //animator.SetBool("isAttack", true); 
            CMC.isDamaged = true;
            //Debug.Log("Speed = "+speed);
            // Simpan posisi saat terjadi collision
            lastCollisionPosition = transform.position;
        }
        else
        {
            Debug.Log("Missing Player");
        }
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.collider == collider_player)
    //    {

    //        Debug.Log("Kena Player");
    //        speed = 0.1f;
    //        animator.SetBool("isAttack", true); CMC.isDamaged = true;
    //        isAttackZom = true;
    //        //InvokeRepeating("DealDamage", 1, 3.5f);
    //        //Vector3 position = transform.position;
    //        //Quaternion rotation = transform.rotation;
    //        //transform.position = position;
    //        //transform.rotation = rotation;
    //    }
    //    else
    //    {
    //        Debug.Log("Missing");
    //    }
    //}

    bool isTouchPlayer = true;
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider == collider_player)
        {
            //Debug.Log("Lepas Player");
            //animator.SetBool("isAttack", false);
            isTouchPlayer = false; //Debug.Log("Touch?: "+isTouchPlayer);
            // Tetapkan kembali posisi ke posisi saat terjadi collision
            transform.position = lastCollisionPosition;

        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag=="Player")
    //    {
    //        Debug.Log("Kena Player");
    //        speed = 0;
    //        animator.SetBool("isAttack", true);

    //        Vector3 position = transform.position;
    //        Quaternion rotation = transform.rotation;
    //        transform.position = position;
    //        transform.rotation = rotation;
    //    }
    //}

    public void ZombieDie()
    {
        GetComponent<Collider>().enabled = false;
        CancelInvoke("DealDamage");
        
        //soundManager.PlaySound(SoundEnum.ZomDead);
        Destroy(gameObject);
        IngameEndless.Instance.isZombieKilled = true;
        //GameFlow.instance.isZombieKilled = true;
    }

    public void ZomDamagedAnim()
    {
        animator.SetTrigger("isHitted");
    }
}
