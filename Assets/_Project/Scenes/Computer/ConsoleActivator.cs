using UnityEngine;

/*
    Class: ConsoleActivator
    Behaviour: This class controls visibility and use of the entire console, containing references to the player,
    mouselook and any other scripts required to enable/disable the use of the in game computer console.

 */
public class ConsoleActivator : MonoBehaviour, IHoverAction
{
    public GameObject UI;

    private ConsoleInput consoleInput;
    private PlayerMovementController player;
    private MouseLook mouseLook;
    private bool visible = false;

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

    public void HoverAction()
    {
        FindPlayerAndCamera();
        DisableMovementAndMouse();
        ReleaseMouse();
        DisableHands();
        EnableUI();
        if (consoleInput == null)
        {
            consoleInput = GetComponentInChildren<ConsoleInput>();
        }
        consoleInput.FocusOnInputField();
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


    public void Cancel()
    {
        DisableUI();
        EnableMovementAndMouse();
        LockMouse();
        EnableHands();
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

    private void DisableUI()
    {
        UI.SetActive(false);
        visible = false;
    }

    private void EnableUI()
    {
        UI.SetActive(true);
        visible = true;
    }
}