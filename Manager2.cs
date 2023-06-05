using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Manager2 : MonoBehaviour
{
    [SerializeField] private Transform distanceActivator; 
    private Transform lookAtActivator;
    private Transform activator;
    private bool activeState = false;
    public CanvasGroup target;
    public float distance;
    [SerializeField] private GameObject PackingMenu;
    [SerializeField] private GameObject MaintenancePackingMenu;
    [SerializeField] private TMP_Text textMaintenancePackingMenu;
    [SerializeField] public TMP_Text MessagePacking;
    //  MessagePacking = 1 "Presiona [Y] para configurar la empaquetadora"
    //  MessagePacking = 2 "Pulsa [Y] para hacer mantenimiento"
    //  MessagePacking = 3 "Espera x segundos"
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

        if (GameManager.NeedsMaintenancePacking && GameManager.changeMessageMaintenancePacking)
        {
            MessagePacking.text = "Presiona [Y] para hacer mantenimiento";
            GameManager.MessagePacking = 2;
            GameManager.changeMessageMaintenancePacking = false;
        }

        //Maintenance time Dryer
        if (GameManager.CountDownMaintenancePacking && GameManager.MaintenanceTimePacking > 1)
        {
        GameManager.MaintenanceTimePacking -= Time.deltaTime;
        MessagePacking.text = "Espera " + GameManager.MaintenanceTimePacking.ToString("F0") + " segundos";
        GameManager.MessagePacking = 3;
        }

        if (GameManager.MaintenanceTimePacking <= 1 && !GameManager.ReadyMaintenancePacking)
        {
            MessagePacking.text = "Presiona [Y] para configurar";
            GameManager.ReadyMaintenancePacking = true;
            GameManager.MessagePacking = 1;
            GameManager.MaintenancePacking = false;
            GameManager.CountDownMaintenanceTimePacking = GameManager.timeBetweenMaintenancePacking;
            GameManager.NeedsMaintenancePacking = false;
            GameManager.CountDownMaintenancePacking = false;
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

    public void MaintenancePackingMachine(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && GameManager.MessagePacking == 2 && activeState)
        {
            GameManager.MaintenanceCostPacking = Random.Range(80, 120);
            GameManager.MaintenanceTimePacking = Random.Range(15, 20);
            textMaintenancePackingMenu.text = "Es momento de realizarle el mantenimiento preventivo programado a los sellos de la máquina empaquetadora, esto tiene un costo de $" + GameManager.MaintenanceCostPacking.ToString("F0") + " y se demora un tiempo de " + GameManager.MaintenanceTimePacking.ToString("F0") + " segundos, así que eres tu como Ingeniero Industrial el que tiene que decidir si realizarle el mantenimiento o no. Recuerda que todas tus decisiones afectarán a la calidad del producto terminado.";
            MaintenancePackingMenu.SetActive(true);
            GameManager.ReadyMaintenancePacking = false;
            GameManager.MaintenancePackingMenu = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
    }
}
