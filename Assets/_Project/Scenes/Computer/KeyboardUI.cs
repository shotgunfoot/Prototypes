using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardUI : MonoBehaviour, IHoverAction
{

    public Canvas UI;
    public TMP_InputField InputField;
    public AudioClips clips;

    private AudioSource audioSource;
    private bool visible;
    private PlayerMovementController player;
    private MouseLook mouseLook;

    private void Update()
    {
        if (visible)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                Cancel();
            }
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Cancel()
    {
        ToggleUI();
        EnableMovementAndMouse();
        LockMouse();
        EnableHands();
    }

    public void HoverAction()
    {
        FindPlayerAndCamera();
        DisableMovementAndMouse();
        ReleaseMouse();
        DisableHands();
        ToggleUI();
        FocusOnInputField();
    }

    public void PlayRandomTypeSound()
    {
        audioSource.PlayOneShot(clips.sounds[UnityEngine.Random.Range(0, clips.sounds.Length)]);
    }

    private void FocusOnInputField()
    {
        InputField.ActivateInputField();
    }

    private void EnableHands()
    {
        foreach (Hand hand in player.GetComponentsInChildren<Hand>())
        {
            hand.EnableHand();
        }
    }


    private void LockMouse()
    {
        mouseLook.LockMouse();
    }

    private void DisableHands()
    {
        foreach (Hand hand in player.GetComponentsInChildren<Hand>())
        {
            hand.DisableHand();
        }
    }

    private void ReleaseMouse()
    {
        mouseLook.ReleaseMouse();
    }

    private void EnableMovementAndMouse()
    {
        mouseLook.EnableMouseMovement();
        player.EnableMovement();
    }

    private void DisableMovementAndMouse()
    {
        mouseLook.DisableMouseMovement();
        player.DisableMovement();
    }

    private void FindPlayerAndCamera()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovementController>();
        }
        if (mouseLook == null)
        {
            mouseLook = FindObjectOfType<MouseLook>();
        }
    }

    private void ToggleUI()
    {
        UI.enabled = !UI.enabled;
        visible = !visible;
    }
}
