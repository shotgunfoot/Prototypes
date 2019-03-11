using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Networking;

public class MouseLook : MonoBehaviour
{

    Vector2 mouseAbsolute;
    Vector2 smoothMouse;
    Vector2 storedSensitivity;

    public Vector2 ClampInDegrees = new Vector2(360, 180);
    public bool lockCursor;
    public Vector2 Sensitivity = new Vector2(2, 2);
    public Vector2 Smoothing = new Vector2(3, 3);
    public Vector2 TargetDirection;
    public Vector2 TargetCharacterDirection;

    // Assign this if there's a parent object controlling motion, such as a Character Controller.
    // Yaw rotation will affect this object instead of the camera if set.
    public GameObject CharacterBody;
    public MovementController MoveController;

    void Start()
    {
        // Set target direction to the camera's initial orientation.
        TargetDirection = transform.localRotation.eulerAngles;

        // Set target direction for the character body to its inital state.
        if (CharacterBody)
            TargetCharacterDirection = CharacterBody.transform.localRotation.eulerAngles;

        storedSensitivity = Sensitivity;

        MoveController = GetComponentInParent<MovementController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockCursor = !lockCursor;
        }

        if (lockCursor == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        // Allow the script to clamp based on a desired target value.
        Quaternion targetOrientation = Quaternion.Euler(TargetDirection);
        Quaternion targetCharacterOrientation = Quaternion.Euler(TargetCharacterDirection);

        // Get raw mouse input for a cleaner reading on more sensitive mice.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("MouseHorizontal"), Input.GetAxisRaw("MouseVertical"));        

        //if (Mathf.Abs(mouseAbsolute.y) > 90)
        //{
        //    Sensitivity.x = -Sensitivity.x;
        //}

        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(Sensitivity.x * Smoothing.x, Sensitivity.y * Smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseDelta.x, 1f / Smoothing.x);
        smoothMouse.y = Mathf.Lerp(smoothMouse.y, mouseDelta.y, 1f / Smoothing.y);
                

        // Find the absolute mouse movement value from point zero.
        mouseAbsolute += smoothMouse;

        // Clamp and apply the local x value first, so as not to be affected by world transforms.
        if (ClampInDegrees.x < 360)
            mouseAbsolute.x = Mathf.Clamp(mouseAbsolute.x, -ClampInDegrees.x * 0.5f, ClampInDegrees.x * 0.5f);

        // Then clamp and apply the global y value.
        if (ClampInDegrees.y < 360)
            mouseAbsolute.y = Mathf.Clamp(mouseAbsolute.y, -ClampInDegrees.y * 0.5f, ClampInDegrees.y * 0.5f);


        // If there's a character body that acts as a parent to the camera
        if (CharacterBody)
        {

            if (!MoveController.Grounded())
            {                
                //CharacterBody.transform.localRotation = Quaternion.Euler(mouseAbsolute.y, mouseAbsolute.x, 0);
                CharacterBody.transform.RotateAround(CharacterBody.transform.position, CharacterBody.transform.up, smoothMouse.x);
                CharacterBody.transform.RotateAround(CharacterBody.transform.position, CharacterBody.transform.right, smoothMouse.y);                
            }
            else
            {
                //transform.RotateAround(transform.position, transform.right, smoothMouse.y); //rotate camera up/down
                transform.localRotation = Quaternion.Euler(mouseAbsolute.y, 0, 0);
                CharacterBody.transform.RotateAround(CharacterBody.transform.position, CharacterBody.transform.up, smoothMouse.x); //rotate body left right. 

                //if the player is grounded then rotate the body left and right only.
                //Quaternion yRotation = Quaternion.identity;
                //yRotation = Quaternion.AngleAxis(mouseAbsolute.x, CharacterBody.transform.up);
                //CharacterBody.transform.localRotation = yRotation * targetCharacterOrientation;
            }

            //---------------------
            /**
             * This code only supports standing at a 90 degree angle, great when on the ground, terrible for when floating around
             * It works by rotating the character body left and right on a 90 degree angle.
             * */
            //Quaternion yRotation = Quaternion.identity;            

            //yRotation = Quaternion.AngleAxis(mouseAbsolute.x, Vector3.up);            

            //CharacterBody.transform.localRotation = yRotation * targetCharacterOrientation;

            //---------------------

        }
        else
        {
            Quaternion yRotation = Quaternion.AngleAxis(mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
            transform.localRotation *= yRotation;
        }

    }

    public void EnableMouseMovement()
    {
        Sensitivity = storedSensitivity;
    }

    public void DisableMouseMovement()
    {
        Sensitivity = Vector2.zero;
    }
}
