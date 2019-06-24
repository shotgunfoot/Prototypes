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
    public bool CheckForEvent(string lowerCase)
    {
        //builder.AppendLine();
        foreach (CompEvent ev in ComputerEvents)
        {
            if (lowerCase == ev.command)
            {
                //builder.Append("Executing command : " + ev.command).AppendLine();
                //builder.Append(ev.response).AppendLine();
                ev.consoleEvent.Raise();
                return true;
            }
        }
        return false;
    }
}

