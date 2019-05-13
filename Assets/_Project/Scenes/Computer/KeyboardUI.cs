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

    public void HoverAction()
    {
        FindPlayerAndCamera();
        DisableMovementAndMouse();
        ToggleUI();
    }

    private void Update()
    {
        if (visible)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                ToggleUI();
                EnableMovementAndMouse();
            }
        }
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
