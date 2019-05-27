using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using System;
using System.Text;
using UnityEditor;

[CreateAssetMenu(fileName = "EmailsSO", menuName = "Prototypes/EmailsSO", order = 0)]
public class EmailsSO : ScriptableObject
{
    [System.Serializable]
    public class Email
    {
        [SerializeField] public string Command;
        [SerializeField] public string Title;
        [SerializeField] public string Subject;
        [SerializeField] public string Contents;
    }
    [SerializeField] private List<Email> emails;

    public List<Email> Emails { get { return emails; } }

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

    public void AddEmail(Email email)
    {
        emails.Add(email);
    }

    public void SaveData()
    {
        EditorUtility.SetDirty(this);
    }

    public void DeleteEmail(int index)
    {
        emails.RemoveAt(index);
    }
}