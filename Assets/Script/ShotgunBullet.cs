using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    [SerializeField]private Rigidbody rb;
    private float speed = 20f;
    private GameObject aimTarget;
    [SerializeField] private CapsuleCollider Zombie;


    private void Start()
    {
        aimTarget = GameObject.FindGameObjectWithTag("Target");
        Zombie = GameObject.FindGameObjectWithTag("Zombie").GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, aimTarget.transform.position);

        if (distance > 0)
        {
            //Debug.Log(aimTarget.transform.position);
            Vector3 direction = (aimTarget.transform.position - transform.position);

            transform.rotation = Quaternion.LookRotation(aimTarget.transform.position - transform.position);
            transform.position += direction * Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {                
        if (collision.gameObject.tag == "Target")
        {
            //Debug.Log("Kena Target");
            DestroyBullet();
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider == Zombie)
    //    {
    //        Debug.Log("Kena Zombie");
    //        DestroyBullet();
    //    }
    //}
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
