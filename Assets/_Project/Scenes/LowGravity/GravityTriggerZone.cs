using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityTriggerZone : MonoBehaviour
{
    
    [Header("The circle represents where the gravity is 'pointing'.")]
    public Vector3 Gravity;
    [Header("Align the player reference with gravity. Feet being the small part.")]
    public Vector3 PlayerRotation;
    public Transform PlayerReference;    

    private void OnValidate()
    {
        PlayerReference.rotation = Quaternion.Euler(PlayerRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<MovementController>().SetGravityAndRotation(Gravity, PlayerRotation);
            
        }
    }

    //draw the direction of the gravity vector.
    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Gravity.normalized * 2);
        Gizmos.DrawSphere(transform.position + Gravity.normalized * 2, .2f);
    }
}
