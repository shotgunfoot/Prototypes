using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HoldableItem))]
public class WieldableObject : MonoBehaviour, IHoverAction, IOnPickUpAction, IObjectAction, IOnDropAction
{
    public virtual void HoverAction()
    {

    }

    public virtual void ObjectAction()
    {

    }

    public virtual void OnPickUpAction()
    {

    }

    public virtual void OnDropAction()
    {

    }
}
