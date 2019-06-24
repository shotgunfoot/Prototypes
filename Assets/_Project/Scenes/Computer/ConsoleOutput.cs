using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using System.Collections;

/*
    Class: ConsoleOutput
    Behaviour: This class contains a reference to a TextMeshProUGUI component that it updates whenever 
    a string is sent to it.
 */
public class ConsoleOutput : MonoBehaviour
{
    public TextMeshProUGUI console;
    public ScrollRect content;
    private StringBuilder localBuilder = new StringBuilder();
    public bool isApplyingText = false;

    [SerializeField] private float textRevealSpeed;
    public void ApplyToText(StringBuilder builder)
    {
        isApplyingText = true;
        StartCoroutine(AppendToTextSlowly(builder));
    }

    public IEnumerator AppendToTextSlowly(StringBuilder _builder)
    {
        //loop through passed in string builder length, adding the currently focused letter to the localstringbuilder
        //then update the text to reflect it, then continue the loop at a designated speed.
        int i = 0;
        float timer = 0;        
        while (i < _builder.Length)
        {
            if (timer > textRevealSpeed)
            {
                localBuilder.Append(_builder[i].ToString());
                console.text = localBuilder.ToString();
                i++;
                timer = 0;
                ForceToBottom();
            }
            timer += Time.deltaTime;
            yield return null;
        }
        _builder.Clear();
        isApplyingText = false;
    }

    public void ForceToBottom()
    {
        Canvas.ForceUpdateCanvases();
        content.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    public void ClearText(){
        localBuilder.Clear();
        console.text = "";
        ForceToBottom();        
    }
}