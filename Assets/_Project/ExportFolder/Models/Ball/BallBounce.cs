using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour, IHoverAction
{
    Rigidbody rb;
    public float StrengthOfForce = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void HoverAction()
    {
        AddRandomForce();
    }

    private void AddRandomForce()
    {
        Debug.Log(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
        rb.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * StrengthOfForce, ForceMode.Impulse);
    }
}
