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
    [SerializeField] private TMP_Text textCanva;
    [SerializeField] private TMP_Text Changetext;
    [SerializeField] private GameObject Result;
    [SerializeField] private GameObject ContextCheckPoint;
    private bool ResultBool = false;
    public float distance;
    // string MoreLess = "\u00B1";
    public bool lookAtCamera = true;
    private bool ContextView = true;

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
                ResultBool = false;
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

        if (GameManager.PastaScore > 0f && GameManager.changePrincipalText1CheckPoint2)
        {
            textCanva.text = "Tomar una muestra con [Y]";
            GameManager.changePrincipalText1CheckPoint2 = false;
        }

        if (GameManager.changePrincipalText2CheckPoint2 && GameManager.timeWaitCheckPoint2 > 1)
        {
            GameManager.timeWaitCheckPoint2 -= Time.deltaTime;
            textCanva.text = "Espera  " + GameManager.timeWaitCheckPoint2.ToString("F0") + "  para poder ver el Resultado";
        }

        if (GameManager.timeWaitCheckPoint2 <= 1 && GameManager.changePrincipalText3CheckPoint2)
        {
            textCanva.text = "Ver los resultados del Lab con [Y]";
            GameManager.changePrincipalText3CheckPoint2 = false;
            GameManager.changePrincipalText2CheckPoint2 = true;
        }

        Changetext.text = "Aqui va a ir un pequeno contexto para ofrecerle informacion importante al usuario que le permita entender mejor los valores reflejados abajo " + "\n" + "\n" + "Cantidad empaquetada : " + GameManager.PastaScore.ToString() + "\n" + "\n" + "Peso promedio : 1 Kg" + "\n" + "\n" + "Desviacion del peso : " + GameManager.StdDevWeight.ToString() + "%" + "\n" + "\n" + "Ruptura Mecanica : 1" + "\n" + "\n" + "Acidez : " + GameManager.Acidity + "%" + "\n" + "\n" + "Ceniza : " + GameManager.Ash + "%" + "\n" + "\n" + "Proteina : " + GameManager.Protein + "%";
    }

    public void Context(InputAction.CallbackContext callbackContext)
    {
        // The last condition allow active a modal if there is Unpacked pasta
        if (activeState && callbackContext.performed && !GameManager.changePrincipalText1CheckPoint2 && !GameManager.changePrincipalText2CheckPoint2)
        {
            if (ContextView)
            {
                ContextCheckPoint.SetActive(true);
                GameManager.ContextCheckPoint2 = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ContextView = false;
            }
            else if(!ContextView)
            {
                GameManager.timeCheckPoint2 = Time.time;
                GameManager.changePrincipalText2CheckPoint2 = true;
            }
        }
    }

    public void ResultCheckPoint2(InputAction.CallbackContext callbackContext)
    {
        if (ResultBool)
        {
            Result.SetActive(false);
            ResultBool = false; 
        }
        else if (!GameManager.changePrincipalText3CheckPoint2 && activeState)
        {
            Result.SetActive(true);
            ResultBool = true;
        }
    }

    public void Accept()
    {
        ContextCheckPoint.SetActive(false);
        GameManager.ContextCheckPoint2 = false;
        GameManager.timeCheckPoint2 = Time.time;
        GameManager.changePrincipalText2CheckPoint2 = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
