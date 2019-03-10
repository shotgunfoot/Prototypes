using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotate : MonoBehaviour
{
    public Vector3 rotation;
    public float rotSpeed;

    private void FixedUpdate()
    {
        transform.Rotate((rotation * rotSpeed) * Time.deltaTime);
    }
}
