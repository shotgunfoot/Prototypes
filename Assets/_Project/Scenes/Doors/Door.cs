using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    public AnimationClip Door_Open;
    public AnimationClip Door_Close;
    public bool isAnimPlaying;
    private bool isOpen;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isAnimPlaying = false;
    }

    public void UseDoor()
    {
        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        if (!isAnimPlaying)
        {
            isOpen = true;
            anim.Play(Door_Open.name);            
        }
    }

    private void CloseDoor()
    {
        if (!isAnimPlaying)
        {
            isOpen = false;
            anim.Play(Door_Close.name);            
        }        
    }
}
