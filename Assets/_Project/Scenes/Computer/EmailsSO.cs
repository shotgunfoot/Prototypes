using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using System;
using System.Text;

[CreateAssetMenu(fileName = "EmailsSO", menuName = "Prototypes/EmailsSO", order = 0)]
public class EmailsSO : ScriptableObject
{
    [System.Serializable]
    public struct Email
    {
        [SerializeField] private string command;
        [SerializeField] private string title;
        [SerializeField] private string subject;
        [SerializeField] private string contents;        
        public string Command { get => command; }
        public string Title { get => title; }
        public string Subject { get => subject; }
        public string Contents { get => contents; }        
    }
    [SerializeField] private List<Email> emails;

    public List<Email> Emails { get => emails; }

    public bool CheckForEmail(string lowerCase, StringBuilder builder)
    {
        builder.AppendLine();
        foreach (Email email in emails)
        {
            if (lowerCase == email.Command)
            {
                builder.Append("Title : " + email.Title).AppendLine();
                builder.Append("Subject : " + email.Subject).AppendLine();
                builder.Append(email.Contents).AppendLine();                
                return true;
            }
        }
        return false;
    }
}