using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class CheckPoint2 : MonoBehaviour
{

    public Transform distanceActivator;
    private Transform activator, lookAtActivator;
    private bool activeState = false;
    public CanvasGroup target;
    [SerializeField] private TMP_Text Changetext;
    [SerializeField] private GameObject Result;
    public float distance;

    public bool lookAtCamera = true;

    float alpha;
    //public CanvasGroup infoPanel;
    Quaternion originRotation, targetRotation;

    void Start()
    {
        originRotation = transform.rotation;
        alpha = activeState ? 1 : -1;
        if (activator == null) activator = Camera.main.transform;
        //infoIcon.SetActive(infoPanel != null);
    }

    bool IsTargetNear()
    {
        var distanceDelta = distanceActivator.position - activator.position;
        if (distanceDelta.sqrMagnitude < distance * distance)
        {
            if (lookAtActivator != null)
            {
                var lookAtActivatorDelta = lookAtActivator.position - activator.position;
                if (Vector3.Dot(activator.forward, lookAtActivatorDelta.normalized) > 0.95f)
                    return true;
            }
            var lookAtDelta = target.transform.position - activator.position;
            if (Vector3.Dot(activator.forward, lookAtDelta.normalized) > 0.95f)
                return true;
        }
        return false;
    }

    void Update()
    {
        if (!activeState)
        {
            if (IsTargetNear() && GameManager.PastaScore > 0f)
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

        Changetext.text = "Contexto... " + "\n" + "\n" + "Desviacion del Peso : " + GameManager.WeightDeviationString + "\n" + "Pastas Producidas : " + GameManager.PastaScore.ToString() + "\n" + "Ruptura Mecanica";
    }

    public void DestroyScript(InputAction.CallbackContext callbackContext)
    {
        if (activeState && callbackContext.performed)
        {
            Result.SetActive(true);
        }
        
    }

}
