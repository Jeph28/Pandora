using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveH : MonoBehaviour
{
    private PlayerInput playerInput;
    private Vector2 input;
    public Transform playerBody;
    private float xRotacion = 22.096f;
    public float SensibilityX;
    public float SensibilityY;
    public float mouseSensitivity;
    public SceneManagerContext sceneManagerContext;


    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Move Head
        input = playerInput.actions["MoveHead"].ReadValue<Vector2>();
        playerBody.Rotate(Vector3.up * input.x * SensibilityX);
        xRotacion -= input.y * SensibilityY;
        xRotacion = Mathf.Clamp(xRotacion,-80,80);
        transform.localRotation = Quaternion.Euler(xRotacion,0,0);
        if (input == Vector2.zero && !GameManager.DryerMenu && !GameManager.PackingMenu && !GameManager.MaintenanceDryerMenu && !GameManager.MaintenancePackingMenu && !GameManager.ContextCheckPoint1 && !GameManager.ContextCheckPoint2 && !GameManager.RawMaterialMenu && !sceneManagerContext.Context1.activeSelf && !sceneManagerContext.Context2.activeSelf && !sceneManagerContext.Norm1.activeSelf && !sceneManagerContext.Norm2.activeSelf && !sceneManagerContext.Norm3.activeSelf)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotacion -= mouseY;
            xRotacion = Mathf.Clamp(xRotacion, -90, 90f);
            transform.localRotation = Quaternion.Euler(xRotacion, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
