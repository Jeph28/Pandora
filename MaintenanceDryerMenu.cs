using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaintenanceDryerMenu : MonoBehaviour
{
    [SerializeField] private GameObject MaintenanceDryer;
    [SerializeField] private TMP_Text textDryerMachine;
    public Money money;
    

    
    public void Yes()
    {
        MaintenanceDryer.SetActive(false);
        GameManager.MaintenanceDryer = true;
        GameManager.MaintenanceDryerMenu = false;
        GameManager.CountDownMaintenanceDryer = true;
        GameManager.failureRateExpDryer -= 0.01f;
        GameManager.failureRatePoissonDryer -= 0.01f;
        GameManager.Money -= GameManager.MaintenanceCostDryer;
        money.ChangeMoneyValue();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void No()
    {
        MaintenanceDryer.SetActive(false);
        GameManager.MaintenanceDryer = false;
        GameManager.MaintenanceDryerMenu = false;
        GameManager.failureRateExpDryer += 0.02f;
        GameManager.failureRatePoissonDryer += 0.02f;
        GameManager.MessageDryer = 1;
        textDryerMachine.text = "Presiona [Y] para configurar";
        GameManager.NeedsMaintenanceDryer = false;
        GameManager.CountDownMaintenanceTimeDryer = 150;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
