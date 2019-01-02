using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{

    private GameObject EquippedObject;
    public Camera cam;

    public Image handSprite;

    private void Update()
    {
        //if mousedown and hand is empty.
        if (Input.GetMouseButtonDown(0) && EquippedObject == null)
        {
            PickUpItem();            
        }
        else if (Input.GetMouseButtonDown(0))
        {
            UseObjectInHand();
        }

        if (Input.GetMouseButtonDown(1))
        {
            DropItem();
        }

        ItemHover();

    }

    private void ItemHover()
    {
        handSprite.enabled = false;

        RaycastHit hit;
        if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(.5f, .5f, 0)), out hit, 5f))
        {
            if(hit.collider.tag == "Wieldable")
            {
                handSprite.enabled = true;
            }
        }        
    }


    private void UseObjectInHand()
    {
        if (EquippedObject != null)
        {
            EquippedObject.SendMessage("Action", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void PickUpItem()
    {
        //fire a ray from the camera outwards a short distance
        RaycastHit hit;
        if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(.5f, .5f, 0)), out hit, 5f))
        {
            HoldableItem item = hit.collider.GetComponent<HoldableItem>();
            if (item != null)
            {
                if (item.AttachOffset != null)
                {
                    AttachObject(item.gameObject, item.AttachOffset);
                }
            }
        }

    }

    private void AttachObject(GameObject objectToAttach, Transform attachOffset = null)
    {
        if (attachOffset != null)
        {
            objectToAttach.transform.SetParent(gameObject.transform);
            objectToAttach.GetComponent<Rigidbody>().isKinematic = true;
            objectToAttach.GetComponent<Collider>().enabled = false;
            objectToAttach.transform.localPosition = Vector3.zero - attachOffset.localPosition;
            objectToAttach.transform.localRotation = Quaternion.identity;
        }
        else
        {
            objectToAttach.transform.SetParent(gameObject.transform);
            objectToAttach.GetComponent<Rigidbody>().isKinematic = true;
            objectToAttach.GetComponent<Collider>().enabled = false;
            objectToAttach.transform.localPosition = Vector3.zero;
            objectToAttach.transform.localRotation = Quaternion.identity;
        }        
        EquippedObject = objectToAttach;
    }

    private void DropItem()
    {
        if (EquippedObject != null)
        {
            EquippedObject.GetComponent<Rigidbody>().isKinematic = false;
            EquippedObject.GetComponent<Collider>().enabled = true;
            EquippedObject.transform.SetParent(null);            
            EquippedObject = null;
        }
    }

}
