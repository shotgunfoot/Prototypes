using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour {

    private Animator anim;
    private bool toggle;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (toggle)
            {
                anim.Play("Open");
                toggle = !toggle;
            }
            else
            {
                anim.Play("Close");
                toggle = !toggle;
            }
        }
	}
}
