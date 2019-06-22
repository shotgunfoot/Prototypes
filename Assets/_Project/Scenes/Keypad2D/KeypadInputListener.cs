using UnityEngine;

public class KeypadInputListener : MonoBehaviour
{
    private Keypad keypad;

    private KeyCode[] keycodes = {
        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
    };

    private KeyCode[] numCodes = {
        KeyCode.Keypad0,
        KeyCode.Keypad1,
        KeyCode.Keypad2,
        KeyCode.Keypad3,
        KeyCode.Keypad4,
        KeyCode.Keypad5,
        KeyCode.Keypad6,
        KeyCode.Keypad7,
        KeyCode.Keypad8,
        KeyCode.Keypad9,
    };
    private void Start()
    {
        keypad = GetComponentInParent<Keypad>();
    }

    private void Update()
    {
        for (int i = 0; i < keycodes.Length; i++)
        {
            if (Input.GetKeyDown(keycodes[i]))
            {
                keypad.AddToString(i.ToString());
            }
        }
        for (int i = 0; i < numCodes.Length; i++)
        {
            if (Input.GetKeyDown(numCodes[i]))
            {
                keypad.AddToString(i.ToString());
            }
        }

        if (Input.GetButtonDown("Clear"))
        {
            keypad.Clear();
        }

        if (Input.GetButtonDown("Submit"))
        {
            keypad.SubmitCode();
        }

        if(Input.GetButtonDown("Cancel")){
            keypad.Cancel();
        }
    }


}
