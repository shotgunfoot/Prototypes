using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour, IHoverAction
{
    private string sequence;
    private MouseLook mouseLook;
    private PlayerMovementController player;
    private bool visible;
    [SerializeField] private string solution;
    public TextMeshProUGUI textObject;

    public GameObject UI;

    public void AddToString(string num)
    {
        if (sequence.Length < 4)
        {
            sequence += num;
            textObject.text = sequence;
        }
    }

    public void Clear()
    {
        sequence = "";
        textObject.text = sequence;
    }

    public void SubmitCode()
    {
        if (sequence == solution)
        {
            Debug.Log("Correct Code Entered!");
        }
        else
        {
            Debug.Log("Wrong! Try again");
        }
    }

    public void Cancel()
    {
        DisableUI();
        EnableMovementAndMouse();
        LockMouse();
        EnableHands();
    }

    public void HoverAction()
    {
        sequence = "";
        FindPlayerAndCamera();
        DisableMovementAndMouse();
        ReleaseMouse();
        DisableHands();
        EnableUI();
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
}
