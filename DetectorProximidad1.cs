using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class DetectorProximidad1 : MonoBehaviour
{

    public Transform distanceActivator, lookAtActivator;
    public float distance;
    public Transform activator;
    public bool activeState = false;
    public CanvasGroup target;
    public bool lookAtCamera = true;
    public bool enableInfoPanel = false;
    float alpha;
    public CanvasGroup infoPanel;
    [SerializeField] private TMP_Text textCanva;
    Quaternion originRotation, targetRotation;
    bool changePrincipalText = true;
    [SerializeField] private GameObject Result;
    [SerializeField] private TMP_Text Changetext;


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
                enableInfoPanel = false;
                Result.SetActive(false);
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

        if (GameManager.UnpackOn > 0f && changePrincipalText )
        {
            textCanva.text = "Tomar una muestra con [Y]";
            changePrincipalText = false;
        }

        
    }
    public void ResultCheckPoint(InputAction.CallbackContext callbackContext)
    {
        if (activeState && callbackContext.performed && !changePrincipalText)
        {
            Changetext.text = "Contexto paja y mas... " + "\n" + "\n" + "Humedad : " + GameManager.pastaHumidityPercentageString + "\n" + "Color : " + GameManager.pastaColorString + "\n" + "Craqueo : " + GameManager.pastaCrakingString + "\n" + "Microorganismos : " + GameManager.pastaMicroorganismsString;
            Result.SetActive(true);
        }
        
    }

    

}