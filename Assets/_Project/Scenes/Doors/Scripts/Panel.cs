using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour, IHoverAction
{
    private bool open;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    public void HoverAction()
    {
        if (open)
        {
            anim.Play("Panel_Close");
            open = !open;
        }
        else
        {
            anim.Play("Panel_Open");
            open = !open;
        }
    }
    
}
