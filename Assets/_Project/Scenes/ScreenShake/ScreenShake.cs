using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    public float ShakeDuration;
    public float ShakeStrength;
    public bool SmoothShake;
    public float SmoothAmount;

    private Camera cam;
    private bool shake;
    private bool shakeRoutineRunning;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    public void StartShakeRoutine()
    {
        StartCoroutine(ShakeRoutine(ShakeDuration, ShakeStrength));
    }

    private IEnumerator ShakeRoutine(float shakeDuration, float shakeStrength)
    {
        shakeRoutineRunning = true;        

        float shakePerc;

        float shakeDurationRemaining = shakeDuration;

        while (shakeDurationRemaining > 0)
        {
            Vector3 rotation = Random.insideUnitSphere * shakeStrength;
            rotation.z = 0;

            shakePerc = shakeDurationRemaining / shakeDuration;

            shakeDuration = Mathf.Lerp(shakeDuration, 0, shakePerc);

            if (SmoothShake)
            {
                cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(rotation), Time.deltaTime * SmoothAmount);
            }
            else
            {
                cam.transform.localRotation = Quaternion.Euler(rotation);
            }

            shakeDurationRemaining -= Time.deltaTime;

            yield return null;
        }        

        shakeRoutineRunning = false;

        yield return null;
    }
}
