using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackingConveyorbelt : MonoBehaviour
{
    private Material material;
    
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        GetComponent<MeshRenderer>().material.mainTextureOffset += new Vector2(0, 1) * GameManager.PackingconveyorSpeed * Time.deltaTime;
    }
}
