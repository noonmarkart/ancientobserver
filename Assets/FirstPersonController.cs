using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{

    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 5.0f;
    public float jumpSpeed = 20.0f;

    float verticalRotation = 0;
    public float upDownRange = 60.0f;

    float verticalVelocity = 0;
    public bool frozen = false;

    CharacterController characterController;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen)
        {
            // Rotation

            float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
            transform.Rotate(0, rotLeftRight, 0);


            verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


            // Movement

            float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
            float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

            verticalVelocity += Physics.gravity.y * Time.deltaTime;

            if (characterController.isGrounded && Input.GetButton("Jump"))
            {
                verticalVelocity = jumpSpeed;
            }

            Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

            speed = transform.rotation * speed;

            if (characterController.enabled)
                characterController.Move(speed * Time.deltaTime);
        }

            //if escape in the app = quit
            if (Input.GetKey(KeyCode.Escape))
            {

#if UNITY_EDITOR
                //UnityEditor.EditorApplication.isPlaying = false;
                //Cursor.lockState = CursorLockMode.None;
#else
                    Application.Quit();
#endif
            }
        
    }

    //this functions can be called by the dialogue manager and block the controls
    public void Freeze()
    {
        frozen = true;

        //unfreeze the mouse for fps control
        Cursor.lockState = CursorLockMode.None;

    }

    public void UnFreeze()
    {
        frozen = false;
    }

}
