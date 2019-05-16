using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardUI : MonoBehaviour, IHoverAction
{

    public Canvas UI;

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

    public void Cancel()
    {
        ToggleUI();
        EnableMovementAndMouse();
        LockMouse();
        EnableHands();
    }

    private void LockMouse()
    {
        mouseLook.LockMouse();
    }

    public void HoverAction()
    {
        FindPlayerAndCamera();
        DisableMovementAndMouse();
        ReleaseMouse();
        DisableHands();
        ToggleUI();
    }

    private void EnableHands()
    {
        foreach (Hand hand in player.GetComponentsInChildren<Hand>())
        {
            hand.EnableHand();
        }
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
