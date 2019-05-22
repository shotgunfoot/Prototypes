using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CustomOnEndEdit : MonoBehaviour
{
    private TMP_InputField inputField;
    public StringEvent OnSubmit;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }


    public void Validate(string input)
    {
        if (Input.GetButtonDown("Submit"))
        {
            if(OnSubmit != null){
                OnSubmit.Invoke(input);
            }
        }
    }
}