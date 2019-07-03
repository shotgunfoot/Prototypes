using System.Collections.Generic;
using System.Text;
using RoboRyanTron.Unite2017.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "ComputerEventSO", menuName = "Prototypes/ComputerEvent", order = 0)]
public class ComputerEventSO : ScriptableObject
{

    [System.Serializable]
    public struct CompEvent
    {
        public string command;
        public string response;
        public GameEvent consoleEvent;
    }

    public List<CompEvent> ComputerEvents;

    //Checks if the SO has the event stored and if so, raises the event and returning true. Otherwise returns false.
    public bool CheckForEvent(string lowerCase, StringBuilder builder)
    {
        builder.AppendLine();
        foreach (CompEvent ce in ComputerEvents)
        {
            if (lowerCase == ce.command)
            {
                builder.Append("Executing command : " + ce.command).AppendLine();
                builder.Append(ce.response).AppendLine();
                ce.consoleEvent.Raise();
                return true;
            }
        }
        return false;
    }
}

