using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Settings : MonoBehaviour
{
    private int LimitFPS = 60;
    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
        Application.targetFrameRate = LimitFPS;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Application.Quit();
        }
    }
}
