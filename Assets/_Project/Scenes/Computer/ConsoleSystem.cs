using System.Linq;
using System.Text;
using RoboRyanTron.Unite2017.Events;
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
    private string fluff = ">. ";

    public ComputerEvent ComputerEvents;
    public ConsoleOutput ConsoleOutput;
    void Start()
    {
        builder = new StringBuilder();
    }

    public void ValidateInput(string input)
    {
        string lowerCase = input.ToLower();

        builder.Append(fluff);
        builder.Append(input).AppendLine();
        if (ComputerEvents.CheckForEvent(lowerCase))
        {
            builder.Append("Executing Command...").AppendLine();
            ConsoleOutput.ApplyToText(builder);
            ComputerEvents.RaiseGameEvent(lowerCase);
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
        builder.Append("Here are valid commands ");

        string lastKey = ComputerEvents.Commands.Keys.Last();

        foreach (string command in ComputerEvents.Commands.Keys)
        {
            if (command != lastKey)
            {
                builder.Append(command.ToUpper() + ", ");
            }
            else
            {
                builder.Append(lastKey.ToUpper());
            }
        }
        builder.AppendLine();

        ConsoleOutput.ApplyToText(builder);

    }

    private int numberOfUnreadEmails = 4;


    //Change this to have a reference to a ScriptableObject called "Email" or "ComputerEmail".
    //That SO has a number of emails (strings) stored each with a bool to represent if they are unread or read.
    //This email function will then loop through grabbing all email titles and if they are unread or not.
    //When this function is ran the computer is in a submenu and can access new commands. These commands are the titles of each email.
    //Typing the titles of the email will read its content to the console output.
    //May need to make this a seperate monobehaviour script dubbed "ConsoleEmail";
    public void Email()
    {
        builder.Append(fluff);
        builder.Append("Here are your emails: ").AppendLine();        
        builder.AppendFormat("You have {0} unread emails.", numberOfUnreadEmails).AppendLine();

        ConsoleOutput.ApplyToText(builder);
    }

}