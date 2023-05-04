using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class CheckPoint : MonoBehaviour
{

    public Transform distanceActivator; 
    private Transform lookAtActivator, activator;
    private bool activeState = false;
    public CanvasGroup target;
    float alpha;
    [SerializeField] private TMP_Text textCanva;
    Quaternion originRotation, targetRotation;
    bool changePrincipalText = true;
    bool changePrincipalText1 = false;
    bool changePrincipalText2 = true;
    [SerializeField] private GameObject Result;
    [SerializeField] private TMP_Text Changetext;
    private float timeCheckPoint;
    [SerializeField] float timeWait = 60;
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

        if (Time.time - timeCheckPoint < 60f && changePrincipalText1 && timeWait > 1)
        {
            timeWait -= Time.deltaTime;
            textCanva.text = "Espera  " + timeWait.ToString("F0") + "  para poder ver el Resultado";
        }

        if (timeWait <= 1 && changePrincipalText2)
        {
            textCanva.text = "Ver los resultados del Lab con [Y]";
            changePrincipalText2 = false;
        }
    }

    public void ResultCheckPoint(InputAction.CallbackContext callbackContext)
    {
        // The last condition allow active a modal if there is Unpacked pasta
        if (activeState && callbackContext.performed && !changePrincipalText)
        {
            changePrincipalText1 = true;
            timeCheckPoint = Time.time;
            if (!changePrincipalText2)
            {
                Changetext.text = "Contexto... " + "\n" + "\n" + "Humedad : " + GameManager.pastaHumidityPercentageString + "\n" + "Color : " + GameManager.pastaColorString + "\n" + "Craqueo : " + GameManager.pastaCrakingString + "\n" + "Microorganismos : " + GameManager.pastaMicroorganismsString;
                Result.SetActive(true); 
            }
        }
    }

    

}