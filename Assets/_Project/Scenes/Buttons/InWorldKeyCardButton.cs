using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The same functionality as any button but a specific keycard is needed for it to activate.
/// </summary>
public class InWorldKeyCardButton : MonoBehaviour, IHoverAction
{
    public string KeyRequiredToWork;

    public UnityEvent SuccessResponse;
    public UnityEvent FailureResponse;

    public void ButtonAction(string key)
    {
        if (key == KeyRequiredToWork)
        {
            if (SuccessResponse != null)
            {
                SuccessResponse.Invoke();
            }
        }
    }

    public void HoverAction()
    {
        if (FailureResponse != null)
        {
            FailureResponse.Invoke();
        }
    }
}
