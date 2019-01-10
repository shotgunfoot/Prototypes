using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyFollowCameraRot : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow;
    [SerializeField]
    private float angleBeforeTurning = 20;

    private void FixedUpdate()
    {
        float angle = Vector3.Angle(transform.position, targetToFollow.position);

        Debug.Log(angle);
    }

}

