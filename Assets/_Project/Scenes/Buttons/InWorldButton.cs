using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InWorldButton : MonoBehaviour
{
    public UnityEvent unityEvent;    
    
    public void HoverAction()
    {
        if(unityEvent != null)
        {
            unityEvent.Invoke();
        }        
    }
}
