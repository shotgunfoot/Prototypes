﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{

    public GameObject EquippedObject;
    public GameObject HoverObject;
    public Transform HandFixPoint;
    public string Name;

    public bool DebugView;

    [SerializeField] private Camera cam;

    [SerializeField] private Sprites handSprites;
    [SerializeField] private Image handSprite;
    [SerializeField] private float handDistance = 1f;
    [SerializeField] private bool disabled = false;

    public void HandHover()
    {
        handSprite.enabled = false;
        HoverObject = null;

        //if there's an object in the hand just return.
        if (EquippedObject != null)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(.5f, .5f, 0)), out hit, handDistance))
        {
            if (hit.collider.tag == "Wieldable")
            {
                handSprite.sprite = handSprites.GetSpriteWithName("HandPickUpIcon");
                handSprite.enabled = true;
                HoverObject = hit.collider.gameObject;
            }

            if (hit.collider.tag == "Interactable")
            {
                handSprite.sprite = handSprites.GetSpriteWithName("HandPokeIcon");
                handSprite.enabled = true;
                HoverObject = hit.collider.gameObject;
            }
        }
    }

    public void UseObjectInHandHover()
    {
        if (HoverObject != null)
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
                AttachObject(hand, item.gameObject, item.AttachPositionOffset, item.AttachRotationOffset);
            }
        }

    }

    public void AttachObject(Hand hand, GameObject objectToAttach, Vector3 attachOffset, Vector3 rotationOffset)
    {
        if (attachOffset != Vector3.zero)
        {
            objectToAttach.transform.SetParent(HandFixPoint.transform, true);

            Vector3 offset;
            //if right hand we offset everything positively
            //otherwise we negate the x and z values.
            if (hand.Name == "RightHand")
            {
                offset = new Vector3(-attachOffset.x, attachOffset.y, -attachOffset.z);
            }
            else
            {
                offset = new Vector3(attachOffset.x, attachOffset.y, attachOffset.z);
            }
            objectToAttach.transform.localPosition = Vector3.zero + offset;
            objectToAttach.transform.localRotation = Quaternion.identity * Quaternion.Euler(rotationOffset);
        }
        else
        {
            objectToAttach.transform.SetParent(HandFixPoint.transform);

            objectToAttach.transform.localPosition = Vector3.zero;
            objectToAttach.transform.localRotation = Quaternion.identity;
        }

        //set rigidbody to kinematic and disable all colliders attached is done regardless of attach style.
        objectToAttach.GetComponent<Rigidbody>().isKinematic = true;
        foreach (Collider coll in objectToAttach.GetComponent<HoldableItem>().Colliders)
        {
            coll.enabled = false;
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
            foreach (Collider coll in EquippedObject.GetComponent<HoldableItem>().Colliders)
            {
                coll.enabled = true;
            }
            EquippedObject.transform.SetParent(null);
            EquippedObject.GetComponent<IOnDropAction>().OnDropAction();
            EquippedObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (DebugView)
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            Debug.DrawRay(ray.origin, ray.direction * handDistance, Color.red, .5f);
        }
    }

    public bool Disabled()
    {
        return disabled;
    }

    public void DisableHand()
    {
        disabled = true;
    }

    public void EnableHand()
    {
        disabled = false;
    }
}
