using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public int BaseNumberOfSlots;
    //The parent for all slots to be created under.
    public GameObject SlotHolder;
    //The prefab for an empty slot
    public Slot Slot;
    private Slot[] slots;

    private void Start()
    {
        slots = new Slot[BaseNumberOfSlots];
        for (int i = 0; i < BaseNumberOfSlots; i++)
        {
            slots[i] = Instantiate(Slot, SlotHolder.transform);
        }
        SlotHolder.transform.localPosition = new Vector3(0, -1000, 0); //we do this so that the scrollrect is at the top when first viewing. Remove this line and you'll see what I mean.
    }

    public void AddItemToSlot(Item item)
    {
        for (int i = 0; i < BaseNumberOfSlots; i++)
        {
            if (slots[i].ItemInSlot == null)
            {
                slots[i].ItemInSlot = item;
                slots[i].SetIcon();
                break;
            }
        }
    }    
}
