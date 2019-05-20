using System;
using System.Collections.Generic;
using RoboRyanTron.Unite2017.Events;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "ComputerEvent", menuName = "Prototypes/ComputerEvent", order = 0)]
public class ComputerEvent : SerializedScriptableObject
{

    [Header("The keys need to be lowercase!")]
    public Dictionary<string, GameEvent> Commands;
    

    public bool CheckForEvent(string input)
    {
        foreach (string key in Commands.Keys)
        {
            if (key.ToLower() == input)
            {
                return true;
            }
        }
        return false;
    }

    public void RaiseGameEvent(string key)
    {
        GameEvent temp;
        Commands.TryGetValue(key, out temp);
        temp.Raise();
    }
}