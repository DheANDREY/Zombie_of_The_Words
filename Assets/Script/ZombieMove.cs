using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieMove : MonoBehaviour
{
    private GameObject Player;
    private PlayerMove playerMove;
    //private float speed;
    private CapsuleCollider col_player;
    public BoxCollider bullet;
    private Animator animator;
    public BoxCollider zombieCollider;
    public ShotgunBullet sB;
    Vector3 currentPosition;

    public static ZombieMove instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        col_player = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();

        transform.rotation = Quaternion.LookRotation(Player.transform.position - transform.position);
        currentPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.y > 0.2f)
        {
            animator.SetTrigger("isDeath");
            Invoke("ZombieDie", 2.5f);
        }

        //float distance = Vector3.Distance(transform.position, col_player.transform.position);

        //if (distance > 0)
        //{
        //    Vector3 direction = (col_player.transform.position - transform.position);

        //    transform.rotation = Quaternion.LookRotation(col_player.transform.position - transform.position);
        //    transform.position += direction * Time.deltaTime * speed;
        //}

        if (animator.GetBool("isAttack") == true)
        {
            return; // Hentikan gerakan jika sedang menyerang
        }

    }

    public bool isShooted;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == col_player)
        {
            //speed = 0;
            animator.SetBool("isAttack", true);
            //InvokeRepeating("DealDamage", 1, 3.5f);

                InvokeRepeating("DealDamage2", 1, 3.5f);
            
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            transform.position = position;
            transform.rotation = rotation;
        }
        else 
        {
            Debug.Log("Mati");



            //isDead = true;
            ////playerMove.DestroyBullet();
            //speed = 0;
            //animator.SetTrigger("isDeath");
            ////GameFlow.instance.skor++; // Penambah Skor
            //Invoke("ZombieDie", 2.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Kena Player");
            //speed = 0;
            animator.SetBool("isAttack", true);
            //InvokeRepeating("DealDamage", 1, 3.5f);
            //if (!WD.isZomDead)
            //{
            //    InvokeRepeating("DealDamage2", 1, 3.5f);
            //}
            
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            transform.position = position;
            transform.rotation = rotation;
        }
    }


    private void ZombieDie()
    {        
        GetComponent<Collider>().enabled = false;
        CancelInvoke("DealDamage");
        Destroy(gameObject);
        IngameEndless.Instance.isZombieKilled = true;
        //GameFlow.instance.isZombieKilled = true;
    }
    private bool isDead;
    private void DealDamage()
    {
        if (!isDead)
        {
            GameFlow.instance.TakeDamage(1);
        }
    }
    private void DealDamage2()
    {        
            //PlayerController.instance.Attacked(2);
        
    }
}
