using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class Switch2 : MonoBehaviour
{   
    [SerializeField] private Transform distanceActivator;
    [SerializeField] private GameObject Worker;
    [SerializeField] private CanvasGroup target;
    [SerializeField] private GameObject SwitchM;
    private Transform SwitchMOn;
    [SerializeField] private GameObject Led;
    [SerializeField] private Material Green;
    [SerializeField] private Material Grey;
    [SerializeField] private GameObject initialposition;
    private float timeSwitch = 0f;
    private Transform lookAtActivator;
    private Transform activator;
    private bool activeState = false;
    private Animator animator;
    bool Status = false;
    Quaternion originRotation, targetRotation;
    [SerializeField] public TMP_Text MessageSwitch;
    float alpha;
    private Quaternion start;
    private Quaternion target1;
    [SerializeField] private float lerpDuration;
    [SerializeField] private float distance;
    [SerializeField] private bool lookAtCamera;
    
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
        if (!activeState && !GameManager.FailurePacking && !GameManager.MaintenancePacking && GameManager.RawMaterial != 0)
        {
            if (IsTargetNear())
            {
                alpha = 1;
                activeState = true; 
                GameManager.activeStatePacking  = activeState;
            }
        }
        else
        {
            if (!IsTargetNear() || GameManager.MaintenancePacking || GameManager.RawMaterial == 0)
            {
                alpha = -1;
                activeState = false;
                GameManager.activeStatePacking  = activeState;
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

    public void Switch1(InputAction.CallbackContext callbackContext)
    {
         if (Settings.Instance.IsGamePaused())
            return;
              
        if (activeState && callbackContext.performed)
        {
            if (!Status && (Time.time - timeSwitch) > 3.0f && !GameManager.MaintenancePacking && !GameManager.PackingMenu && !GameManager.MaintenancePackingMenu)
            {
                timeSwitch = Time.time;
                StartCoroutine(TransitionSwitchOn(lerpDuration));
                Status = true;
                GameManager.PackingMachine = Status;
                GameManager.CountDownActivatePacking = true;
                PackingMachine.Instance.Weight();
                PackingMachine.Instance.EfficiencyMachine();
                if (GameManager.ChangeValuePacking || GameManager.BatchPacking == 0)
                {
                    PackingMachine.Instance.SpeedPrice();
                    GameManager.ChangeValuePacking = false;
                }
                GameManager.BatchPacking ++;
                MessageSwitch.text = "Presiona [X] para Apagar";
                StartCoroutine(PackingMachineOn());
            }
            if (Status && (Time.time - timeSwitch) > 3.0f)
            {
                timeSwitch = Time.time;
                StartCoroutine(TransitionSwitchOff(lerpDuration));
                MessageSwitch.text = "Presiona [X] para Encender";
                Status = false;
                GameManager.PackingMachine = Status;
            }
        }        
    }

    IEnumerator PackingMachineOn()
    {
        yield return new WaitForSeconds(2.5f);
        while (Status)
        {
            // Debug.Log(GameManager.hasCollidedPackingMachine);
            // Debug.Log(GameManager.RequestUnpack);
            if ((Status && GameManager.hasCollidedPackingMachine))
            {
                GameManager.RequestUnpack = true;
                yield return new WaitForSeconds(( 100f / GameManager.user_speed));

                if (Status && !GameManager.RequestUnpack)   
                {
                    GameObject packagedpasta = PackPastaPool.Instance.RequestPackPasta();
                    packagedpasta.transform.position = initialposition.transform.position;
                    GameManager.PastaScore ++;
                    animator.SetTrigger("statusPacking");
                    yield return new WaitForSeconds(1f);
                }
                // yield return new WaitForSeconds(1f); 
            }
            else
            {
               yield return new WaitForSeconds(1f);
            //    Debug.Log("No entro a la condicion"); 
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "unpackPasta")
        {
            GameManager.hasCollidedPackingMachine = true;
        }
    }
}