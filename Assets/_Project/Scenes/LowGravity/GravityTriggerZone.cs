using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityTriggerZone : MonoBehaviour
{
    public Vector3 Gravity;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<MovementController>().SetGravity(Gravity);
        }
    }
}
