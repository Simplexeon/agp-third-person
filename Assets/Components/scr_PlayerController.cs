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


    [SerializeField]
    private scr_RecordedGameObject recordedObject;


    // Data

    private ia_DefaultPlayer inputActions;

    private Vector3 velocity;

    private bool recording;
    private bool playback;

    [SerializeField]
    private GameObject playbackObject;

    [SerializeField]
    private scr_Recorder recorder;

    [SerializeField]
    private scr_Recordable recordable;



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

        playback = false;
        recording = false;
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

        if(recording && velocity.sqrMagnitude > 0)
        {
            UpdateRecordedObject();
        }
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


    public void RecordPressed(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed)
        {
            SetRecording(!recording);
        }
    }


    public void PlaybackPressed(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            SetPlayback(!playback);
        }
    }


    public void SetRecording(bool value)
    {
        recording = value;

        if(recording)
        {
            recorder.BeginRecording();
        }
        else
        {
            recorder.EndRecording();
        }
    }

    public void SetPlayback(bool value)
    {
        playback = value;

        if(playback)
        {
            // Create shadow object to record data.

            recorder.StartPlayback();
        }
    }

    public void UpdateRecordedObject()
    {
        recordedObject.posx = gameObject.transform.position.x;
        recordedObject.posy = gameObject.transform.position.y;
        recordedObject.posz = gameObject.transform.position.z;

        Vector3 euler = gameObject.transform.rotation.eulerAngles;
        recordedObject.rotx = euler.x;
        recordedObject.roty = euler.y;
        recordedObject.rotz = euler.z;

        recordedObject.scalex = gameObject.transform.localScale.x;
        recordedObject.scaley = gameObject.transform.localScale.y;
        recordedObject.scalez = gameObject.transform.localScale.z;

        recordable.SaveValue(recordedObject);
    }

    public void PlaybackUpdated(Vector3 pos, Vector3 rot, Vector3 scale)
    {
        playbackObject.transform.position = pos;
        playbackObject.transform.rotation = Quaternion.Euler(rot);
        playbackObject.transform.localScale = scale;
    }
}
