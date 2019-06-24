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

    public ComputerEventSO ComputerEvents;
    public ConsoleOutput ConsoleOutput;
    public EmailsSO ComputerEmails;

    private bool emailsMenuOpen;

    void Start()
    {
        builder = new StringBuilder();
    }

    public void ValidateInput(string input)
    {
        if (!ConsoleOutput.isApplyingText)
        {
            string lowerCase = input.ToLower();

            builder.Append(input).AppendLine();

            if (ComputerEvents.CheckForEvent(lowerCase))
            {
                //SendToConsoleOutput();
            }
            else if (emailsMenuOpen)
            {
                ComputerEmails.CheckForEmail(lowerCase, builder);
                SendToConsoleOutput();
            }
            else
            {
                builder.Append("Unrecognized Command...Type Help to display all commands available").AppendLine();
                SendToConsoleOutput();
            }
        }
    }

    public void Help()
    {
        string lastKey = ComputerEvents.ComputerEvents.Last().command;        
        foreach (ComputerEventSO.CompEvent CE in ComputerEvents.ComputerEvents)
        {
            if (CE.command != lastKey)
            {
                builder.Append(CE.command.ToLower() + ", ");
            }
            else
            {
                builder.Append(lastKey.ToLower());
            }
        }
        builder.AppendLine();

        SendToConsoleOutput();

    }

    public void OpenEmailMenu()
    {
        foreach (EmailsSO.Email email in ComputerEmails.Emails)
        {
            builder.AppendFormat("{0} - {1} - {2} ", email.Command, email.Title, email.Subject).AppendLine();
        }
        SendToConsoleOutput();
        emailsMenuOpen = true;
    }

    public void OpenMainMenu()
    {
        emailsMenuOpen = false;
    }

//BUG ISSUE
//This isn't ?thread safe? If thats the correct term.
//At no point am I checking ot see if the coroutine being called is already running, therefore it will attempt to hijack the 
//string builders in use and cause a horrific never ending while loop.

//Current fix: Careful where I am calling this method. The problem with that is its screwing over when events have a respone to add to the builder and when the 
//event is raised it also wants to add to the stringbuilder.

//Possible solution: 
    private void SendToConsoleOutput()
    {
        ConsoleOutput.ApplyToText(builder);        
    }

}