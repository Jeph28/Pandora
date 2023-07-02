using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectDryerMachine : MonoBehaviour
{
    [SerializeField] private GameObject Fire;
    [SerializeField] private GameObject Shower;
    [SerializeField] private GameObject Explosion;
    [SerializeField] public TMP_Text MessageState2Dryer;
    [SerializeField] private TMP_Text textDryerMachine;
    bool RestartDryer = false;
    [SerializeField] private GameObject DryerMenu;
    [SerializeField] private GameObject DryerMaintenanceMenu;

    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        // Probabilidad de falla de un 30%
        if (GameManager.FailureDryer && !RestartDryer)
        {
            RestartDryer = true;
            int prop = Random.Range(1, 11);
            if (prop <= 5) 
            {
                DryerMenu.SetActive(false);
                GameManager.DryerMenu = false;
                DryerMaintenanceMenu.SetActive(false);
                GameManager.MaintenanceDryerMenu = false;
                StartCoroutine("Failure");
            }
        }
    }

    IEnumerator Failure()
    {
        MessageState2Dryer.text = "\n" + "\n" + "La Maquina Secadora esta presentando fallas";
        GameManager.FailureInProgressDryer = true;
        yield return new WaitForSeconds(10.0f);
        Explosion.SetActive(true);
        yield return new WaitForSeconds(.25f);
        MessageState2Dryer.text = "\n" + "\n" + "La Maquina Secadora acaba de explotar";
        Fire.SetActive(true);
        yield return new WaitForSeconds(15f);
        Shower.SetActive(true);
        MessageState2Dryer.text = "\n" + "\n" + "Se han encendido los aspersores";
        yield return new WaitForSeconds(5.0f);
        MessageState2Dryer.text = "\n" + "\n" + "Ya se apago el incendio el costo fue de 4000$";
        yield return new WaitForSeconds(5.25f);
        Fire.SetActive(false);
        Shower.SetActive(false);
        Explosion.SetActive(false);
        GameManager.Money -= 4000f;
        GameManager.FailureInProgressDryer = false;
        GameManager.FailureDryer = false;
        Restart();
        yield return null;
    }

    private void Restart()
    {
        GameManager.MaintenanceDryer = false;
        GameManager.MaintenanceDryerMenu = false;
        GameManager.MessageDryer = 1;
        textDryerMachine.text = "Presiona [Y] para configurar";
        GameManager.NeedsMaintenanceDryer = false;
        GameManager.CountDownMaintenanceTimeDryer = 150;
    }
    
}
