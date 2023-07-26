using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaintenancePackingMenu : MonoBehaviour
{
    [SerializeField] private GameObject MaintenancePacking;
    [SerializeField] private TMP_Text textPackingMachine;
    public Money money;
    
    
    public void Yes()
    {
        MaintenancePacking.SetActive(false);
        GameManager.MaintenancePacking = true;
        GameManager.MaintenancePackingMenu = false;
        GameManager.CountDownMaintenancePacking = true;
        GameManager.failureRateExpPacking -= 0.01f;
        GameManager.failureRatePoissonPacking -= 0.01f;
        GameManager.Money -= GameManager.MaintenanceCostPacking;
        money.ChangeMoneyValue();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void No()
    {
        MaintenancePacking.SetActive(false);
        GameManager.MaintenancePacking = false;
        GameManager.MaintenancePackingMenu = false;
        GameManager.failureRateExpPacking += 0.02f;
        GameManager.failureRatePoissonPacking += 0.02f;
        GameManager.MessagePacking = 1;
        textPackingMachine.text = "Presiona [Y] para configurar";
        GameManager.NeedsMaintenancePacking = false;
        GameManager.CountDownMaintenanceTimePacking = 50;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
