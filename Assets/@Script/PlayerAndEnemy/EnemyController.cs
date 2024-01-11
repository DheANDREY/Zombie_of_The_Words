using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float damage;
    public float attackRate;
    public WordDisplay wordDisplay;
    private Transform target;
    private float curRate;

    public bool isDead;
    private Animator animator;
    // Start is called before the first frame update
    public void Initialize(Transform _target)
    {
        target = _target;
        WordManager.Instance.AddWord(wordDisplay);
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            isDead = true;
            //playerMove.DestroyBullet();
            speed = 0;
            animator.SetTrigger("isDeath");
            //GameFlow.instance.skor++; // Penambah Skor
            Invoke("ZombieDie", 2.5f);
            //return;
        }

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            if (curRate < 0)
            {
                curRate = attackRate;
                PlayerController.instance.Attacked(damage);
            }
            curRate -= Time.deltaTime;
            return;
        }
        else
        {
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
}
