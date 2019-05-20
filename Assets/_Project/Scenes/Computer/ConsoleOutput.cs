using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

/*
    Class: ConsoleOutput
    Behaviour: This class contains a reference to a TextMeshProUGUI component that it updates whenever 
    a string is sent to it.
 */
public class ConsoleOutput : MonoBehaviour
{    
    public TextMeshProUGUI console;
    public ScrollRect content;        

    public void ApplyToText(StringBuilder builder)
    {
        console.text = builder.ToString();
        ForceToBottom();
    }

    public void ForceToBottom()
    {
        Canvas.ForceUpdateCanvases();
        content.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }
}