using UnityEngine;
using System.Collections;

public class LeftHand : Hand
{
    public string Name = "LeftHand";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LeftHand"))
        {            
            //if hand holding object use the object
            if (EquippedObject != null)
            {
                UseObjectInHand();
            }

            //if hand empty try to pick it up
            if (EquippedObject == null)
            {
                PickUpItem(this);
            }

            //if hand hovering over something try to interact with it
            if (HoverObject != null)
            {
                UseObjectInHandHover();
            }
        }

        if (Input.GetButtonDown("DropLeftHand"))
        {
            DropItem();
        }

        HandHover();
    }
}
