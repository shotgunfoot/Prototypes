using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignRandomColor : MonoBehaviour
{
    public void ApplyRandomColor()
    {
        GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
