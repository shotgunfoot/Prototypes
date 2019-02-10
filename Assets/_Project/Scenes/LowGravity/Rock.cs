using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rock : WieldableObject
{
    public float ThrowForce = 5f;

    PlayerMovementController pc;
    Rigidbody rb;    

    public override void OnPickUpAction()
    {
        pc = GetComponentInParent<PlayerMovementController>();
        rb = GetComponent<Rigidbody>();
    }

    public override void ObjectAction()
    {
        //Drop the object from hand
        GetComponentInParent<Hand>().DropItem();
        //throw the rock forward
        rb.AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
        //apply force in opposite direction of the throw to the player
        pc.AddForce(-transform.forward * ThrowForce);
    }
}
