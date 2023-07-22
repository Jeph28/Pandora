using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    [SerializeField] private GameObject Pasta1;

    void OnCollisionEnter(Collision collision) 
    {
        GameObject other = collision.gameObject;
        if (other.tag == "pack Pasta")
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait() 
    {
        yield return new WaitForSeconds(0.15f);
        Pasta1.SetActive(true);
        yield return null;
    }
}
