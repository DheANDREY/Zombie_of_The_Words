using UnityEngine;
using System.Collections;
public class Bullet :MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void InitializeBullet(Transform _target)
    {
        Vector3 _dir = _target.transform.position- transform.position;
        rb.AddForce(_dir * bulletSpeed);
        StartCoroutine(Lifetime());
    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}