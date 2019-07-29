using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    private Animator anim;
    private bool open = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenOrClose()
    {
        open = !open;
        anim.SetBool("Open", open);
    }

}
