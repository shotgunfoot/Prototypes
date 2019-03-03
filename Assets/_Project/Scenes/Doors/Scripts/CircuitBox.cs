using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CircuitBox : MonoBehaviour
{
    public Circuit[] Circuits;
    public string Solution = "101";
    public UnityEvent CorrectSolution;
    public UnityEvent IncorrectSolution;

    public void CheckSolution()
    {
        string attempt = "";
        foreach (Circuit circ in Circuits)
        {
            attempt += circ.CircuitValue;
        }

        if (attempt == Solution)
        {
            if (CorrectSolution != null)
                CorrectSolution.Invoke();        
        }
        else
        {
            if (IncorrectSolution != null)
                IncorrectSolution.Invoke();
        }
    }
}
