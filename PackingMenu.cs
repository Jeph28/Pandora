using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PackingMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject PackingMenu1;
    public GameObject BottonCancel;
    [SerializeField] private TMP_Text Context;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ( GameManager.user_previousSpeed != GameManager.user_speed && GameManager.PackingMenu && !GameManager.OffBottonCancelPacking)
        {
            BottonCancel.SetActive(false);
            GameManager.OffBottonCancelPacking = true;
            GameManager.ChangeValuePacking = true;

        }

        Context.text = "Desliza para elegir el valor correspondiente a la velocidad de empaquetado expresado en unidades por minuto, recuerda que cada decisión tomada afectará directamente la calidad del producto final, así que recuerda tomar tus decisiones pensando como un Ingeniero Industrial. Esto tendra un costo de " + "<b>" + PackingMachine.Instance.speedPriceInquiry().ToString("F0") + "$</b>";
    }
    public void Accept()
    {
        PackingMenu1.SetActive(false);
        GameManager.PackingMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.changePrincipalText1CheckPoint2 = true;
        GameManager.changePrincipalText2CheckPoint2 = false;
        GameManager.changePrincipalText3CheckPoint2 = true;
        GameManager.timeWaitCheckPoint2 = 10;
        GameManager.user_previousSpeed = GameManager.user_speed;
        GameManager.OffBottonCancelPacking = false;
        BottonCancel.SetActive(true);
    }

    public void Cancel()
    {
        PackingMenu1.SetActive(false);
        GameManager.PackingMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.OffBottonCancelPacking = false;
        BottonCancel.SetActive(true);
    }
    
}
