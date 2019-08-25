using System.Collections;
using System.Collections.Generic;
using RoboRyanTron.Unite2017.Events;
using UnityEngine;

public class Arms : MonoBehaviour
{
    [SerializeField]
    private PlayerInput pInput;
    private Animator anim;    
    private bool lookingAtWatch;
    private float looking;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (pInput.Watch)
        {
            lookingAtWatch = !lookingAtWatch;
            anim.SetBool("watch", lookingAtWatch);                        
        }
    }

}
