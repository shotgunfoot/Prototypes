using UnityEngine;

public class CubeRandomColor : MonoBehaviour
{

    private Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    public void AssignRandomColor()
    {
        mat.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

}