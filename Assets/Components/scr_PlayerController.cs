using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class s_PlayerController : MonoBehaviour
{
    // Parameters

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    [Range(0f, 15f)]
    private float acceleration;

    [SerializeField]
    [Range(0f, 15f)]
    private float friction;

    [SerializeField]
    private float mouseSens;

    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    private GameObject cameraFollowTarget;


    // Data

    private ia_DefaultPlayer inputActions;

    private Vector3 velocity;



    // Processes


    void Start()
    {
        inputActions = new ia_DefaultPlayer();

        if(inputActions != null)
        {
            inputActions.Movement.Enable();
        }

        velocity = Vector3.zero;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void FixedUpdate()
    {
        if(inputActions == null)
        {
            return;
        }

        Vector2 playerInput = inputActions.Movement.Movement.ReadValue<Vector2>();
        Vector3 targetVelocity = (gameObject.transform.rotation * 
            (new Vector3(playerInput.x, 0.0f, playerInput.y))).normalized * maxSpeed;

        float accelerationMethod = acceleration;
        if(targetVelocity == Vector3.zero)
        {
            accelerationMethod = friction;
        }

        velocity = Vector3.Lerp(velocity, targetVelocity, accelerationMethod * Time.fixedDeltaTime);

        gameObject.transform.position += velocity;
    }


    // Functions

    public void MouseAim(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();

        if (movement.x != 0)
        {
            gameObject.transform.Rotate(Vector3.up, movement.x * mouseSens * Time.deltaTime);
        }
        if (movement.y != 0)
        {
            cameraFollowTarget.transform.Rotate(Vector3.right, movement.y * mouseSens * -1.0f * Time.deltaTime);
        }
    }

    public void OnJumpPressed(InputAction.CallbackContext callbackContext)
    {

    }
}
