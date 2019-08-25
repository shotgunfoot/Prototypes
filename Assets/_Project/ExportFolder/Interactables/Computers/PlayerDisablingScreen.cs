using System;
using System.Collections;
using System.Collections.Generic;
using RoboRyanTron.Unite2017.Events;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDisablingScreen : MonoBehaviour, IHoverAction
{
    public GameEvent EnablePlayerMovementEvent;
    public GameEvent DisablePlayerMovementEvent;
    public GameEvent EnablePlayerLookEvent;
    public GameEvent DisablePlayerLookEvent;

    public Canvas screen;

    private bool visible;

    

    public void HoverAction()
    {
        EnableScreen();
    }

    private void DisablePlayerMovementAndLook()
    {
        DisablePlayerMovementEvent.Raise();
        DisablePlayerLookEvent.Raise();
    }

    private void EnablePlayerMovementAndLook()
    {
        EnablePlayerMovementEvent.Raise();
        EnablePlayerLookEvent.Raise();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (visible)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                DisableScreen();
            }
        }
    }

    public void DisableScreen()
    {
        visible = false;
        screen.gameObject.SetActive(false);
        EnablePlayerMovementAndLook();
    }

    public void EnableScreen()
    {
        screen.gameObject.SetActive(true);
        visible = true;
        DisablePlayerMovementAndLook();
    }


}
