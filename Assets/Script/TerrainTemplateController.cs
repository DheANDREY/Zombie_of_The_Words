using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTemplateController : MonoBehaviour
{
    private MapGenController MGC;
    public float terrainTemplateWidth;
    private void Start()
    {
        MGC = GameObject.FindAnyObjectByType<MapGenController>();
    }

    private void Update()
    {
        //Debug.Log("Belum");
        //Invoke("DeleteLateTerrain", 15f);
        //Debug.Log("Sudah");
    }

    private const float debugLineHeight = 10.0f;

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position + Vector3.up * debugLineHeight / 2, transform.position + Vector3.down * debugLineHeight / 2, Color.green);
    }
    private void DeleteLateTerrain()
    {
        //MGC.RemoveTerrain();
    }
}
