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
    public DryerMachine dryerMachine;
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
    }

    IEnumerator Failure()
    {
        GameManager.NeedsMaintenanceDryer = false;
        GameManager.CountDownActivateDryer = false;
        MessageState2Dryer.text = "\n" + "\n" + "La Máquina Secadora esta presentando fallas";
        yield return new WaitForSeconds(10.0f);
        Explosion.SetActive(true);
        yield return new WaitForSeconds(.25f);
        MessageState2Dryer.text = "\n" + "\n" + "La Máquina Secadora acaba de explotar";
        Fire.SetActive(true);
        yield return new WaitForSeconds(15f);
        Shower.SetActive(true);
        MessageState2Dryer.text = "\n" + "\n" + "Se han encendido los aspersores";
        yield return new WaitForSeconds(5.0f);
        MessageState2Dryer.text = "\n" + "Ya se apagó el incendio, el costo fue de 4000$, vuelve a configurar la máquina";
        yield return new WaitForSeconds(7.25f);
        Fire.SetActive(false);
        Shower.SetActive(false);
        Explosion.SetActive(false);
        GameManager.Money -= 4000f;
        money.ChangeMoneyValue();
        GameManager.CountDownActivateDryer = true;
        GameManager.FailureDryer = false;
        timeSinceLastFailure = 0f;
        GameManager.ScaleFailureDryer = 0.12f;
        GameManager.failureEffectDryerRestart = true;
        Restart();
        dryerMachine.MethodFailureEffectDryer();
        yield return null;
    }

    private void Restart()
    {
        GameManager.MaintenanceDryer = false;
        GameManager.MaintenanceDryerMenu = false;
        GameManager.MessageDryer = 1;
        textDryerMachine.text = "Presiona [Y] para configurar";
        GameManager.NeedsMaintenanceDryer = false;
        GameManager.CountDownMaintenanceTimeDryer = 180;
    }

    private float GenerateTimeBetweenFailure()
    {
        //Exponential distribution
        // return -Mathf.Log(1f - Random.Range(0.3f, 0.5f)) / GameManager.failureRateExpDryer;

        //Weibull distribution
        return (1 / Mathf.Clamp(GameManager.ScaleFailureDryer, 0.04f, 0.2f)) * Mathf.Pow(-Mathf.Log(1f - Random.Range(0.1f, 0.9f)),1f / 5f);
    }

    // private float GenerateFailureProbability()
    // {
    // //     probability that K = 1 Poisson
    //     float probabilityOfFailure = GameManager.failureRatePoissonDryer * (timeBetweenFailure) * Mathf.Exp( - GameManager.failureRatePoissonDryer * (timeBetweenFailure));
        
    // //     probability that K >= 1 Poisson
    //     float probabilityOfFailure = 1f - Mathf.Exp(- GameManager.failureRatePoissonDryer * (timeBetweenFailure));
    //     return probabilityOfFailure > Random.Range(0f, 1f);
    // }
    
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
