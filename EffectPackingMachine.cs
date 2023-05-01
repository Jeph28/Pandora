using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectPackingMachine : MonoBehaviour
{
    [SerializeField] private GameObject Fire;
    [SerializeField] private GameObject Shower;
    [SerializeField] private GameObject Explosion;
    
    // Start is called before the first frame update
    void Start()
    {
        // Probabilidad de falla de un 30%

        int prop = Random.Range(1, 11);
        Debug.Log(prop);
        if (prop <= 3) 
        {
            StartCoroutine("Failure");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Failure()
    {
        Explosion.SetActive(true);
        yield return new WaitForSeconds(.25f);
        Fire.SetActive(true);
        yield return new WaitForSeconds(15f);
        Shower.SetActive(true);
        yield return new WaitForSeconds(10.25f);
        Fire.SetActive(false);
        Shower.SetActive(false);
        Explosion.SetActive(false);
        yield return null;
        GameManager.Money -= 4000f;
    }
    
}
