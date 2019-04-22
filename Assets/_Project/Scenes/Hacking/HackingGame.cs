using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingGame : MonoBehaviour
{
    
    [SerializeField] private List<int> solution;
    private int solutionLength = 4;
    private int counter = 0;

    public List<Sprite> images;
    public List<Image> solutionImages;    

    private void Start()
    {        
        for (int i = 0; i < solutionLength; i++)
        {
            solution[i] = Random.Range(1, 8);
        }

        DisplaySolution();
    }

    private void DisplaySolution()
    {
        for (int i = 0; i < solutionLength; i++)
        {
            solutionImages[i].sprite = images[solution[i]];
        }
    }

    public void InputNumber(int num)
    {
        if (num == solution[counter])
        {
            solutionImages[counter].sprite = null;
            counter++;
        }
        else
        {
            counter = 0;
            
        }

        if (counter == solutionLength)
        {
            
            counter = 0;
        }
    }
}
