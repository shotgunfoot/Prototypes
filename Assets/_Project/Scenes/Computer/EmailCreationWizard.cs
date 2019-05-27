using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EmailCreationWizard : EditorWindow
{
    private string newCommand;
    private string newEmailTitle;
    private string newSubject;
    private string newContent;
    private EmailsSO.Email newEmail;
    private UnityEngine.Object EmailsToAddTo;
    private EmailsSO emailsSO;
    private List<bool> toggles;
    private Vector2 scrollPos;
    private GUIStyle richTextStyle = new GUIStyle();
    private GUIStyle buttonRedStyle = new GUIStyle(EditorStyles.toolbarButton);
    private GUIStyle errorCodeStyle = new GUIStyle(EditorStyles.helpBox);
    private string errorString;

    public void ShowWindow(EmailsSO _email)
    {
        EmailsToAddTo = _email;
        emailsSO = _email;
        UpdateToggles();

        richTextStyle.richText = true;
        buttonRedStyle.active.textColor = Color.red;
        buttonRedStyle.normal.textColor = Color.red;

        GetWindow<EmailCreationWizard>("Email Creation Window");
    }

    void OnDestroy()
    {
        emailsSO.SaveData();
    }

    void OnGUI()
    {
        GUILayout.Label("Email Creation", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Email Scriptable Object : <b>" + EmailsToAddTo.name + "</b>", richTextStyle);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(this.position.width));

        //display number of emails in the SO;
        GUILayout.Label("Number of emails in this SO : " + emailsSO.Emails.Count);

        //Loop through and display any stored emails.
        DisplayStoredEmails();

        EditorGUILayout.EndScrollView();

        GUILayout.Space(15);

        GUILayout.Label("Create a new email here!");

        NewEmail();

        if (GUILayout.Button("Add new email", GUILayout.ExpandWidth(false)))
        {
            ValidateInput();
            errorString = "Email added and saved.";
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Save Any Changes", GUILayout.ExpandWidth(false)))
        {
            emailsSO.SaveData();
            errorString = "Data has been saved.";
        }

        GUILayout.FlexibleSpace();
        //Show errors here if any
        GUILayout.Label(errorString, errorCodeStyle);

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
                i = 0;
            }

            GUILayout.EndHorizontal();

            if (toggles[i])
            {

                GUILayout.BeginHorizontal();

                GUILayout.Label("Command: ", GUILayout.ExpandWidth(false));
                emailsSO.Emails[i].Command = GUILayout.TextField(emailsSO.Emails[i].Command);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                GUILayout.Label("Title: ", GUILayout.ExpandWidth(false));
                emailsSO.Emails[i].Title = GUILayout.TextField(emailsSO.Emails[i].Title);

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                GUILayout.Label("Subject: ", GUILayout.ExpandWidth(false));
                emailsSO.Emails[i].Subject = GUILayout.TextField(emailsSO.Emails[i].Subject);

                GUILayout.EndHorizontal();

                GUILayout.Label("Content:");
                emailsSO.Emails[i].Contents = GUILayout.TextArea(emailsSO.Emails[i].Contents, GUILayout.ExpandHeight(true));

                // if (GUILayout.Button("Overwrite?"))
                // {
                //     Overwrite(i);
                // }
            }

            GUILayout.EndVertical();

        }
    }

    private void NewEmail()
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label("Command: ", GUILayout.ExpandWidth(false));
        newCommand = GUILayout.TextField(newCommand);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.Label("Title: ", GUILayout.ExpandWidth(false));
        newEmailTitle = GUILayout.TextField(newEmailTitle);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.Label("Subject: ", GUILayout.ExpandWidth(false));
        newSubject = GUILayout.TextField(newSubject);

        GUILayout.EndHorizontal();

        GUILayout.Label("Content:");
        newContent = GUILayout.TextArea(newContent, GUILayout.ExpandHeight(true));
    }

    private void ValidateInput()
    {
        bool canCreate = false;
        for (int i = 0; i < emailsSO.Emails.Count; i++)
        {
            if (newCommand == emailsSO.Emails[i].Command)
            {
                canCreate = false;
            }
            else
            {
                canCreate = true;
            }
        }
        if (canCreate)
        {
            CreateEmail();
        }
        else
        {
            errorString = "There is already an email with that command, type another!";
            return;
        }
    }

    private void DeleteEmail(int index)
    {
        emailsSO.DeleteEmail(index);
        UpdateToggles();
    }

    private void CreateEmail()
    {
        newEmail = new EmailsSO.Email();
        newEmail.Command = newCommand.ToLower();
        newEmail.Title = newEmailTitle;
        newEmail.Subject = newSubject;
        newEmail.Contents = newContent;
        if (EmailsToAddTo != null)
        {
            emailsSO.AddEmail(newEmail);
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

}
