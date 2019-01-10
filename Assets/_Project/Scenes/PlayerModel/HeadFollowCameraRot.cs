using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollowCameraRot : MonoBehaviour
{   
    [SerializeField]
    private Transform targetToFollow;

    [SerializeField]
    private Vector3 offset;

    private void FixedUpdate()
    {
        transform.LookAt(targetToFollow);
        transform.Rotate(offset);
    }
}
