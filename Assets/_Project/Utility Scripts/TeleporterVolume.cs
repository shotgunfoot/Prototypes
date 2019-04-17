using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterVolume : MonoBehaviour
{

    public Transform target;    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = target.position;
        }
    }
}
