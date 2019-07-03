using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Pickup : WieldableObject
{

    public override void OnPickUpAction()
    {
        GetComponentInChildren<Melee_Weapon_Medium_Controller>().enabled = true;
    }

    public override void OnDropAction()
    {
        GetComponentInChildren<Melee_Weapon_Medium_Controller>().enabled = false;
    }
}
