using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackingMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject PackingMenu1;
    public GameObject BottonCancel;
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
    }
    public void Accept()
    {
        PackingMenu1.SetActive(false);
        GameManager.PackingMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
