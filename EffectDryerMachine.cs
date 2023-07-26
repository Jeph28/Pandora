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
    // bool RestartDryer = false;
    [SerializeField] private GameObject DryerMenu;
    [SerializeField] private GameObject DryerMaintenanceMenu;
    public Switch1 switch1;
    public Money money;
    private float timeSinceLastFailure = 0f; // Time elapsed since last failure
    private float timeBetweenFailure = 0f; // Time between failure
    

    
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenFailure = GenerateTimeBetweenFailure();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastFailure += Time.deltaTime;

        if (timeSinceLastFailure/60 >= timeBetweenFailure && !GameManager.FailureDryer)
        {
            timeBetweenFailure = GenerateTimeBetweenFailure();
            if (GenerateFailureProbability())
            {
                GameManager.FailureDryer = true;
                if (switch1.Status)
                {
                    switch1.timeSwitch = Time.time;
                    switch1.MessageSwitch.text = "Presiona [X] para Encender";
                    GameManager.DryerMachine = false;
                    StartCoroutine(TransitionSwitchOff(3f));
                    switch1.Status = false;
                }
            
                DryerMenu.SetActive(false);
                GameManager.DryerMenu = false;
                DryerMaintenanceMenu.SetActive(false);
                GameManager.MaintenanceDryerMenu = false;
                StartCoroutine("Failure");
            }
            else
            {
                timeSinceLastFailure = 0f;
            }
        }
    }

    IEnumerator Failure()
    {
        GameManager.NeedsMaintenanceDryer = false;
        GameManager.CountDownActivateDryer = false;
        GameManager.CountDownMaintenanceTimeDryer = 150;
        MessageState2Dryer.text = "\n" + "\n" + "La Maquina Secadora esta presentando fallas";
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
        money.ChangeMoneyValue();
        GameManager.CountDownActivateDryer = true;
        GameManager.FailureDryer = false;
        timeSinceLastFailure = 0f;
        GameManager.failureRateExpDryer = 0.1f;
        GameManager.failureRatePoissonDryer = 0.1f;
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

    private float GenerateTimeBetweenFailure()
    {
        return -Mathf.Log(1f - Random.Range(0.3f, 0.5f)) / GameManager.failureRateExpDryer;
    }

    private bool GenerateFailureProbability()
    {
        // probability that K = 1
        // float probabilityOfFailure = GameManager.failureRatePoissonDryer * (timeBetweenFailure) * Mathf.Exp( - GameManager.failureRatePoissonDryer * (timeBetweenFailure));
        
        //probability that K >= 1
        float probabilityOfFailure = 1f - Mathf.Exp(- GameManager.failureRatePoissonDryer * (timeBetweenFailure));
        return probabilityOfFailure > Random.Range(0f, 1f);
    }
    
    IEnumerator TransitionSwitchOff(float lerpDuration)
    {
        float timeElapsed = 0f;
        if (switch1.Status)
        {
            while (timeElapsed < lerpDuration)
            {
                switch1.SwitchM.transform.rotation = Quaternion.Lerp(switch1.SwitchM.transform.rotation, Quaternion.Euler(35f, 0f, 0f), timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                switch1.Led.gameObject.GetComponent<Renderer>().material = switch1.Grey;
                yield return null;
            }
            switch1.SwitchM.transform.rotation = Quaternion.Euler(35f, 0f, 0f);
            yield return null;
        }
    }
}
