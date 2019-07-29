using System;
using System.Collections;
using System.Collections.Generic;
using RoboRyanTron.Unite2017.Events;
using TMPro;
using UnityEngine;

public class Keypad : PlayerDisablingScreen, IHoverAction
{
    private string sequence = "";
    [SerializeField] private string solution;
    public TextMeshProUGUI textObject;

    public GameEvent successEvent;

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
            successEvent.Raise();
        }
        else
        {
            Clear();
        }
    }

    public void Cancel()
    {
        DisableScreen();
    }
}
