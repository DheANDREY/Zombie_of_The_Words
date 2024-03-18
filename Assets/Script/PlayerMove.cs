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
        //if (GameFlow.instance.cdCountdown >= 2 && GameFlow.instance.isCd == false)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        if (shotCounter < maxCounter)
        //        {
        //            Debug.Log("Tembak");
        //            animator.SetTrigger("isShoot");
        //            Shoot();
        //        }
        //        else
        //        {
        //            Debug.Log("Animasi Reload");
        //            GameFlow.instance.isCd = true;
        //            shotCounter = 0;
        //        }
        //    }
        //}

        //======== ROTASI CHAR FROM MOUSE ============================================================================
        //float mouseX = Input.GetAxis("Mouse X");
        ////float mouseY = Input.GetAxis("Mouse Y");
        //float rotationAmountX = mouseX * rotationSpeed;
        ////float rotationAmountY = mouseY * rotationSpeed;

        //transform.Rotate(Vector3.up, rotationAmountX);
        ////transform.Rotate(Vector3.right, rotationAmountY);

        //Quaternion currentRotationX = transform.rotation;
        ////currentRotationX.eulerAngles = new Vector3(currentRotationX.eulerAngles.x, Mathf.Clamp(currentRotationX.eulerAngles.y, 5f, 35f), currentRotationX.eulerAngles.z);// Besar Rotasi ke kiri dan ke kanan
        //float clampedYRotation = Mathf.Clamp(currentRotationX.eulerAngles.y, 5f, 35f);
        //float newYRotation = Mathf.Clamp(clampedYRotation, 5f, 35f);
        //currentRotationX.eulerAngles = new Vector3(0, newYRotation, 0);
        //transform.rotation = currentRotationX;

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
