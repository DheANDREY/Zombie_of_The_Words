using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLockRotation : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        //GameObject cameras = GameObject.FindGameObjectWithTag("YourTag");
        //Camera cameraComponent = GetComponent<Camera>();
        ////mainCamera = FindObjectOfType<Camera>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            // Dapatkan arah dari papan gambar ke posisi kamera
            Vector3 directionToCamera = mainCamera.transform.position - transform.position;
            directionToCamera.y = 0f; // Hanya pertahankan komponen horizontal

            // Buat rotasi dari arah tersebut
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera, Vector3.up);

            // Terapkan rotasi ke papan gambar
            transform.rotation = targetRotation;
        }
    }
}
