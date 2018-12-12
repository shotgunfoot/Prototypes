using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{

    public FloatVariable _limb;

    public void DamageLimb(float _amount)
    {
		_limb.Value -= _amount;	

		_limb.Value = Mathf.Clamp(_limb.Value, 0, 100);
    }

}
