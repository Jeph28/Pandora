using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    private int LimitFPS = 60;
    private bool isPaused = false;
    public GameObject PauseMenu;
    [SerializeField] private GameObject DryerMenu;
    [SerializeField] private GameObject PackingMenu;
    [SerializeField] private GameObject MaintenanceDryerMenu;
    [SerializeField] private GameObject MaintenancePackingMenu;
    [SerializeField] private GameObject ContextCheckPoint1;
    [SerializeField] private GameObject ContextCheckPoint2;
    [SerializeField] private GameObject RawMaterialMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject eventSystem;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject ceilings;
    [SerializeField] private GameObject corridors;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject packing;
    [SerializeField] private GameObject worker;
    [SerializeField] private GameObject failure;
    [SerializeField] private GameObject officeThings;
    [SerializeField] private GameObject productionLine;
    [SerializeField] private GameObject Capsule;
    [SerializeField] private GameObject Context1, Context2, Norm1, Norm2, Norm3;
    [SerializeField] private GameObject Gameover, Cp, Table, Qualification, Ishikawa;
    public Switch1 switch1;
    public Switch2 switch2;
    private static Settings instance;
    public static Settings Instance { get { return instance; } }
   
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
        Application.targetFrameRate = LimitFPS;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Pause(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && !Context1.activeSelf && !Context2.activeSelf && !Norm1.activeSelf && !Norm2.activeSelf && !Norm3.activeSelf && !Gameover.activeSelf && !Cp.activeSelf && !Table.activeSelf && !Qualification.activeSelf && !Ishikawa.activeSelf)
        {
            UpdateGameState();
        }
    }

    private void UpdateGameState()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            if (GameManager.DryerMenu || GameManager.PackingMenu || GameManager.MaintenanceDryerMenu || GameManager.MaintenancePackingMenu || GameManager.ContextCheckPoint1 || GameManager.ContextCheckPoint2 || GameManager.RawMaterialMenu)
            {
                DryerMenu.SetActive(false);
                GameManager.DryerMenu = false;
                
                PackingMenu.SetActive(false);
                GameManager.PackingMenu = false; 

                MaintenanceDryerMenu.SetActive(false);
                GameManager.MaintenanceDryerMenu = false;

                MaintenancePackingMenu.SetActive(false);
                GameManager.MaintenancePackingMenu = false;

                ContextCheckPoint1.SetActive(false);
                GameManager.ContextCheckPoint1 = false;

                ContextCheckPoint2.SetActive(false);
                GameManager.ContextCheckPoint2 = false;

                RawMaterialMenu.SetActive(false);
                GameManager.RawMaterialMenu = false;
            }
            
            PauseMenu.SetActive(true); 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else if (!GameManager.DryerMenu && !GameManager.PackingMenu && !GameManager.MaintenanceDryerMenu && !GameManager.MaintenancePackingMenu && !GameManager.ContextCheckPoint1 && !GameManager.ContextCheckPoint2 && !GameManager.RawMaterialMenu)
        {
            PauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        else
        {
            PauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 1f;
        }
    }

    public void Exit() 
    {
        Application.Quit();
    }

    public void Cancel()
    {
        UpdateGameState();
        PauseMenu.SetActive(false);
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }

    public void GameOver()
    {
        wall.SetActive(false);
        ceilings.SetActive(false);
        corridors.SetActive(false);
        buttons.SetActive(false);
        packing.SetActive(false);
        worker.SetActive(false);
        failure.SetActive(false);
        officeThings.SetActive(false);
        productionLine.SetActive(false);
        switch1.Status = false;
        switch2.Status = false;
        DryerMenu.SetActive(false);
        PackingMenu.SetActive(false);
        MaintenanceDryerMenu.SetActive(false);
        MaintenancePackingMenu.SetActive(false);
        ContextCheckPoint1.SetActive(false);
        ContextCheckPoint2.SetActive(false);
        RawMaterialMenu.SetActive(false);

        gameOver.SetActive(true);
        eventSystem.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
