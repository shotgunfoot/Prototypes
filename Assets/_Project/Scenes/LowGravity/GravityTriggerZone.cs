using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityTriggerZone : MonoBehaviour
{
    public Vector3 Gravity;

    public bool NormalGravity;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovementController>().SetGravity(Gravity);
        }
        else
        {
            other.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovementController>().SetGravity(new Vector3(0, 20, 0));
        }
        else
        {
            other.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
