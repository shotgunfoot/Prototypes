using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool Jump { get; private set; }
    public bool Crouch { get; private set; }
    public bool Run { get; private set; }

    // Update is called once per frame
    void Update()
    {
        MovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Crouch = Input.GetButtonDown("Crouch");
        Jump = Input.GetButtonDown("Jump");
        Run = Input.GetButton("Run");
    }
}
