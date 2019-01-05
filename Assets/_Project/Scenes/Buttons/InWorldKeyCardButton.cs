using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The same functionality as any button but a specific keycard is needed for it to activate.
/// </summary>
public class InWorldKeyCardButton : MonoBehaviour
{
    public string KeyRequiredToWork;

    public UnityEvent unityEvent;

    public void Action(string key)
    {
        if(key == KeyRequiredToWork)
        {
            if (unityEvent != null)
            {
                unityEvent.Invoke();
            }
        }       
    }
}
