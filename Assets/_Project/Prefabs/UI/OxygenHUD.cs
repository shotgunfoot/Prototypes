using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OxygenHUD : MonoBehaviour
{
    
    public FloatVariable oxygen;

    public TextMeshProUGUI oxygenText;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        oxygenText = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        oxygenText.text = oxygen.Value.ToString();
    }

}
