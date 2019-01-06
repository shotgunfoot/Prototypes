using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : WieldableObject
{
    public Color color;    
    public string accessKey;
    private Camera cam;        

    private void Start()
    {
        GetComponent<MeshRenderer>().materials[1].color = color;
    }

    public override void OnPickUpAction()
    {
        cam = FindObjectOfType<Camera>();
    }

    public override void ObjectAction()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(.5f, .5f, 0)), out hit, 5f))
        {
            InWorldKeyCardButton button = hit.collider.GetComponent<InWorldKeyCardButton>();
            if (button != null)
            {
                button.ButtonAction(accessKey);
            }
        }
    }
}
