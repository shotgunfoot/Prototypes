using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyIK : MonoBehaviour
{
    
    public Animator avatar;
    public Transform lookAtObject;
    public float lookAtWeight = 1f;

    private void OnAnimatorIK(int layerIndex)
    {
        if (avatar)
        {
            avatar.SetLookAtPosition(lookAtObject.position);                       
            avatar.SetLookAtWeight(lookAtWeight);            
        }
    }
}
