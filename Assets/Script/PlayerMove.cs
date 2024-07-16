using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //private float rotationSpeed = 5f;
    public Animator animator;
    public ZombieMove ZM;

    public static PlayerMove instance;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (ZM.isShooted)
        {
            Debug.Log("Kena Zombie" + ZM.isShooted);
            Destroy(obj);
        }        
    }

    public GameObject bulletPrefab;
    public GameObject posBullet;
    private GameObject obj;
    private int shotCounter;
    public int maxCounter;
    public AudioClip shotClip;
    public void Shoot()
    {

        // Set the bullet's position to the shooter's position
        SoundManager.instance.PlaySFX2("SFX_Shot");
        obj = Instantiate(bulletPrefab, posBullet.transform.position, Quaternion.identity);
        obj.transform.SetParent(posBullet.transform);
        Debug.Log("Tembak");
        shotCounter += 1;
        //bullet.GetComponent<Rigidbody>().velocity = 3 * bullet.transform.forward;
        //bullet.GetComponent<ShotgunBullet>().aimDotPosition = ShotgunBullet.instance.aimDotPosition;
        //transform.LookAt(ShotgunBullet.instance.aimDotPosition);        
    }

    public void DestroyBullet()
    {
        Destroy(obj);
    }
    //public static bool isReloadAnim;
    //private void ReloadAnim()
    //{
    //    animator.SetBool("isReload", false);
    //    isReloadAnim = false;
    //}
}
