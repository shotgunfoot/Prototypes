using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{

    public GameObject EquippedObject;
    public GameObject HoverObject;
    public Transform HandFixPoint;
    [SerializeField] private Camera cam;

    [SerializeField] private Sprites handSprites;
    [SerializeField] private Image handSprite;
    [SerializeField] private float handDistance = 1f;

    public void HandHover()
    {
        handSprite.enabled = false;
        HoverObject = null;

        //if there's an object in the hand just return.
        if(EquippedObject != null)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(.5f, .5f, 0)), out hit, handDistance))
        {
            if(hit.collider.tag == "Wieldable")
            {
                handSprite.sprite = handSprites.GetSpriteWithName("HandPickUpIcon");
                handSprite.enabled = true;
                HoverObject = hit.collider.gameObject;
            }

            if(hit.collider.tag == "Interactable")
            {
                handSprite.sprite = handSprites.GetSpriteWithName("HandPokeIcon");
                handSprite.enabled = true;
                HoverObject = hit.collider.gameObject;
            }            
        }
    }

    

    public void UseObjectInHandHover()
    {
        if(HoverObject != null)
        {
            //HoverObject.SendMessage("HoverAction", SendMessageOptions.DontRequireReceiver);
            HoverObject.GetComponent<IHoverAction>().HoverAction();
        }
    }

    public void UseObjectInHand()
    {
        if (EquippedObject != null)
        {
            //EquippedObject.SendMessage("Action", SendMessageOptions.DontRequireReceiver);
            EquippedObject.GetComponent<IObjectAction>().ObjectAction();
        }
    }

    public void PickUpItem(Hand hand)
    {
        //fire a ray from the camera outwards a short distance
        RaycastHit hit;
        if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(.5f, .5f, 0)), out hit, handDistance))
        {
            HoldableItem item = hit.collider.GetComponent<HoldableItem>();
            if (item != null)
            {
                if (item.AttachOffset != null)
                {
                    AttachObject(hand, item.gameObject, item.AttachOffset);
                }
            }
        }

    }

    public void AttachObject(Hand hand, GameObject objectToAttach, Transform attachOffset = null)
    {
        if (attachOffset != null)
        {
            objectToAttach.transform.SetParent(HandFixPoint.transform, true);
            objectToAttach.GetComponent<Rigidbody>().isKinematic = true;
            objectToAttach.GetComponent<Collider>().enabled = false;
            objectToAttach.transform.localPosition = Vector3.zero + attachOffset.localPosition;
            objectToAttach.transform.localRotation = transform.localRotation * attachOffset.localRotation;                
        }
        else
        {
            objectToAttach.transform.SetParent(HandFixPoint.transform);
            objectToAttach.GetComponent<Rigidbody>().isKinematic = true;
            objectToAttach.GetComponent<Collider>().enabled = false;
            objectToAttach.transform.localPosition = Vector3.zero;
            objectToAttach.transform.localRotation = Quaternion.identity;
        }        
        EquippedObject = objectToAttach;

        //EquippedObject.SendMessage("OnPickUp", SendMessageOptions.DontRequireReceiver);       
        EquippedObject.GetComponent<IOnPickUpAction>().OnPickUpAction();
    }

    public void DropItem()
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
