using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLimb : MonoBehaviour
{
    [SerializeField]
    private Transform limbToFollow;
    [SerializeField]
    private float angleLimit = 20;
    
    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
