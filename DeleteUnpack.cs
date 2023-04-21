using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class DeleteUnpack : MonoBehaviour
{
   private bool collisionStatus = false;

    void OnCollisionEnter(Collision collision) 
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Packing Machine")
        {
            StartCoroutine(Wait());
            collisionStatus = true;

        }
    }

    public void switch1(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && GameManager.activeStatePacking && GameManager.PackingMachine && collisionStatus)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait() 
    {
        yield return new WaitForSeconds(0.1f);
        if (GameManager.PackingMachine)
        {
            gameObject.SetActive(false); 
            GameManager.UnpackOn = GameManager.UnpackOn - 1;
        } 
        yield return null;
    }
}
