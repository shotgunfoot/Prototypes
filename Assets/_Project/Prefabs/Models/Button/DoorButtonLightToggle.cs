using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonLightToggle : MonoBehaviour, IHoverAction
{

    Animator anim;
    Animation clip;
    private float direction = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void HoverAction()
    {        
        RerverseAnimation();
    }

    public void RerverseAnimation()
    {        
        direction = direction * -1;
        if(direction == 1){
            
        }else{
            
        }        
        anim.SetFloat("Direction", direction);
        
    }
}
