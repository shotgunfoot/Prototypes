using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private float speed = 8;
    [SerializeField] private float topSpeed = 16;
    [SerializeField] private float speedDecay = .8f;
    [SerializeField] private bool engine = false;        
    [SerializeField] private Vector3 velocity = new Vector3();
    
    public void ToggleEngine()
    {
        engine = !engine;
    }

    private void FixedUpdate()
    {
        if (engine)
        {            
            if(velocity.magnitude < topSpeed)
                velocity -= transform.forward * speed * Time.deltaTime;
        }
        else
        {
            velocity *= speedDecay;
        }

        transform.position += velocity * Time.deltaTime;
    }
}
