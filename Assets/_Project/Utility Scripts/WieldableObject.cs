using UnityEngine;
using System.Collections;

public class WieldableObject : MonoBehaviour, IHoverAction, IOnPickUpAction, IObjectAction
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
}
