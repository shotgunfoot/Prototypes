using System.Linq;
using System.Text;
using UnityEngine;

/*
    Class: ConsoleSystem
    Behaviour: This class handles the validation of input from the player which generates the correct
    response in a string. It will also house any events tied to the computer it is attached to. 
    i.e This class controls the Email system which when the event is called will generate string(s) containing relevant
    information in regards to the emails stored within the system.
 */
public class ConsoleSystem : MonoBehaviour
{
    private StringBuilder builder;
    private string fluff = "";

    public ComputerEventsSO ComputerEvents;
    public ConsoleOutput ConsoleOutput;
    public EmailsSO ComputerEmails;

    private bool emailsMenuOpen;

    void Start()
    {
        builder = new StringBuilder();
    }

    public void ValidateInput(string input)
    {
        string lowerCase = input.ToLower();
        builder.Append(fluff);
        builder.Append(input).AppendLine();
        if (ComputerEvents.CheckForEvent(lowerCase, builder))
        {            
            ConsoleOutput.ApplyToText(builder);            
        }
        else if (emailsMenuOpen)
        {
            ComputerEmails.CheckForEmail(lowerCase, builder);
            ConsoleOutput.ApplyToText(builder);
        }
        else
        {
            builder.Append("Unrecognized Command...Type Help to display all commands available").AppendLine();
            ConsoleOutput.ApplyToText(builder);
        }
    }

    public void Help()
    {
        builder.Append(fluff);
        builder.Append("Here are valid commands: ");

        string lastKey = ComputerEvents.ComputerEvents.Last().ToString();

        foreach (ComputerEventsSO.Event compEvent in ComputerEvents.ComputerEvents)
        {
            if (compEvent.Command != lastKey)
            {
                builder.Append(compEvent.Command.ToUpper() + ", ");
            }
            else
            {
                builder.Append(lastKey.ToUpper());
            }
        }
        builder.AppendLine();

        ConsoleOutput.ApplyToText(builder);

    }


    public void ClearStringBuilder()
    {
        builder.Clear();
        ConsoleOutput.ApplyToText(builder);
    }

    public void OpenEmailMenu()
    {

        builder.Append(fluff);   

        foreach (EmailsSO.Email email in ComputerEmails.Emails)
        {
            builder.AppendFormat("{0} - {1} - {2} ", email.Command, email.Title, email.Subject).AppendLine();
        }
        ConsoleOutput.ApplyToText(builder);
        emailsMenuOpen = true;
    }

    public void OpenMainMenu()
    {
        emailsMenuOpen = false;
    }

}