using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour
{
    public float durationDestroy;
    void Update()
    {
        Invoke("DestroyEffect", durationDestroy);
    }

    private void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
