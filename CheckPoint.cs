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
    [SerializeField] private GameObject Result;
    [SerializeField] private GameObject ReferenceValuesScreen;
    [SerializeField] private TMP_Text Changetext;
    [SerializeField] private GameObject ContextCheckPoint;
    private bool ResultBool = false;
    // private float timeCheckPoint;
    public float distance;
    public bool lookAtCamera = true;
    private bool ContextView = true;
    string MoreLess = "\u00B1";


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
            if (IsTargetNear() && !GameManager.activeStateManager1 && !GameManager.activeStateManager2)
            {
                alpha = 1;
                activeState = true;
            }
        }
        else
        {
            if (!IsTargetNear() || GameManager.activeStateManager1 || GameManager.activeStateManager2)
            {
                alpha = -1;
                activeState = false;
                Result.SetActive(false);
                ResultBool = false;
                if (GameManager.timeWaitCheckPoint1 <= 1)
                {
                    textCanva.text = "Ver los resultados del Lab con [Y]";
                }
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

        if (GameManager.UnpackOn > 0f && GameManager.changePrincipalText1CheckPoint1)
        {
            textCanva.text = "Tomar una muestra con [Y]";
            GameManager.changePrincipalText1CheckPoint1 = false;
        }

        if (GameManager.changePrincipalText2CheckPoint1 && GameManager.timeWaitCheckPoint1 > 1)
        {
            GameManager.timeWaitCheckPoint1 -= Time.deltaTime;
            textCanva.text = "Espera  " + GameManager.timeWaitCheckPoint1.ToString("F0") + "  para poder ver el Resultado";
        }

        if (GameManager.timeWaitCheckPoint1 <= 1 && GameManager.changePrincipalText3CheckPoint1)
        {
            textCanva.text = "Ver los resultados del Lab con [Y]";
            GameManager.changePrincipalText3CheckPoint1 = false;
            GameManager.changePrincipalText2CheckPoint1 = true;
        }

        Changetext.text = "La prueba de laboratorio fue realizada con éxito. Se recomienda calcular el cv para analizar la variabilidad de los datos y como esto afecta la calidad del producto." + "\n" + "\n" + "Cantidad producida : " + GameManager.UnpackPastaScore + "\n" + "\n" + "Promedio de la Humedad : " + GameManager.pastaHumidityPercentageString + "\n" + "\n" + "Desviacion de la Humedad : " + MoreLess + " " + GameManager.StdDevHumidity + "%" + "\n" + "\n"+ "Color : " + GameManager.pastaColorString + "\n" + "\n" + "Craqueo : " + GameManager.pastaCrakingString + "\n" + "\n" + "Microorganismos : " + GameManager.pastaMicroorganismsString;
    }

    public void Context(InputAction.CallbackContext callbackContext)
    {
         if (Settings.Instance.IsGamePaused())
            return;

        // The last condition allow active a modal if there is Unpacked pasta
        if (activeState && callbackContext.performed && !GameManager.changePrincipalText1CheckPoint1 && !GameManager.changePrincipalText2CheckPoint1)
        {
            if (ContextView)
            {
                ContextCheckPoint.SetActive(true);
                GameManager.ContextCheckPoint1 = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ContextView = false;
            }
            else if(!ContextView)
            {
                GameManager.timeCheckPoint1 = Time.time;
                GameManager.changePrincipalText2CheckPoint1 = true;
            }
        }
    }

    public void ResultCheckPoint(InputAction.CallbackContext callbackContext)
    {
         if (Settings.Instance.IsGamePaused())
            return;
            
        if (ResultBool)
        {
            Result.SetActive(false);
            ResultBool = false;
            textCanva.text = "Ver los resultados del Lab con [Y]";
        }
        else if (!GameManager.changePrincipalText3CheckPoint1 && activeState)
        {
            Result.SetActive(true);
            ResultBool = true;
            textCanva.text = "Manten [X] para ver los valores de referencia";
        }
    }

    public void ReferenceValue(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && activeState && ResultBool)
        {
            ReferenceValuesScreen.SetActive(true);
        }
        if (callbackContext.canceled)
        {
            ReferenceValuesScreen.SetActive(false);
            
        }
    }

    public void Accept()
    {
        ContextCheckPoint.SetActive(false);
        GameManager.ContextCheckPoint1 = false;
        GameManager.timeCheckPoint1 = Time.time;
        GameManager.changePrincipalText2CheckPoint1 = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}