using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMonitor : MonoBehaviour
{
    public FloatVariable oxygen;
    private TimeSince timer;

    private void Start()
    {
        timer = Time.deltaTime;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (timer > 1)
        {
            oxygen.Value = oxygen.Value - 1;
            timer = Time.deltaTime;
        }
        oxygen.Value = Mathf.Clamp(oxygen.Value, 0, 500);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Oxygen")
        {
            oxygen.Value = oxygen.Value += 1;
            oxygen.Value = Mathf.Clamp(oxygen.Value, 0, 30);
        }
    }
}
