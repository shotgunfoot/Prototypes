using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Movement Properties")]

    #region Movement_Properties

    public float walkSpeed = 6.0f;

    public float runSpeed = 11.0f;

    // If true, diagonal speed (when strafing + moving forward or back) can't exceed normal move speed; otherwise it's about 1.4 times faster
    public bool limitDiagonalSpeed = true;

    // If checked, the run key toggles between running and walking. Otherwise player runs if the key is held down and walks otherwise
    // There must be a button set up in the Input Manager called "Run"
    public bool toggleRun = false;

    public float jumpSpeed = 8.0f;

    public Vector3 Gravity;
    public Vector3 Orientation;

    // Small amounts of this results in bumping when walking down slopes, but large amounts results in falling too fast
    public float antiBumpFactor = .75f;

    // Player must be grounded for at least this many physics frames before being able to jump again; set to 0 to allow bunny hopping
    public int antiBunnyHopFactor = 1;

    public bool crouching = false;

    public Rigidbody rb;

    // If the player ends up on a slope which is at least the Slope Limit as set on the character controller, then he will slide down
    public bool slideWhenOverSlopeLimit = false;

    // If checked and the player is on an object tagged "Slide", he will slide down it regardless of the slope limit
    public bool slideOnTaggedObjects = false;

    public float slideSpeed = 12.0f;

    public Animator anim;

    public bool DebugView = true;

    public Transform feet;

    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;
    private CapsuleCollider capsule;
    private float speed;
    private Vector3 contactPoint;
    private int jumpTimer;
    private bool canMove = true;
    private PlayerInput pInput;
    private bool jumping;
    private Collisions collisions;    

    #endregion    

    void Start()
    {
        pInput = GetComponent<PlayerInput>();
        capsule = GetComponent<CapsuleCollider>();
        speed = walkSpeed;
        jumpTimer = antiBunnyHopFactor;
        rb = GetComponent<Rigidbody>();
    }

    #region FIXEDUPDATE

    void FixedUpdate()
    {
        float inputY;
        float inputX = inputY = 0;
        if (canMove)
        {
            inputX = pInput.MovementInput.x;
            inputY = pInput.MovementInput.y;
        }
        // If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;

        //anim.SetFloat("Forward", inputY);
        //anim.SetFloat("Left", inputX);

        if (pInput.Crouch)
        {
            crouching = !crouching;
        }

        if (grounded)
        {         

            // If running isn't on a toggle, then use the appropriate speed depending on whether the run button is down
            if (!toggleRun)
                speed = pInput.Run ? runSpeed : walkSpeed;
            
                moveDirection = new Vector3(inputX * inputModifyFactor * speed, 0, inputY * inputModifyFactor * speed);
                moveDirection = transform.TransformDirection(moveDirection);                            

            // Jump! But only if the jump button has been released and player has been grounded for a given number of frames
            // /// --- NOTE BY SION --- ///
            // This works by increasing the jumpTimer whenever the space bar isn't held down.
            // Then the else if passes when the jumpTimer is above the anti-BunnyHopFactor, this is where the jump actually happens.
            // The antiBunnyHopFactor is there to act as a buffer between being able to jump again. This is because the Jump button is being checked every frame. If this counter was 
            // 0, then the player would jump constantly.
            // /// --- NOTE BY SION --- ///
            if (!pInput.Jump)
                jumpTimer++;
            else if (jumpTimer >= antiBunnyHopFactor && !jumping)
            {
                jumping = true;
                collisions.Below = false;
                //moveDirection.y = jumpSpeed; /// THIS IS WHERE THE JUMP HAPPENS      
                //THIS NEEDS CHANGING, jump upwards should be based on wherever UP is for the players current orientation.                
                //rigidbody version
                rb.AddForce(transform.up * Mathf.Sqrt(jumpSpeed * -2f * Gravity.y), ForceMode.VelocityChange);

                /// Modified by Sion
                jumpTimer = 0;
                StartCoroutine(Jumping());
            }
        }
        else
        {
            //falling

            //air control
        }        

        // Move the controller, and set grounded true or false depending on whether we're standing on something        
        grounded = collisions.Below;

        CollisionCheck();
        Move(moveDirection);

        UpdateAnimator();
    }

    #endregion

    public void CollisionCheck()
    {
        //fire a sphere collider at players feet, if hitting floor store in collisions.below
        //collisions.Below = Physics.SphereCast(transform.position, .5f, -transform.up, out collisions.BelowHit, .5f);
        collisions.Below = Physics.Raycast(transform.position, -transform.up, out collisions.BelowHit, 1.2f);
    }

    public IEnumerator Jumping()
    {
        yield return new WaitForSeconds(0.5f);
        jumping = false;
    }

    /// <summary>
    /// This function takes the currently set gravity and stands the character so that its feet is going with the gravity.
    /// </summary>
    public void AlignCharacterWithGravity()
    {
        //just set this characters gravity
        //then tell the MouseLook script's targetdirection to be a direction that best fits the gravity direction.
    }

    private void Move(Vector3 moveDirection)
    {

        //if (collisions.Below)
        //{
        //    if (!jumping)
        //    {
        //        rb.velocity = Vector3.zero;
        //    }
        //}
        //else
        //{
        //    rb.AddForce(Gravity);
        //}

        rb.AddForce(Gravity);

        transform.position += moveDirection * Time.deltaTime;
        //rb.MovePosition(rb.position + moveDirection * Time.deltaTime);
        //transform.position += moveDirection;
    }


    void Update()
    {
        // If the run button is set to toggle, then switch between walk/run speed. (We use Update for this...
        // FixedUpdate is a poor place to use GetButtonDown, since it doesn't necessarily run every frame and can miss the event)
        if (toggleRun && grounded && pInput.Jump)
            speed = (speed == walkSpeed ? runSpeed : walkSpeed);
    }

    private void UpdateAnimator()
    {
        anim.SetBool("Crouch", crouching);
    }

    public struct Collisions
    {
        public RaycastHit BelowHit;
        public bool Below;
    }

    public bool InZeroGrav()
    {
        if (Gravity == Vector3.zero)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Store point that we're in contact with for use in FixedUpdate if needed
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contactPoint = hit.point;
    }

    private void OnDrawGizmos()
    {
        if (DebugView)
        {
            Gizmos.color = Color.yellow;
            //Gizmos.DrawWireSphere(collisions.BelowHit.point, .5f);
            Debug.DrawRay(transform.position, -transform.up * 1.2f);
        }
    }

    #region Getters_&_SETTERS
    //GETTERS & SETTERS
    public void DisableMovement()
    {
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void SetGravity(Vector3 grav)
    {
        Gravity = grav;
    }
    
    public void SetGravityAndRotation(Vector3 grav, Vector3 rot)
    {
        Gravity = grav;
        Orientation = rot;
        transform.rotation = Quaternion.Euler(rot);
    }

    public bool Grounded()
    {
        return grounded;
    }

    #endregion    
}