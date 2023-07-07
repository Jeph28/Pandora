using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DryerMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject DryerMenu1;
    public GameObject BottonCancel;
    [SerializeField] private TMP_Text Context;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameManager.user_previousTemperature != GameManager.user_temperature || GameManager.user_previousTime != GameManager.user_time) && GameManager.DryerMenu && !GameManager.OffBottonCancelDryer)
        {
            BottonCancel.SetActive(false);
            GameManager.OffBottonCancelDryer = true;
            GameManager.ChangeValueDryer = true;

        }

        Context.text = "Desliza para elegir el valor correspondiente a la temperatura y el tiempo de secado, recuerda que cada decisión tomada afectará directamente la calidad del producto final, así que recuerda tomar tus decisiones pensando como un Ingeniero Industrial. Esto tendra un costo de " + "<b>" + DryerMachine.Instance.temperaturePriceInquiry().ToString("F0") + "$</b>";
    }
    public void Accept()
    {
        DryerMenu1.SetActive(false);
        GameManager.DryerMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.changePrincipalText1CheckPoint1 = true;
        GameManager.changePrincipalText2CheckPoint1 = false;
        GameManager.changePrincipalText3CheckPoint1 = true;
        GameManager.timeWaitCheckPoint1 = 10;
        GameManager.user_previousTemperature = GameManager.user_temperature;
        GameManager.user_previousTime = GameManager.user_time;
        GameManager.OffBottonCancelDryer = false;
        BottonCancel.SetActive(true);
    }

    public void Cancel()
    {
        DryerMenu1.SetActive(false);
        GameManager.DryerMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.OffBottonCancelDryer = false;
        BottonCancel.SetActive(true);

    }
}
