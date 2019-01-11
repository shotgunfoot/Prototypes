using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollowCameraRot : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow;

    [SerializeField]
    private Vector3 offset;

    public bool ignoreX, ignoreY, ignoreZ, ignoreOffset;

    private void FixedUpdate()
    {        

        Vector3 target = targetToFollow.position;

        if (ignoreX)
        {
            target.x = transform.position.x;
        }
        if (ignoreY)
        {
            target.y = transform.position.y;
        }
        if (ignoreZ)
        {
            target.z = transform.position.z;
        }

        transform.LookAt(target);
        if (!ignoreOffset)
        {
            transform.Rotate(offset);
        }        
    }
}
