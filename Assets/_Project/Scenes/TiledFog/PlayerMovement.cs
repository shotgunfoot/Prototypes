using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    /// <summary>
    /// The default time between the next time the player can move a tile when holding a movement button down
    /// </summary>
    public float runTick = .3f;
    private Rigidbody2D rb;
    private float currentRunTick;

    private Vector2 input;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {        
        if(input != Vector2.zero)
        {
            if(currentRunTick > runTick)
            {
                currentRunTick = 0;
                rb.transform.position += new Vector3(input.x * .32f, input.y * .32f, 0);
            }
        }
        currentRunTick += Time.deltaTime;
    }
}
