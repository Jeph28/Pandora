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
    float MaintenanceTimeDryer = 10f;
    float MaintenanceTimePacking = 20f;
    bool NeedsMaintenanceDryer = false;
    bool NeedsMaintenancePacking = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.CountDownActivateDryer && MaintenanceTimeDryer > 1)
        {
        MaintenanceTimeDryer -= Time.deltaTime;
        MessageState2Dryer.text = "Mantenimiento preventivo de la secadora en: " + MaintenanceTimeDryer.ToString("F0");
        }

         if (GameManager.CountDownActivatePacking && MaintenanceTimePacking > 1)
        {
        MaintenanceTimePacking -= Time.deltaTime;
        MessageState2Packing.text = "Mantenimiento preventivo de la empaquetadora en: " + MaintenanceTimePacking.ToString("F0");
        }

        if (MaintenanceTimeDryer <= 1 && !NeedsMaintenanceDryer)
        {
            MessageState2Dryer.text = "¡La Secadora requiere mantenimiento!";
            NeedsMaintenanceDryer = true;

            if (!GameManager.PanelControlState2)
            {
                StartCoroutine(SpamIcon());
            }
        }

        if (MaintenanceTimePacking <= 1 && !NeedsMaintenancePacking)
        {
            MessageState2Packing.text = "¡La Empaquetadora requiere mantenimiento!";
            NeedsMaintenancePacking = true;

            if (!GameManager.PanelControlState2)
            {
                StartCoroutine(SpamIcon());
            }
        }
    }

    public void TransitionState(InputAction.CallbackContext callbackContext)
    {
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
