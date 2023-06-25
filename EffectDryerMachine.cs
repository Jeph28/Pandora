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
    
    // Start is called before the first frame update
    void Start()
    {
        // Probabilidad de falla de un 30%

        
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            int prop = Random.Range(1, 11);
            if (prop <= 3) 
            {
                StartCoroutine("Failure");

            }
            else
            {
                StartCoroutine("Failure");  
            }
        }
    }

    IEnumerator Failure()
    {
        GameManager.FailureDryer = true;
        Explosion.SetActive(true);
        yield return new WaitForSeconds(.25f);
        MessageState2Dryer.text = "La Maquina Secadora acaba de explotar";
        Fire.SetActive(true);
        yield return new WaitForSeconds(15f);
        Shower.SetActive(true);
        yield return new WaitForSeconds(10.25f);
        Fire.SetActive(false);
        Shower.SetActive(false);
        Explosion.SetActive(false);
        MessageState2Dryer.text = "Ya se apago el incendio el costo fue de 4000$";
        GameManager.FailureDryer = false;
        GameManager.Money -= 4000f;
        yield return null;
    }
    
}
