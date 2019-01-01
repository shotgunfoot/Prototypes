using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    public float ShakeDuration;
    public float ShakeStrength;    
    public bool SmoothShake;
    public float SmoothAmount;

    private bool shake;
    private bool shakeRoutineRunning;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.N))
        {
            shake = !shake;
        }

        if (shake)
        {
            //do the shake

        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!shakeRoutineRunning)
            {
                StartCoroutine(ShakeRoutine(ShakeDuration, ShakeStrength));
            }            
        }
	}

    IEnumerator ShakeRoutine(float shakeDuration, float shakeStrength)
    {
        shakeRoutineRunning = true;
        
        float shakePerc;

        float shakeDurationRemaining = shakeDuration;

        while(shakeDurationRemaining > .1f)
        {
            Vector3 rotation = Random.insideUnitSphere * shakeStrength;
            rotation.z = 0;

            shakePerc = shakeDurationRemaining / shakeDuration;

            shakeDuration = Mathf.Lerp(shakeDuration, 0, shakePerc);

            if (SmoothShake)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotation), Time.deltaTime * SmoothAmount);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(rotation);
            }

            shakeDurationRemaining -= Time.deltaTime;

            yield return null;
        }

        transform.rotation = Quaternion.identity;

        shakeRoutineRunning = false;

        yield return null;
    }
}
