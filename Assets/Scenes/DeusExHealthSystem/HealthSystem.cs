using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{

    //limb health system
    //Rules: 
    //All limbs have their own HP that range from 0-100.
    //All limbs have a crippled status when under 25%.
    //Limbs that reach 0 are considered dead.
    //If the torso or head limb reaches 0 the person is considered dead.

    //The overall HP of the person is all limbs health combined. Some rules override general rules.

    public FloatVariable head, torso, legLeft, legRight, armRight, armLeft;	

    private void Start()
    {
		armLeft.Value = 100f;
        head = torso = legLeft = legRight = armRight = armLeft;
    }

    public void HurtLimb(FloatVariable _limb, float _amount)
    {

    }

}
