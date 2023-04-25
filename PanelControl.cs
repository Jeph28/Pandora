using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PanelControl : MonoBehaviour
{
    [SerializeField] private GameObject State2;
    [SerializeField] private GameObject State1;
    [SerializeField] public TMP_Text MessageState2;
    float MaintenanceTimeDryer = 10;
    // float MaintenanceTimePacking = 60;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (GameManager.DryerMachine && MaintenanceTimeDryer > 1)
       {
        MaintenanceTimeDryer -= Time.deltaTime;
        MessageState2.text = "\n" +"Mantenimiento preventivo de la secadora en: " + MaintenanceTimeDryer.ToString("F0");

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
}
