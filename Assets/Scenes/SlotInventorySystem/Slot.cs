using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public Item ItemInSlot;
	public Sprite blankSlotIcon;

    public Image icon;


    public void SetIcon()
    {
        if (ItemInSlot != null)
        {
            icon.sprite = ItemInSlot.Icon;
			icon.color = new Color(1,1,1, 1);
        }
    }

	public void RemoveItem(){
		ItemInSlot = null;
		icon.sprite = blankSlotIcon;
		icon.color = new Color(1,1,1, 0);
	}
}
