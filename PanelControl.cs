using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PanelControl : MonoBehaviour
{
    [SerializeField] private GameObject State1;
    [SerializeField] private GameObject State2;
    [SerializeField] private GameObject State3;
    [SerializeField] public TMP_Text MessageState2Dryer;
    [SerializeField] public TMP_Text MessageState2Packing;
    [SerializeField] private TMP_Text textDryerMachine;
    [SerializeField] private TMP_Text textPackingMachine;
    [SerializeField] private CanvasGroup Target;
    private bool InitialState = true;
    bool FailureIcon = false;
    float alpha;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.activeStateDryer || GameManager.activeStatePacking)
        {
            alpha = -1;
        }
        else
        {
            alpha = 1;
        }

        Target.alpha = Mathf.Clamp01(Target.alpha + alpha * Time.deltaTime);
        
        //CountDown Dryer
        if (GameManager.CountDownActivateDryer && GameManager.CountDownMaintenanceTimeDryer > 1)
        {
        GameManager.CountDownMaintenanceTimeDryer -= Time.deltaTime;
        MessageState2Dryer.text = "\n" + "\n" + "Mantenimiento preventivo de la secadora en: " + GameManager.CountDownMaintenanceTimeDryer.ToString("F0");
        }

        //CountDown Packing
         if (GameManager.CountDownActivatePacking && GameManager.CountDownMaintenanceTimePacking > 1)
        {
        GameManager.CountDownMaintenanceTimePacking -= Time.deltaTime;
        MessageState2Packing.text = "Mantenimiento preventivo de la empaquetadora en: " + GameManager.CountDownMaintenanceTimePacking.ToString("F0");
        }

        //Initial state 
        if (InitialState)
        {
            StartCoroutine(SpamIcon());
            InitialState = false;
        }

        //Need Maintenance Dryer Machine
        if (GameManager.CountDownMaintenanceTimeDryer <= 1 && !GameManager.NeedsMaintenanceDryer)
        {
            MessageState2Dryer.text = "\n" + "\n" + "¡La Secadora requiere mantenimiento!";
            GameManager.NeedsMaintenanceDryer = true;
            GameManager.changeMessageMaintenanceDryer = true;
            GameManager.OpportunityMaintenanceDryer = Time.time;

            if (!GameManager.PanelControlState2)
            {
                StartCoroutine(SpamIcon());
            }
        }

        //Opportunity to do Maintenance Dryer Machine
        if (Time.time - GameManager.OpportunityMaintenanceDryer > GameManager.MaintenanceExpirationDryer && GameManager.NeedsMaintenanceDryer)
        {
            GameManager.MaintenanceDryer = false;
            GameManager.MessageDryer = 1;
            GameManager.NeedsMaintenanceDryer = false;
            textDryerMachine.text = "Presiona [Y] para configurar";
            GameManager.CountDownMaintenanceTimeDryer = GameManager.timeBetweenMaintenanceDryer;
            GameManager.MaintenanceCounterDryer ++;
        }

        if (GameManager.MessageDryer == 3)
        {
            MessageState2Dryer.text = "\n" + "\n" + "Se le esta realizando mantenimiento a la secadora";
        }

        //Need Maintenance Packing Machine
        if (GameManager.CountDownMaintenanceTimePacking <= 1 && !GameManager.NeedsMaintenancePacking)
        {
            MessageState2Packing.text = "¡La Empaquetadora requiere mantenimiento!";
            GameManager.NeedsMaintenancePacking = true;
            GameManager.changeMessageMaintenancePacking = true;
            GameManager.OpportunityMaintenancePacking = Time.time;

            if (!GameManager.PanelControlState2)
            {
                StartCoroutine(SpamIcon());
            }
        }

        //Opportunity to do Maintenance Packing Machine
        if (Time.time - GameManager.OpportunityMaintenancePacking > GameManager.MaintenanceExpirationPacking && GameManager.NeedsMaintenancePacking)
        {
            GameManager.MaintenancePacking = false;
            GameManager.MessagePacking = 1;
            GameManager.NeedsMaintenancePacking = false;
            textPackingMachine.text = "Presiona [Y] para configurar";
            GameManager.CountDownMaintenanceTimePacking = GameManager.timeBetweenMaintenancePacking;
        }

        if (GameManager.MessagePacking == 3)
        {
            MessageState2Packing.text = "Se le esta realizando mantenimiento a la empaquetadora";
        }

        if (GameManager.FailureDryer && !GameManager.PanelControlState2 && !FailureIcon)
        {
            StartCoroutine(SpamIcon());
            FailureIcon = true;
        }

        if (GameManager.FailurePacking && !GameManager.PanelControlState2 && !FailureIcon)
        {
            StartCoroutine(SpamIcon());
            FailureIcon = true;
        }
    }

    public void TransitionState(InputAction.CallbackContext callbackContext)
    {
         if (Settings.Instance.IsGamePaused())
            return;
            
        if (callbackContext.performed)
        {
            if (!GameManager.PanelControlState2)
            {
                State1.SetActive(false);
                State2.SetActive(true);
                State3.SetActive(false);
                GameManager.PanelControlState2 = true;
                GameManager.PanelControlState1 = false;
            }
            else
            {
                State2.SetActive(false);
                State1.SetActive(true);
                GameManager.PanelControlState2 = false;
                GameManager.PanelControlState1 = true;
            }
        }  
    }

    IEnumerator SpamIcon()
    {
        while (!GameManager.PanelControlState2)
        {
            State3.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            State3.SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
