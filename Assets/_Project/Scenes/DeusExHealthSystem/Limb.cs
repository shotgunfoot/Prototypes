using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour, IHoverAction
{

    public FloatVariable _limb;
    public float damage = 10f;

    public void DamageLimb(float _amount)
    {
        _limb.Value -= _amount;
        _limb.Value = Mathf.Clamp(_limb.Value, 0, 100);
    }

    public void HoverAction()
    {
        DamageLimb(damage);
    }
}
