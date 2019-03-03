using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Circuit : MonoBehaviour, IHoverAction
{
    private Color[] colors;
    private int index = 0;
    private MeshRenderer ren;

    public int CircuitValue = 999;
    public UnityEvent ValueChanged;

    private void Start()
    {
        colors = new Color[3];
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
        ren = GetComponent<MeshRenderer>();
    }

    public void HoverAction()
    {
        index++;
        if (index >= colors.Length)
        {
            index = 0;
        }
        ren.material.color = colors[index];
        CircuitValue = index;

        if (ValueChanged != null)
            ValueChanged.Invoke();
    }
}
