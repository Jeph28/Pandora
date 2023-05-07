using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Manager2 : MonoBehaviour
{
    [SerializeField] private Transform distanceActivator; 
    private Transform lookAtActivator;
    private Transform activator;
    private bool activeState = false;
    public CanvasGroup target;
    public float distance;
    [SerializeField] private GameObject PackingMenu;
    public bool lookAtCamera = true;
    float alpha;
    Quaternion originRotation, targetRotation;
    
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (!activeState && !GameManager.PackingMachine)
        {
            if (IsTargetNear())
            {
                alpha = 1;
                activeState = true;
                }
        }
        else
        {
            if (!IsTargetNear() || GameManager.PackingMachine)
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

    public void PackingMachine(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && GameManager.MessagePacking == 1 && activeState)
        {
            PackingMenu.SetActive(true);
            GameManager.PackingMenu = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
    }
}
