using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveH : MonoBehaviour
{
    private PlayerInput playerInput;
    private Vector2 input;
    public Transform playerBody;
    private float xRotacion = 0f;
    public float Sensibility;
    public float mouseSensitivity;


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
        playerBody.Rotate(Vector3.up * input.x * Sensibility);
        xRotacion -= input.y;
        xRotacion = Mathf.Clamp(xRotacion,-80,80);
        transform.localRotation = Quaternion.Euler(xRotacion,0,0);
        if (input == Vector2.zero && !GameManager.DryerMenu && !GameManager.PackingMenu && !GameManager.MaintenanceDryerMenu && !GameManager.MaintenancePackingMenu && !GameManager.ContextCheckPoint)
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
