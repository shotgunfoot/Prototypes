using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.SetParent(GetComponentInParent<Transform>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
