using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchOutService : MonoBehaviour
{

    public Transform distanceActivator;
    private Transform activator, lookAtActivator;
    private bool activeState = false;
    public CanvasGroup target;
    float alpha;
    Quaternion originRotation, targetRotation;
    public float distance;
    public bool lookAtCamera = true;


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
            if (IsTargetNear())
            {
                alpha = 1;
                activeState = true;
                
            }
        }
        else
        {
            if (!IsTargetNear())
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


    

}