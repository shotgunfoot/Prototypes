using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keyboard : MonoBehaviour, IHoverAction
{
    public AudioClips clips;
    public Transform playerLockPosition;
    public TextMeshProUGUI consoleText;
    public TMP_InputField consoleInput;

    private bool beingUsed = false;
    private AudioSource source;
    private PlayerMovementController player;
    private MouseLook mouseLook;    

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void HoverAction()
    {
        //lock player in position       
        player = FindObjectOfType<PlayerMovementController>();
        mouseLook = FindObjectOfType<MouseLook>();
        mouseLook.DisableMouseMovement();
        player.DisableMovement();
        Transform playerTransform = player.GetComponent<Transform>();
        playerTransform.position = playerLockPosition.position;
        mouseLook.transform.rotation = playerLockPosition.rotation;
        beingUsed = true;
        consoleInput.Select();
        consoleInput.ActivateInputField();
    }

    private void Update()
    {
        if (beingUsed)
        {
            if (Input.GetButton("Cancel"))
            {
                player.EnableMovement();
                mouseLook.EnableMouseMovement();
                beingUsed = false;
                consoleInput.DeactivateInputField();
            }
            
        }
    }

    public void AddToConsoleText(string stringToAdd)
    {
        //append string here
    }
}
