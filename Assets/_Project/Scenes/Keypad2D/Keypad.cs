using System;
using System.Collections;
using System.Collections.Generic;
using RoboRyanTron.Unite2017.Events;
using TMPro;
using UnityEngine;

public class Keypad : PlayerDisablingScreen, IHoverAction
{
    private string sequence = "";
    private AudioSource soundPlayer;
    public AudioClip[] sounds;
    [SerializeField] private string solution;
    public TextMeshProUGUI textObject;
    public GameEvent successEvent;


    private void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        soundPlayer.PlayOneShot(sounds[index]);
    }

    public void AddToString(string num)
    {
        PlaySound(0);
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
            PlaySound(1);
            Clear();
            DisableScreen();
        }
        else
        {
            Clear();
            PlaySound(2);
        }
    }

    public void Cancel()
    {
        DisableScreen();
    }
}
