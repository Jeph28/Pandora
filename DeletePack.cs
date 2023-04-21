using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePack : MonoBehaviour
{
   void OnCollisionEnter(Collision collision) 
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Dead Line")
        {
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait() 
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        yield return null;
    }
}
