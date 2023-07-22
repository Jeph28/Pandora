using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastaHandDelete : MonoBehaviour
{
    [SerializeField] private GameObject Worker;
    [SerializeField] private GameObject Pasta;
    [SerializeField] private GameObject Pasta1;
    //private float PositionTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Worker.transform.position.y < -0.38f)
        {
            StartCoroutine("ActivatePasta");
        }
        
    }

    IEnumerator ActivatePasta()
    {
        yield return new WaitForSeconds(0.25f);
        Pasta.SetActive(true);
        Pasta1.SetActive(false);
        yield return new WaitForSeconds(5.25f);
        Pasta.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Worker.transform.position = new Vector3(0.23f, 0, 17.21f);
        Worker.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
