using System;
using System.Collections.Generic;
using RoboRyanTron.Unite2017.Events;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Text;

[CreateAssetMenu(fileName = "ComputerEvent", menuName = "Prototypes/ComputerEvent", order = 0)]
public class ComputerEventsSO : ScriptableObject
{
    [System.Serializable]
    public struct Event
    {
        [SerializeField] private GameEvent consoleEvent;
        [SerializeField] private string command;
        [SerializeField] private string response;

        public GameEvent ConsoleEvent { get => consoleEvent; }
        public string Command { get => command; }
        public string Response { get => response;}
    }

    public List<Event> ComputerEvents;

    public bool CheckForEvent(string lowerCase, StringBuilder builder)
    {
        builder.AppendLine();
        foreach (Event ev in ComputerEvents)
        {
            if (lowerCase == ev.Command)
            {
                builder.Append("Executing command : " + ev.Command).AppendLine();
                builder.Append(ev.Response).AppendLine();
                ev.ConsoleEvent.Raise();           
                return true;
            }
        }
        return false;
    }
}