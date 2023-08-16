using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;



public class Switch1 : MonoBehaviour
{
    [SerializeField] private Transform distanceActivator;
    private Transform lookAtActivator;
    private Transform activator;
    private bool activeState;
    [SerializeField] private CanvasGroup target;
    [SerializeField] public GameObject SwitchM;
    [SerializeField] public GameObject Led;
    [SerializeField] private Material Green;
    [SerializeField] public Material Grey;
    [SerializeField] private GameObject pasta;
    [SerializeField] private GameObject initialposition;
    [SerializeField] private float lerpDuration;
    [SerializeField] public TMP_Text MessageSwitch;
    [SerializeField] private GameObject FailureDryer;
    [SerializeField] private GameObject Worker;
    public float timeSwitch = 0f;
    public PanelControl panelControl;
    public DryerMachine dryerMachine;
    public Money money;
    [SerializeField] private float distance;
    Quaternion originRotation, targetRotation;
    [SerializeField] private bool lookAtCamera;
    public bool Status = false;
    private Animator animator;

    float alpha;

    void Start()
    {
        originRotation = transform.rotation;
        alpha = activeState ? 1 : -1;
        if (activator == null) activator = Camera.main.transform;
        animator = Worker.GetComponent<Animator>();

    }

    bool IsTargetNear()
    {
        var distanceDelta = distanceActivator.position - activator.position;
        if (distanceDelta.sqrMagnitude < distance * distance)
        {
            if (lookAtActivator != null)
            {
                var lookAtActivatorDelta = lookAtActivator.position - activator.position;
                if (Vector3.Dot(activator.forward, lookAtActivatorDelta.normalized) > 0.8f)
                    return true;
            }
            var lookAtDelta = target.transform.position - activator.position;
            if (Vector3.Dot(activator.forward, lookAtDelta.normalized) > 0.8f)
                return true;
        }
        return false;
    }

    void Update()
    {
        if (!activeState)
        {
            if (IsTargetNear() && !GameManager.FailureDryer && !GameManager.MaintenanceDryer && GameManager.RawMaterial != 0)
            {
                alpha = 1;
                activeState = true; 
                GameManager.activeStateDryer = activeState;
            }
        }
        else
        {
            if (!IsTargetNear() || GameManager.MaintenanceDryer || GameManager.RawMaterial == 0 || GameManager.FailureDryer)
            {
                alpha = -1;
                activeState = false;
                GameManager.activeStateDryer = activeState;
            }
        }
        target.alpha = Mathf.Clamp01(target.alpha + alpha * Time.deltaTime);
        
        if (lookAtCamera)
        {
            if (activeState)
                targetRotation = Quaternion.LookRotation(activator.position - transform.position);
            else
                targetRotation = originRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
    }

    public void Switch(InputAction.CallbackContext callbackContext)
    {
         if (Settings.Instance.IsGamePaused())
            return;

        if (activeState && callbackContext.performed)
        {
            if (!Status && (Time.time - timeSwitch) > 3.0f && !GameManager.MaintenanceDryer && !GameManager.DryerMenu && !GameManager.MaintenanceDryerMenu && !GameManager.FailureDryer)
            {
                timeSwitch = Time.time;
                StartCoroutine(TransitionSwitchOn(lerpDuration));
                GameManager.DryerconveyorSpeed = 1f;
                Status = true;
                GameManager.DryerMachine = true;
                GameManager.CountDownActivateDryer = true;
                animator.SetTrigger("Sleep");

                

                if (GameManager.Batch == 0)
                {
                    FailureDryer.SetActive(true);
                    panelControl.MessageState2Packing.text = "Ahora ve a la Empaquetadora configurarla y enciéndela";                            
                    GameManager.InitialDryer = true;
                    DryerMachine.Instance.Humidity();
                    DryerMachine.Instance.Color();
                    DryerMachine.Instance.Craking();
                    DryerMachine.Instance.Microbiological();
                    DryerMachine.Instance.TemperaturePrice();
                    money.ChangeMoneyValue();

                    if (GameManager.InitialDryer && GameManager.InitialPacking)
                    {
                        GameManager.Batch ++;
                    }
                }
                else if(GameManager.ChangeValueDryer)
                {
                    dryerMachine.BatchSize(GameManager.previousUnpackPastaScore , GameManager.UnpackPastaScore);
                    GameManager.previousUnpackPastaScore = GameManager.UnpackPastaScore;
                    GameManager.Batch ++;
                    DryerMachine.Instance.Humidity();
                    DryerMachine.Instance.Color();
                    DryerMachine.Instance.Craking();
                    DryerMachine.Instance.Microbiological();
                    PackingMachine.Instance.StdDevWeight();
                    DryerMachine.Instance.TemperaturePrice();
                    money.ChangeMoneyValue();
                    GameManager.ChangeValueDryer = false;
                }

                MessageSwitch.text = "Presiona [X] para Apagar";
                StartCoroutine(DryerMachineOn());
            }

            if (Status && (Time.time - timeSwitch) > 3.0f)
            {
                timeSwitch = Time.time;
                StartCoroutine(TransitionSwitchOff(lerpDuration));
                MessageSwitch.text = "Presiona [X] para Encender";
                Status = false; 
                GameManager.DryerMachine = false;
            }
        }         
    }

    IEnumerator DryerMachineOn()
    {
        yield return new WaitForSeconds(2f);
        
        while (Status)
        {
            if (GameManager.UnpackOn < 10.0f && !GameManager.MaintenanceDryer)
            {
                //Case VeryRaw
                if (GameManager.pastaColor == 1)
                {
                    yield return new WaitForSeconds(GameManager.user_time / 100f);

                    if (Status)
                    {
                        GameObject unpackagedpasta = UnpackPastaPoolVeryRaw.Instance.RequestUnpackPastaVeryRaw();
                        unpackagedpasta.transform.position = initialposition.transform.position;
                    }

                    yield return new WaitForSeconds(2.0f);
                }
                //Case Raw
                if (GameManager.pastaColor == 2)
                {
                    yield return new WaitForSeconds(GameManager.user_time / 100f);

                    if (Status)
                    {
                        GameObject unpackagedpasta = UnpackPastaPoolRaw.Instance.RequestUnpackPastaRaw();
                        unpackagedpasta.transform.position = initialposition.transform.position;
                    }

                    yield return new WaitForSeconds(2.0f);
                }
                //Case Good
                if (GameManager.pastaColor == 3)
                {
                    yield return new WaitForSeconds(GameManager.user_time / 100f);

                    if (Status)
                    {
                        GameObject unpackagedpasta = UnpackPastaPoolGood.Instance.RequestUnpackPastaGood();
                        unpackagedpasta.transform.position = initialposition.transform.position;
                    }

                    yield return new WaitForSeconds(2.0f);
                }
                //Case SemiBernt
                if (GameManager.pastaColor == 4)
                {
                    yield return new WaitForSeconds(GameManager.user_time / 100f);

                    if (Status)
                    {
                        GameObject unpackagedpasta = UnpackPastaPoolSemiBernt.Instance.RequestUnpackPastaSemiBernt();
                        unpackagedpasta.transform.position = initialposition.transform.position;
                    }
                   
                    yield return new WaitForSeconds(2.0f);
                }
                //Case Bernt
                if (GameManager.pastaColor == 5)
                {
                    yield return new WaitForSeconds(GameManager.user_time / 100f);

                    if (true)
                    {
                        GameObject unpackagedpasta = UnpackPastaPoolBernt.Instance.RequestUnpackPastaBernt();
                        unpackagedpasta.transform.position = initialposition.transform.position;
                    }
                    
                    yield return new WaitForSeconds(2.0f);
                }
                yield return null;
            }
            yield return null;
        }
        yield return null; 
    }

    IEnumerator TransitionSwitchOn(float lerpDuration)
    {
        float timeElapsed = 0f;
        if (!Status)
        {
            while (timeElapsed < lerpDuration)
            {
                SwitchM.transform.rotation = Quaternion.Lerp(SwitchM.transform.rotation, Quaternion.Euler(-25f, 0f, 0f), timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                Led.gameObject.GetComponent<Renderer>().material = Green;
                yield return null;
            }
            SwitchM.transform.rotation = Quaternion.Euler(-25f, 0f, 0f);
            yield return null;
        }

    }
        
    IEnumerator TransitionSwitchOff(float lerpDuration)
    {
        float timeElapsed = 0f;
        if (Status)
        {
            while (timeElapsed < lerpDuration)
            {
                SwitchM.transform.rotation = Quaternion.Lerp(SwitchM.transform.rotation, Quaternion.Euler(35f, 0f, 0f), timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                Led.gameObject.GetComponent<Renderer>().material = Grey;
                yield return null;
            }
            SwitchM.transform.rotation = Quaternion.Euler(35f, 0f, 0f);
            yield return null;
        }
    }
}
