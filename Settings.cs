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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // public void Quit(InputAction.CallbackContext callbackContext)
    // {
    //     if (callbackContext.performed)
    //     {
    //         Application.Quit();
    //     }
    // }

    public void Pause()
    {
        UpdateGameState();
    }

    private void UpdateGameState()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
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

    public void Restart()
    {
        UpdateGameState();
        SceneManager.LoadScene("Game");
    }

    public void Exit() 
    {
        UpdateGameState();
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
