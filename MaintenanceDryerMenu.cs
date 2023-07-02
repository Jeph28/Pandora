using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaintenanceDryerMenu : MonoBehaviour
{
    [SerializeField] private GameObject MaintenanceDryer;
    [SerializeField] private TMP_Text textDryerMachine;
    

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
        MaintenanceDryer.SetActive(false);
        GameManager.MaintenanceDryer = true;
        GameManager.MaintenanceDryerMenu = false;
        GameManager.CountDownMaintenanceDryer = true;
        GameManager.Money -= GameManager.MaintenanceCostDryer;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void No()
    {
        MaintenanceDryer.SetActive(false);
        GameManager.MaintenanceDryer = false;
        GameManager.MaintenanceDryerMenu = false;
        GameManager.MessageDryer = 1;
        textDryerMachine.text = "Presiona [Y] para configurar";
        GameManager.NeedsMaintenanceDryer = false;
        GameManager.CountDownMaintenanceTimeDryer = 150;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
