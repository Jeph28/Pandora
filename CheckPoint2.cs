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
    [SerializeField] private GameObject ReferenceValuesScreen2;
    [SerializeField] private GameObject ContextCheckPoint;
    private bool ResultBool = false;
    public float distance;
    public bool lookAtCamera = true;
    private bool ContextView = true;
    private float MechanicalBreak;
    private float MechanicalBreakPorcent;
    string MoreLess = "\u00B1";

    float alpha;
    //public CanvasGroup infoPanel;
    Quaternion originRotation, targetRotation;

    void Start()
    {
        originRotation = transform.rotation;
        alpha = activeState ? 1 : -1;
        if (activator == null) activator = Camera.main.transform;
        MechanicalBreakPorcent = Random.Range(10f, 20f);
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
                if (GameManager.timeWaitCheckPoint2 <= 1)
                {
                    textCanva.text = "Ver los resultados del Laboratorio con [Y]";
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

        if (GameManager.PastaScore > 0f && GameManager.changePrincipalText1CheckPoint2)
        {
            textCanva.text = "Toma una muestra presionando [Y]";
            GameManager.changePrincipalText1CheckPoint2 = false;
        }

        if (GameManager.changePrincipalText2CheckPoint2 && GameManager.timeWaitCheckPoint2 > 1)
        {
            GameManager.timeWaitCheckPoint2 -= Time.deltaTime;
            textCanva.text = "Espera  " + GameManager.timeWaitCheckPoint2.ToString("F0") + "  para poder ver el Resultado";
        }

        if (GameManager.timeWaitCheckPoint2 <= 1 && GameManager.changePrincipalText3CheckPoint2)
        {
            textCanva.text = "Ver los resultados del Laboratorio con [Y]";
            GameManager.changePrincipalText3CheckPoint2 = false;
            GameManager.changePrincipalText2CheckPoint2 = true;
        }

        MechanicalBreak = (GameManager.PastaScore * MechanicalBreakPorcent)/100;
        Changetext.text = "Recuerda comparar la amplitud de su intervalo natural de variación con la fórmula de μ ± 3σ para verificar si tus resultados cumplen con la norma. En la página 9 del manual esta la relación entre las variables de la maquina y los parámetros de la pasta" + "\n" + "\n" + "Cantidad empaquetada : " + GameManager.PastaScore.ToString() + "\n" + "\n" + "Peso promedio : " + GameManager.pastaWeight.ToString("F2") + " Kg" + "\n" + "\n" + "Desviacion del peso : " + MoreLess + " " + GameManager.StdDevWeight.ToString("F4") + " Kg" + "\n" + "\n" + "Ruptura Mecanica : " + Mathf.FloorToInt(MechanicalBreak)+ "\n" + "\n" + "Acidez : " + GameManager.Acidity + "%" + "\n" + "\n" + "Ceniza : " + GameManager.Ash + "%" + "\n" + "\n" + "Proteina : " + GameManager.Protein + "%";
    }

    public void Context(InputAction.CallbackContext callbackContext)
    {
         if (Settings.Instance.IsGamePaused())
            return;

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
         if (Settings.Instance.IsGamePaused())
            return;
            
        if (ResultBool)
        {
            Result.SetActive(false);
            ResultBool = false;
            textCanva.text = "Ver los resultados del Laboratorio con [Y]";
        }
        else if (!GameManager.changePrincipalText3CheckPoint2 && activeState)
        {
            Result.SetActive(true);
            ResultBool = true;
            textCanva.text = "Manten presionado [X] para ver las referencias";
        }
    }

    public void ReferenceValue2(InputAction.CallbackContext callbackContext)
    {
        if (Settings.Instance.IsGamePaused())
            return;
            
        if (callbackContext.performed && activeState && ResultBool)
        {
            ReferenceValuesScreen2.SetActive(true);
        }
        if (callbackContext.canceled)
        {
            ReferenceValuesScreen2.SetActive(false);
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
