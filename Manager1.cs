using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;


public class Manager1 : MonoBehaviour
{

    [SerializeField] private Transform distanceActivator; 
    private Transform lookAtActivator;
    private Transform activator;
    private bool activeState = false;
    public CanvasGroup target;
    public float distance;
    [SerializeField] private GameObject DryerMenu;
    [SerializeField] public TMP_Text MessageDryer;
    //  MessageDryer = 1 "Presiona [Y] para configurar la secaodra"
    //  MessageDryer = 2 "Pulsa [Y] para hacer mantenimiento"
    public bool lookAtCamera = true;
    float alpha;

    Quaternion originRotation, targetRotation;


    void Start()
    {
        originRotation = transform.rotation;
        alpha = activeState ? 1 : -1;
        if (activator == null) activator = Camera.main.transform;
    }

    bool IsTargetNear()
    {
        var distanceDelta = distanceActivator.position - activator.position;
        if (distanceDelta.sqrMagnitude < distance * distance)
        {
            if (lookAtActivator != null)
            {
                var lookAtActivatorDelta = lookAtActivator.position - activator.position;
                if (Vector3.Dot(activator.forward, lookAtActivatorDelta.normalized) > 0.8f)
                    return true;
            }
            var lookAtDelta = target.transform.position - activator.position;
            if (Vector3.Dot(activator.forward, lookAtDelta.normalized) > 0.8f)
                return true;
        }
        return false;
    }

    void Update()
    {
        if (!activeState)
        {
            if (IsTargetNear() && !GameManager.DryerMachine && !GameManager.FailureDryer)
            {
                alpha = 1;
                activeState = true;
            }
        }
        else
        {
            if (!IsTargetNear() || GameManager.DryerMachine)
            {
                alpha = -1;
                activeState = false;
            }
        }
        target.alpha = Mathf.Clamp01(target.alpha + alpha * Time.deltaTime);
        
        if (lookAtCamera)
        {
            if (activeState)
                targetRotation = Quaternion.LookRotation(activator.position - transform.position);
            else
                targetRotation = originRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }

        if (GameManager.NeedsMaintenanceDryer)
        {
            MessageDryer.text = "Presiona [Y] para hacer mantenimiento";
            GameManager.MessageDryer = 2;
        }  
    }

    public void DryerMachine(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && GameManager.MessageDryer == 1 && activeState)
        {
            DryerMenu.SetActive(true);
            GameManager.DryerMenu = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
    }
}