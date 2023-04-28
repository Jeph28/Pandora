using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager2 : MonoBehaviour
{
    [SerializeField] private Transform distanceActivator; 
    private Transform lookAtActivator;
    private Transform activator;
    private bool activeState = false;
    public CanvasGroup target;
    public float distance;
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
                // enableInfoPanel = false;
                // Result.SetActive(false);
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
