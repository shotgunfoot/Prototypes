using UnityEngine;
using System.Collections;

public class RightHand : Hand
{

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetButtonDown("RightHand"))
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
            if(HoverObject != null)
            {
                UseObjectInHandHover();
            }
        }

        if (Input.GetButtonDown("DropRightHand"))
        {
            DropItem();
        }

        ItemHover();
    }
}
