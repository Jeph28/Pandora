using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaintenancePackingMenu : MonoBehaviour
{
    [SerializeField] private GameObject MaintenancePacking;
    [SerializeField] private TMP_Text textPackingMachine;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Yes()
    {
        MaintenancePacking.SetActive(false);
        GameManager.MaintenancePacking = true;
        GameManager.MaintenancePackingMenu = false;
        GameManager.CountDownMaintenancePacking = true;
        GameManager.Money -= GameManager.MaintenanceCostPacking;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void No()
    {
        MaintenancePacking.SetActive(false);
        GameManager.MaintenancePacking = false;
        GameManager.MaintenancePackingMenu = false;
        GameManager.MessagePacking = 1;
        textPackingMachine.text = "Presiona [Y] para configurar";
        GameManager.NeedsMaintenancePacking = false;
        GameManager.CountDownMaintenanceTimePacking = 15;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
