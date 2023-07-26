using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneManagerContext : MonoBehaviour
{
    public GameObject Context1;
    public GameObject Context2;
    public GameObject Norm1;
    public GameObject Norm2;
    public GameObject Norm3;
    public GameObject timeAndMoney;
    public GameObject panelControl;
    public GameObject eventSystem;

    void Start()
    {
        Context1.SetActive(true);
    }
    
    //Context 1
    public void ChangeContext2()
    {
        Context2.SetActive(true);
        Context1.SetActive(false);
    }

    //Context 2
    public void BackContext1()
    {
        Context2.SetActive(false);
        Context1.SetActive(true);
    }
    public void ChangeNorm1()
    {
        Norm1.SetActive(true);
        Context2.SetActive(false);
    }

    //Norm 1
    public void BackContext2()
    {
        Norm1.SetActive(false);
        Context2.SetActive(true);
    }
    public void ChangeNorm2()
    {
        Norm2.SetActive(true);
        Norm1.SetActive(false);
    }

    //Norm 2
    public void BackNorm1()
    {
        Norm1.SetActive(true);
        Norm2.SetActive(false);
    }
    public void ChangeNorm3()
    {
        Norm3.SetActive(true);
        Norm2.SetActive(false);
    }

    //Norm 3
    public void BackNorm2()
    {
        Norm2.SetActive(true);
        Norm3.SetActive(false);
    }
    public void ChangePlayGame()
    {
        timeAndMoney.SetActive(true);
        panelControl.SetActive(true);
        eventSystem.SetActive(false);
        Norm3.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
