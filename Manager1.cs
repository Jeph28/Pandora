
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Manager1 : MonoBehaviour
{

    [SerializeField] private Transform distanceActivator; 
    private Transform lookAtActivator;
    private Transform activator;
    private bool activeState = false;
    public CanvasGroup target;
    public float distance;
    [SerializeField] private GameObject DryerMenu;
    [SerializeField] private GameObject MaintenanceDryerMenu;
    [SerializeField] private TMP_Text textMaintenanceDryerMenu;

    [SerializeField] public TMP_Text MessageDryer;
    //  MessageDryer = 1 "Presiona [Y] para configurar la secaodra"
    //  MessageDryer = 2 "Pulsa [Y] para hacer mantenimiento"
    //  MessageDyer = 3 "Espera x segundos"
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
            if (IsTargetNear() && !GameManager.DryerMachine && !GameManager.FailureInProgressDryer)
            {
                alpha = 1;
                activeState = true;
                GameManager.activeStateManager1 = activeState;
            }
        }
        else
        {
            if (!IsTargetNear() || GameManager.DryerMachine || GameManager.FailureInProgressDryer)
            {
                alpha = -1;
                activeState = false;
                GameManager.activeStateManager1 = activeState;
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

        if (GameManager.NeedsMaintenanceDryer && GameManager.changeMessageMaintenanceDryer)
        {
            MessageDryer.text = "Presiona [Y] para hacer mantenimiento";
            GameManager.MessageDryer = 2;
            GameManager.changeMessageMaintenanceDryer = false;
            GameManager.MaintenanceCounterDryer ++;

            if (GameManager.MaintenanceCounterDryer == 2 && !GameManager.FailureRestartDryer)
            {
                GameManager.FailureDryer = true;
                GameManager.FailureRestartDryer = true;
            }
        }

        //Maintenance time Dryer
        if (GameManager.CountDownMaintenanceDryer && GameManager.MaintenanceTimeDryer > 1)
        {
        GameManager.MaintenanceTimeDryer -= Time.deltaTime;
        MessageDryer.text = "Espera " + GameManager.MaintenanceTimeDryer.ToString("F0") + " segundos";
        GameManager.MessageDryer = 3;
        }

        if (GameManager.MaintenanceTimeDryer <= 1 && !GameManager.ReadyMaintenanceDryer)
        {
            MessageDryer.text = "Presiona [Y] para configurar";
            GameManager.ReadyMaintenanceDryer = true;
            GameManager.MessageDryer = 1;
            GameManager.MaintenanceDryer = false;
            GameManager.CountDownMaintenanceTimeDryer = GameManager.timeBetweenMaintenanceDryer;
            GameManager.NeedsMaintenanceDryer = false;
            GameManager.CountDownMaintenanceDryer = false;
        }
    }

    public void DryerMachine(InputAction.CallbackContext callbackContext)
    {
         if (Settings.Instance.IsGamePaused())
            return;

        if (callbackContext.performed && GameManager.MessageDryer == 1 && activeState)
        {
            DryerMenu.SetActive(true);
            GameManager.DryerMenu = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
    }

    public void MaintenanceDryerMachine(InputAction.CallbackContext callbackContext)
    {
         if (Settings.Instance.IsGamePaused())
            return;
            
        if (callbackContext.performed && GameManager.MessageDryer == 2 && activeState)
        {
            GameManager.MaintenanceCostDryer = Random.Range(80, 120);
            GameManager.MaintenanceTimeDryer = Random.Range(15, 20);
            textMaintenanceDryerMenu.text = "Es momento de realizarle el mantenimiento preventivo programado al ventilador de la máquina secadora, esto tiene un costo de $" + GameManager.MaintenanceCostDryer.ToString("F0") + " y se demora un tiempo de " + GameManager.MaintenanceTimeDryer.ToString("F0") + " segundos, así que eres tu como Ingeniero Industrial el que tiene que decidir si realizarle el mantenimiento o no. Recuerda que todas tus decisiones afectarán a la calidad del producto terminado.";
            MaintenanceDryerMenu.SetActive(true);
            GameManager.ReadyMaintenanceDryer = false;
            GameManager.MaintenanceDryerMenu = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
    }
}