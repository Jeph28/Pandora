using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DryerMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject DryerMenu1;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackToGame()
    {
        DryerMenu1.SetActive(false);
        GameManager.DryerMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.changePrincipalText1CheckPoint1 = true;
        GameManager.changePrincipalText2CheckPoint1 = false;
        GameManager.changePrincipalText3CheckPoint1 = true;
        GameManager.timeWaitCheckPoint1 = 10;
    }
}
