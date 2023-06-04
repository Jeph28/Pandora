using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 inputM;
    private Vector3 velocity;
    [SerializeField] public float speed;
    [SerializeField] public CharacterController controller;
    [SerializeField] private float timeJump = 0f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float gravity;
    public float jumpHeight = 3f;
    private PlayerInput playerInput;
    public AudioClip footStepSound;
    public float footStepDelay; 
    private float nextFootstep = 0;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Gravity and Jump
        if (isGrounded && velocity.y <0)
        {
            velocity.y = -3f;
        }

        //Move body
        inputM = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 motion = transform.right * inputM.x + transform.forward * inputM.y;
        if (!GameManager.DryerMenu && !GameManager.PackingMenu && !GameManager.MaintenanceDryerMenu && !GameManager.MaintenancePackingMenu && !GameManager.ContextCheckPoint && !GameManager.RawMaterialMenu)
        {
            controller.Move(motion * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }        

        if (inputM.x != 0 || inputM.y != 0)
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0) 
                {
                 GetComponent<AudioSource>().PlayOneShot(footStepSound, 0.7f);
                 nextFootstep += footStepDelay+0.5f;
                }
        }
    }

    public void jump(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed && (Time.time - timeJump) > 1.0f)
        {
            timeJump = Time.time;
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }
    }

}
