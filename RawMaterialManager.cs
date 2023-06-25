
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RawMaterialManager : MonoBehaviour
{

    [SerializeField] private Transform distanceActivator; 
    private Transform lookAtActivator;
    private Transform activator;
    private bool activeState = false;
    public CanvasGroup target;
    public float distance;
    [SerializeField] private GameObject RawMaterialMenu;
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
            if (IsTargetNear() && GameManager.RawMaterial == 0)
            {
                alpha = 1;
                activeState = true;
            }
        }
        else
        {
            if (!IsTargetNear() || GameManager.RawMaterial != 0)
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
    }

    public void rawMaterialMenu(InputAction.CallbackContext callbackContext)
    {
        if (Settings.Instance.IsGamePaused())
            return;
            
        if (callbackContext.performed && activeState)
        {
            RawMaterialMenu.SetActive(true);
            GameManager.RawMaterialMenu = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
    }
}