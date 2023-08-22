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
        GameManager.ScaleFailurePacking += 50000f;
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
        GameManager.ScaleFailurePacking -= 1000000f;
        GameManager.MessagePacking = 1;
        textPackingMachine.text = "Presiona [Y] para configurar";
        GameManager.NeedsMaintenancePacking = false;
        GameManager.CountDownMaintenanceTimePacking = 50;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
