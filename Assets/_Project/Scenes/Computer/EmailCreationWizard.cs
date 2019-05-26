using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EmailCreationWizard : EditorWindow
{
    public string Command;
    public string EmailTitle;
    public string Subject;
    public string Content;
    public EmailsSO.Email Email;
    public UnityEngine.Object EmailsToAddTo;
    private EmailsSO emailsSO;
    private List<bool> toggles;
    private Vector2 scrollPos;
    GUIStyle boldStyle = new GUIStyle();
    GUIStyle buttonRedStyle = new GUIStyle(EditorStyles.toolbarButton);

    public void ShowWindow(EmailsSO _email)
    {
        EmailsToAddTo = _email;
        emailsSO = _email;
        UpdateEmailData();
        UpdateToggles();

        boldStyle.richText = true;
        buttonRedStyle.active.textColor = Color.red;
        buttonRedStyle.normal.textColor = Color.red;

        GetWindow<EmailCreationWizard>("Email Creation Window");
    }

    void OnGUI()
    {
        GUILayout.Label("Email Creation", EditorStyles.boldLabel);


        EditorGUILayout.LabelField("Email Scriptable Object : <b>" + EmailsToAddTo.name + "</b>", boldStyle);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(this.position.width));

        //display number of emails in the SO;
        GUILayout.Label("Number of emails in this SO : " + emailsSO.Emails.Count);

        //Loop through and display any stored emails.
        DisplayStoredEmails();

        EditorGUILayout.EndScrollView();

        GUILayout.Space(5);

        // GUILayout.Label("New Email");
        // Command = EditorGUILayout.TextField("Command :", Command);
        // EmailTitle = EditorGUILayout.TextField("Title :", EmailTitle);
        // Subject = EditorGUILayout.TextField("Subject :", Subject);
        // GUILayout.Label("Content:");
        // Content = EditorGUILayout.TextArea("", GUILayout.ExpandHeight(true));

        // if (GUILayout.Button("Add Email"))
        // {
        //     CreateEmail();
        // }
    }

    private void DisplayStoredEmails()
    {
        for (int i = 0; i < emailsSO.Emails.Count; i++)
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.BeginHorizontal();


            if (GUILayout.Button(emailsSO.Emails[i].Title, GUILayout.ExpandWidth(false)))
            {
                toggles[i] = !toggles[i];     
            }

            if (GUILayout.Button("Delete This Email", buttonRedStyle, GUILayout.ExpandWidth(false)))
            {
                DeleteEmail(i);
            }

            GUILayout.EndHorizontal();

            if (toggles[i])
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("Command: ", GUILayout.ExpandWidth(false));
                GUILayout.Label(emailData[i].Command, GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                GUILayout.Label("Title: ", GUILayout.ExpandWidth(false));
                GUILayout.Label(emailData[i].Title, GUILayout.ExpandWidth(false));

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                GUILayout.Label("Subject: ", GUILayout.ExpandWidth(false));
                GUILayout.Label(emailData[i].Subject, GUILayout.ExpandWidth(false));

                GUILayout.EndHorizontal();

                GUILayout.Label("Content:");
                GUILayout.Label(emailData[i].Content, GUILayout.ExpandWidth(false));

                if (GUILayout.Button("Overwrite?"))
                {
                    Overwrite(i);
                }
            }
            GUILayout.EndVertical();

            GUILayout.Space(10);
        }
    }

    private void DeleteEmail(int index)
    {
        emailsSO.Emails.RemoveAt(index);
        UpdateEmailData();
        UpdateToggles();
    }

    private void Overwrite(int index)
    {
        Email = new EmailsSO.Email();
        Email.Command = emailData[index].Command.ToLower();
        Email.Title = emailData[index].Title;
        Email.Subject = emailData[index].Subject;
        Email.Contents = emailData[index].Content;
        if (EmailsToAddTo != null)
        {
            emailsSO.OverwriteEmail(Email, index);
        }
    }

    private void CreateEmail()
    {
        Email = new EmailsSO.Email();
        Email.Command = Command.ToLower();
        Email.Title = EmailTitle;
        Email.Subject = Subject;
        Email.Contents = Content;
        if (EmailsToAddTo != null)
        {
            emailsSO.AddEmail(Email);
        }

        UpdateToggles();
    }

    private void UpdateToggles()
    {
        toggles = new List<bool>();
        for (int i = 0; i < emailsSO.Emails.Count; i++)
        {
            toggles.Add(false);
        }
    }

    private void UpdateEmailData()
    {
        emailData = new List<EmailData>();
        for (int i = 0; i < emailsSO.Emails.Count; i++)
        {
            EmailData email = new EmailData();
            email.Command = (emailsSO.Emails[i].Command);
            email.Title = (emailsSO.Emails[i].Title);
            email.Subject = (emailsSO.Emails[i].Subject);
            email.Content = (emailsSO.Emails[i].Contents);
            emailData.Add(email);
        }
    }

    public List<EmailData> emailData;

    public struct EmailData
    {
        private string command;
        private string title;
        private string subject;
        private string content;

        public string Command { get => command; set => command = value; }
        public string Title { get => title; set => title = value; }
        public string Subject { get => subject; set => subject = value; }
        public string Content { get => content; set => content = value; }
    }

}
