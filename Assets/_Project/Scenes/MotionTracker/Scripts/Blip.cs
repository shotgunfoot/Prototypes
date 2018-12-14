using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blip : MonoBehaviour
{

    public Rigidbody rigidbody;
    MeshRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude > 0)
        {
            renderer.enabled = true;
        }
        else
        {
            renderer.enabled = false;
        }
    }
}
