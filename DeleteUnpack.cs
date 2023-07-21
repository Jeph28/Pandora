using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class DeleteUnpack : MonoBehaviour
{
    private bool hasCollided = false;
    public bool status = true;

    void OnCollisionEnter(Collision collision) 
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Packing Machine" && !hasCollided && status)
        {
            hasCollided = true;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait() 
    {
        while (true)
        {
            if (GameManager.RequestUnpack)
            {
                yield return new WaitForSeconds(0.1f);
                GameManager.UnpackOn = GameManager.UnpackOn - 1;
                GameManager.hasCollidedPackingMachine = false;
                GameManager.RequestUnpack = false;
                hasCollided = false;
                status = false;
                gameObject.SetActive(false);
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
