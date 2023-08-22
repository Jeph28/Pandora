using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject tableResult;
    public GameObject gameOver;
    public GameObject cp;
    public GameObject qualificationObtained;
    public GameObject ishikawa;
    public GameObject ishikawa2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResultTable()
    {
        cp.SetActive(false);
        tableResult.SetActive(true);
    }

    public void Cp()
    {
        gameOver.SetActive(false);
        cp.SetActive(true);
    }

    public void BackCp()
    {
        cp.SetActive(true);
        tableResult.SetActive(false);
    }

    public void BackGameOver()
    {
        cp.SetActive(false);
        gameOver.SetActive(true);
    }
    
    public void BackResultTable()
    {
        qualificationObtained.SetActive(false);
        tableResult.SetActive(true);
    }

    public void QualificationObtained()
    {
        qualificationObtained.SetActive(true);
        tableResult.SetActive(false);
    }

    public void BackQualificationObtained()
    {
        qualificationObtained.SetActive(true);
        ishikawa.SetActive(false);
    }

    public void Ishikawa()
    {
        qualificationObtained.SetActive(false);
        ishikawa.SetActive(true);
    }

    public void BackIshikawa()
    {
        ishikawa2.SetActive(false);
        ishikawa.SetActive(true);
    }

    public void Ishikawa2()
    {
        ishikawa.SetActive(false);
        ishikawa2.SetActive(true);
    }
}
