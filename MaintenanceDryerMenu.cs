using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaintenanceDryerMenu : MonoBehaviour
{
    [SerializeField] private GameObject MaintenanceDryer;
    [SerializeField] private TMP_Text textDryerMachine;
    [SerializeField] private TMP_Text textMaintenanceDryerMenu;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.MaintenanceCostDryer = Random.Range(80, 120);
        GameManager.MaintenanceTimeDryer = Random.Range(15, 20);
        textMaintenanceDryerMenu.text = "Es momento de realizarle el mantenimiento preventivo programado al ventilador de la máquina secadora, esto tiene un costo de $" + GameManager.MaintenanceCostDryer.ToString("F0") + " y se demora un tiempo de " + GameManager.MaintenanceTimeDryer.ToString("F0") + " segundos, así que eres tu como Ingeniero Industrial el que tiene que decidir si realizarle el mantenimiento o no. Recuerda que todas tus decisiones afectarán a la calidad del producto terminado.";
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
        GameManager.CountDownMaintenanceTimeDryer = 15;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
