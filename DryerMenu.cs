using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DryerMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject DryerMenu1;
    public GameObject BottonCancel;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameManager.user_previousTemperature != GameManager.user_temperature || GameManager.user_previousTime != GameManager.user_time) && GameManager.DryerMenu && !GameManager.OffBottonCancel)
        {
            BottonCancel.SetActive(false);
            GameManager.OffBottonCancel = true;
            GameManager.ChangeValueDryer = true;

        }
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
        GameManager.OffBottonCancel = false;
        BottonCancel.SetActive(true);
    }

    public void Cancel()
    {
        DryerMenu1.SetActive(false);
        GameManager.DryerMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.OffBottonCancel = false;
        BottonCancel.SetActive(true);

    }
}
