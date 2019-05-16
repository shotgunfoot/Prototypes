using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class ComputerConsole : MonoBehaviour
{

    public ComputerEvent computerEvents;

    public TextMeshProUGUI console;
    public ScrollRect content;

    private string fluff = ">. ";
    private StringBuilder builder;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        builder = new StringBuilder();
    }

    public void Validate(string input)
    {
        string lowerCase = input.ToLower();

        builder.Append(fluff);        

        if (computerEvents.CheckEventsAndInvoke(lowerCase))
        {
            builder.Append("Executing Command...").AppendLine();       
            console.text = builder.ToString();
            computerEvents.RaiseGameEvent(lowerCase);     
        }
        else
        {
            builder.Append("Unrecognized Command...").AppendLine();
            console.text = builder.ToString();
        }

        ForceToBottom();
    }

    public void Help()
    {
        builder.Append(fluff);
        builder.Append("Here are valid commands ");

        string lastKey = computerEvents.Commands.Keys.Last();

        foreach (string command in computerEvents.Commands.Keys)
        {
            if (command != lastKey)
            {
                builder.Append(command.ToUpper() + ", ");
            }else{
                builder.Append(lastKey.ToUpper());
            }
        }
        builder.AppendLine();

        console.text = builder.ToString();

        ForceToBottom();
    }

    public void ForceToBottom()
    {
        content.verticalNormalizedPosition = 0f;
    }
}