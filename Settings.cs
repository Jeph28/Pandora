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
    // Start is called before the first frame update
    [SerializeField] private GameObject DryerMenu;
    [SerializeField] private GameObject PackingMenu;
    [SerializeField] private GameObject MaintenanceDryerMenu;
    [SerializeField] private GameObject MaintenancePackingMenu;
    [SerializeField] private GameObject ContextCheckPoint1;
    [SerializeField] private GameObject ContextCheckPoint2;
    [SerializeField] private GameObject RawMaterialMenu;
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

        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    public void Pause(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
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
}
