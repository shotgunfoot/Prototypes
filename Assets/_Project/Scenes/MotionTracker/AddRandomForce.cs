using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRandomForce : MonoBehaviour
{

	private Rigidbody rigidbody;
	private TimeSince time;

    public float TimeUntilRandomForce = 1f;
	public float ForceStrength = 2f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
		time = 0;
    }

    private void Update()
    {		
        if (time > TimeUntilRandomForce)
        {
			Vector3 randomDirection = new Vector3(Random.Range(0, 360), Random.Range(0, 360),Random.Range(0, 360));
			rigidbody.AddForce(randomDirection * ForceStrength);
			time = 0;			
        }		
    }
}
