using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public Color color;    
    public string accessKey;
    private Camera cam;        

    private void Start()
    {
        GetComponent<MeshRenderer>().materials[1].color = color;
    }

    public void Action()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(.5f, .5f, 0)), out hit, 5f))
        {
            InWorldKeyCardButton button = hit.collider.GetComponent<InWorldKeyCardButton>();
            if(button != null)
            {
                button.Action(accessKey);
            }
        }
    }

    public void OnPickUp()
    {
        cam = FindObjectOfType<Camera>();
    }
}
