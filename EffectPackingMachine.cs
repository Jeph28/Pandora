using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectPackingMachine : MonoBehaviour
{
    [SerializeField] private GameObject Fire;
    [SerializeField] private GameObject Shower;
    [SerializeField] private GameObject Explosion;
    [SerializeField] public TMP_Text MessageState2Packing;
    [SerializeField] private TMP_Text textPackingMachine;
    [SerializeField] private GameObject PackingMenu;
    [SerializeField] private GameObject PackingMaintenanceMenu;
    public Switch2 switch2;
    public PackingMachine packingMachine;
    public Money money;
    [SerializeField] private float failureRateExp; // Average failure rate in failures per unit of time
    [SerializeField] private float failureRatePoisson;
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

        if (timeSinceLastFailure/60 >= timeBetweenFailure && !GameManager.FailurePacking)
        {
            timeBetweenFailure = GenerateTimeBetweenFailure();
            GameManager.FailurePacking = true;
                if (switch2.Status)
                {
                    switch2.timeSwitch = Time.time;
                    switch2.MessageSwitch.text = "Presiona [X] para Encender";
                    GameManager.PackingMachine = false;
                    StartCoroutine(TransitionSwitchOff(3f));
                    switch2.Status = false;
                }

            PackingMenu.SetActive(false);
            GameManager.PackingMenu = false;
            PackingMaintenanceMenu.SetActive(false);
            GameManager.MaintenancePackingMenu = false;
            StartCoroutine("Failure");
            
        }
    }

    IEnumerator Failure()
    {
        GameManager.NeedsMaintenancePacking = false;
        GameManager.CountDownActivatePacking = false;
        MessageState2Packing.text = "La Máquina Empaquetadora esta presentando fallas";
        yield return new WaitForSeconds(10.0f);
        Explosion.SetActive(true);
        yield return new WaitForSeconds(.25f);
        MessageState2Packing.text = "La Máquina Empaquetadora acaba de explotar";
        Fire.SetActive(true);
        yield return new WaitForSeconds(15f);
        Shower.SetActive(true);
        MessageState2Packing.text = "Se han encendido los aspersores";
        yield return new WaitForSeconds(5.0f);
        MessageState2Packing.text = "Ya se apago el incendio el costo fue de 4000$, vuelve a configurar la máquina";
        yield return new WaitForSeconds(7.25f);
        Fire.SetActive(false);
        Shower.SetActive(false);
        Explosion.SetActive(false);
        GameManager.Money -= 4000f;
        money.ChangeMoneyValue();
        GameManager.CountDownActivatePacking = true;
        GameManager.FailurePacking = false;
        timeSinceLastFailure = 0f;
        GameManager.ScaleFailurePacking = 0.10f;
        Restart();
        GameManager.failureEffectPackingRestart = true;
        packingMachine.MethodFailureEffectPacking();
        yield return null;
    }

    private void Restart()
    {
        GameManager.MaintenancePacking = false;
        GameManager.MaintenancePackingMenu = false;
        GameManager.MessagePacking = 1;
        textPackingMachine.text = "Presiona [Y] para configurar";
        GameManager.NeedsMaintenancePacking = false;
        GameManager.CountDownMaintenanceTimePacking = 240;
    }

    private float GenerateTimeBetweenFailure()
    {
        return (1 / Mathf.Clamp(GameManager.ScaleFailureDryer, 0.04f, 0.2f)) * Mathf.Pow(-Mathf.Log(1f - Random.Range(0.1f, 0.9f)), 1f / 5f);
    }

    //  private bool GenerateFailureProbability()
    // {
    //     //probability that K = 1
    //     // float probabilityOfFailure = GameManager.failureRateExpPacking * (timeBetweenFailure) * Mathf.Exp( - GameManager.failureRateExpPacking * (timeBetweenFailure));

    //     //probability that K >= 1
    //     float probabilityOfFailure = 1f - Mathf.Exp( - GameManager.failureRatePoissonPacking * (timeBetweenFailure));
    //     return probabilityOfFailure > Random.Range(0f, 1f);
    // }

    IEnumerator TransitionSwitchOff(float lerpDuration)
    {
        float timeElapsed = 0f;
        if (switch2.Status)
        {
            while (timeElapsed < lerpDuration)
            {
                switch2.SwitchM.transform.rotation = Quaternion.Lerp(switch2.SwitchM.transform.rotation, Quaternion.Euler(35f, 0f, 0f), timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                switch2.Led.gameObject.GetComponent<Renderer>().material = switch2.Grey;
                yield return null;
            }
            switch2.SwitchM.transform.rotation = Quaternion.Euler(35f, 0f, 0f);
            yield return null;
        }
    }

}
