using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EmailsSO))]
public class EmailsSOEditor : Editor
{
    EmailsSO email;    

    public override void OnInspectorGUI()
    {
        if (email == null)
        {
            email = (EmailsSO)target;
        }

        if (GUILayout.Button("Open Email Builder"))
        {
            EmailCreationWizard wiz = EditorWindow.GetWindow<EmailCreationWizard>();
            wiz.ShowWindow(email);   
        }
        base.DrawDefaultInspector();
    }

}