using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIManipulation
{

    // Class: GameBoyButtonController
    // Use: This class controls the visual settings of buttons and the text within the buttons that are 
    // used in the gameboy's screen. 
    //e.g when scrolling up an down on the menu buttons it will change the highlighted button's colors and revert the others.    
    public class GameBoyButtonController : MonoBehaviour
    {

        private List<Button> Buttons;
        private int buttonIndex;

        private void Start()
        {
            Buttons = new List<Button>();
            foreach (Button b in GetComponentsInChildren<Button>())
            {
                Buttons.Add(b);
            }
            Buttons.Reverse();
            buttonIndex = 0;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (Input.GetAxis("MouseWheel") != 0)
            {
                //this line clamps the index between 0 and the last index value of the buttons list.
                buttonIndex = Mathf.Clamp(buttonIndex += Mathf.RoundToInt(Input.GetAxis("MouseWheel")), 0, Buttons.Count - 1);

                //change the button to be highlighted and unhighlight the rest.
                Buttons[buttonIndex].Select();

                for (int i = 0; i < Buttons.Count; i++)
                {
                    if (i == buttonIndex)
                    {
                        TextMeshProUGUI text = Buttons[i].GetComponentInChildren<TextMeshProUGUI>();
                        text.color = Color.black;
                    }
                    else
                    {
                        TextMeshProUGUI text = Buttons[i].GetComponentInChildren<TextMeshProUGUI>();
                        text.color = Color.white;
                    }
                }
            }

            if (Input.GetButtonDown("LeftHand"))
            {
                Buttons[buttonIndex].onClick.Invoke();
            }
        }

    }
}
