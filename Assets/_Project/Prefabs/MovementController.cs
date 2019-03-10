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

    // Units that player can fall before a falling damage function is run. To disable, type "infinity" in the inspector
    public float fallingDamageThreshold = 10.0f;

    // If the player ends up on a slope which is at least the Slope Limit as set on the character controller, then he will slide down
    public bool slideWhenOverSlopeLimit = false;

    // If checked and the player is on an object tagged "Slide", he will slide down it regardless of the slope limit
    public bool slideOnTaggedObjects = false;

    public float slideSpeed = 12.0f;

    // If checked, then the player can change direction while in the air
    public bool airControl = false;

    // Small amounts of this results in bumping when walking down slopes, but large amounts results in falling too fast
    public float antiBumpFactor = .75f;

    // Player must be grounded for at least this many physics frames before being able to jump again; set to 0 to allow bunny hopping
    public int antiBunnyHopFactor = 1;

    public Transform Feet;

    public bool crouching = false;

    Collisions collisions;

    public Animator anim;

    public bool DebugView = true;

    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;
    private CapsuleCollider capsule;
    private float speed;
    private RaycastHit hit;
    private float fallStartLevel;
    private bool falling;
    private float slideLimit;
    private float rayDistance;
    private Vector3 contactPoint;
    private bool playerControl = false;
    private int jumpTimer;
    private bool canMove = true;
    private PlayerInput pInput;

    #endregion    

    void Start()
    {
        pInput = GetComponent<PlayerInput>();
        capsule = GetComponent<CapsuleCollider>();
        speed = walkSpeed;
        rayDistance = capsule.height * .5f + capsule.radius;
        slideLimit = 45 - .1f;
        jumpTimer = antiBunnyHopFactor;
    }

    public void AddForce(Vector3 forceDirection)
    {
        moveDirection = forceDirection;
    }

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
            bool sliding = false;
            // See if surface immediately below should be slid down. We use this normally rather than a ControllerColliderHit point,
            // because that interferes with step climbing amongst other annoyances
            if (Physics.Raycast(transform.position, -transform.up, out hit, rayDistance))
            {
                if (Vector3.Angle(hit.normal, transform.up) > slideLimit)
                    sliding = true;
            }
            // However, just raycasting straight down from the center can fail when on steep slopes
            // So if the above raycast didn't catch anything, raycast down from the stored ControllerColliderHit point instead
            else
            {
                Physics.Raycast(contactPoint + transform.up, -transform.up, out hit);
                if (Vector3.Angle(hit.normal, transform.up) > slideLimit)
                    sliding = true;
            }

            // If running isn't on a toggle, then use the appropriate speed depending on whether the run button is down
            if (!toggleRun)
                speed = pInput.Run ? runSpeed : walkSpeed;

            // If sliding (and it's allowed), or if we're on an object tagged "Slide", get a vector pointing down the slope we're on
            if ((sliding && slideWhenOverSlopeLimit) || (slideOnTaggedObjects && hit.collider.tag == "Slide"))
            {
                Vector3 hitNormal = hit.normal;
                moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                Vector3.OrthoNormalize(ref hitNormal, ref moveDirection);
                moveDirection *= slideSpeed;
                playerControl = false;
            }
            // Otherwise recalculate moveDirection directly from axes, adding a bit of -y to avoid bumping down inclines
            else
            {
                moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor); // we should add the antibumpfactor, but it                                 
                moveDirection = transform.TransformDirection(moveDirection) * speed;
                playerControl = true;
            }

            // Jump! But only if the jump button has been released and player has been grounded for a given number of frames
            // /// --- NOTE BY SION --- ///
            // This works by increasing the jumpTimer whenever the space bar isn't held down.
            // Then the else if passes when the jumpTimer is above the anti-BunnyHopFactor, this is where the jump actually happens.
            // The antiBunnyHopFactor is there to act as a buffer between being able to jump again. This is because the Jump button is being checked every frame. If this counter was 
            // 0, then the player would jump constantly.
            // /// --- NOTE BY SION --- ///
            if (!pInput.Jump)
                jumpTimer++;
            else if (jumpTimer >= antiBunnyHopFactor)
            {
                moveDirection.y = jumpSpeed; /// THIS IS WHERE THE JUMP HAPPENS      
                //THIS NEEDS CHANGING, jump upwards should be based on wherever UP is for the players current orientation.

                /// Modified by Sion
                jumpTimer = 0;
            }
        }
        //else
        //{
        //    // If we stepped over a cliff or something, set the height at which we started falling
        //    if (!falling)
        //    {
        //        falling = true;
        //        fallStartLevel = transform.position.y;
        //    }

        //    // If air control is allowed, check movement but don't touch the y component
        //    if (airControl && playerControl)
        //    {
        //        moveDirection.x = inputX * speed * inputModifyFactor;
        //        moveDirection.z = inputY * speed * inputModifyFactor;
        //        moveDirection = transform.TransformDirection(moveDirection);
        //    }
        //}
        
        // Move the controller, and set grounded true or false depending on whether we're standing on something        
        grounded = collisions.Below;

        Move(moveDirection * Time.deltaTime);
        moveDirection = Vector3.zero;
        UpdateAnimator();
        UpdateCollisions();        
    }


    public void UpdateCollisions()
    {
        collisions.Below = Physics.SphereCast(transform.position, 0.2f, -transform.up, out collisions.BelowHit, 1f);
        Debug.Log(collisions.Below);
    }


    private void OnDrawGizmos()
    {
        if (DebugView)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + -transform.up * 1f, .2f);
        }
    }

    public struct Collisions
    {
        public RaycastHit BelowHit;
        public bool Below;
    }

    private void Move(Vector3 moveDirection)
    {                      

        if (collisions.Below)
        {
            //dont add gravity because the player is now on the ground.
            Quaternion rotCur = Quaternion.FromToRotation(transform.up, collisions.BelowHit.normal) * transform.rotation;
            transform.rotation = rotCur;
        }
        else
        {
            moveDirection -= Gravity * Time.deltaTime;
        }
        Debug.Log(Gravity);
        transform.position += moveDirection;
    }


    private void UpdateAnimator()
    {
        anim.SetBool("Crouch", crouching);
    }

    void Update()
    {
        // If the run button is set to toggle, then switch between walk/run speed. (We use Update for this...
        // FixedUpdate is a poor place to use GetButtonDown, since it doesn't necessarily run every frame and can miss the event)
        if (toggleRun && grounded && pInput.Jump)
            speed = (speed == walkSpeed ? runSpeed : walkSpeed);
    }

    // Store point that we're in contact with for use in FixedUpdate if needed
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contactPoint = hit.point;
    }

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

    public bool Grounded()
    {
        return grounded;
    }
}
