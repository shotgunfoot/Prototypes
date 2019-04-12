//using system.collections;
//using system.collections.generic;
//using unityengine;

//public class movementcontroller : monobehaviour
//{
//    [header("movement properties")]

//    #region movement_properties

//    public float walkspeed = 6.0f;

//    public float runspeed = 11.0f;

//     if true, diagonal speed (when strafing + moving forward or back) can't exceed normal move speed; otherwise it's about 1.4 times faster
//    public bool limitdiagonalspeed = true;

//     if checked, the run key toggles between running and walking. otherwise player runs if the key is held down and walks otherwise
//     there must be a button set up in the input manager called "run"
//    public bool togglerun = false;

//    public float jumpspeed = 8.0f;

//    public vector3 gravity;

//     small amounts of this results in bumping when walking down slopes, but large amounts results in falling too fast
//    public float antibumpfactor = .75f;

//     player must be grounded for at least this many physics frames before being able to jump again; set to 0 to allow bunny hopping
//    public int antibunnyhopfactor = 1;

//    public bool crouching = false;

//    public rigidbody rb;

//    collisions collisions;

//    public animator anim;

//    public bool debugview = true;

//    public transform feet;

//    private vector3 movedirection = vector3.zero;
//    private bool grounded = false;
//    private capsulecollider capsule;
//    private float speed;
//    private vector3 contactpoint;
//    private int jumptimer;
//    private bool canmove = true;
//    private playerinput pinput;
//    private bool jumping;

//    #endregion    

//    void start()
//    {
//        pinput = getcomponent<playerinput>();
//        capsule = getcomponent<capsulecollider>();
//        speed = walkspeed;
//        jumptimer = antibunnyhopfactor;
//        rb = getcomponent<rigidbody>();
//    }

//    #region fixedupdate

//    void fixedupdate()
//    {
//        float inputy;
//        float inputx = inputy = 0;
//        if (canmove)
//        {
//            inputx = pinput.movementinput.x;
//            inputy = pinput.movementinput.y;
//        }
//         if both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
//        float inputmodifyfactor = (inputx != 0.0f && inputy != 0.0f && limitdiagonalspeed) ? .7071f : 1.0f;

//        anim.setfloat("forward", inputy);
//        anim.setfloat("left", inputx);

//        if (pinput.crouch)
//        {
//            crouching = !crouching;
//        }

//        if (grounded)
//        {

//            movedirection = new vector3(inputx * inputmodifyfactor * speed, -antibumpfactor, inputy * inputmodifyfactor * speed);
//            movedirection = transform.transformdirection(movedirection);

//             if running isn't on a toggle, then use the appropriate speed depending on whether the run button is down
//            if (!togglerun)
//                speed = pinput.run ? runspeed : walkspeed;

//             jump! but only if the jump button has been released and player has been grounded for a given number of frames
//             / --- note by sion --- ///
//             this works by increasing the jumptimer whenever the space bar isn't held down.
//             then the else if passes when the jumptimer is above the anti-bunnyhopfactor, this is where the jump actually happens.
//             the antibunnyhopfactor is there to act as a buffer between being able to jump again. this is because the jump button is being checked every frame. if this counter was 
//             0, then the player would jump constantly.
//             / --- note by sion --- ///
//            if (!pinput.jump)
//                jumptimer++;
//            else if (jumptimer >= antibunnyhopfactor && !jumping)
//            {
//                jumping = true;
//                collisions.below = false;
//                movedirection.y = jumpspeed; /// this is where the jump happens      
//                this needs changing, jump upwards should be based on wherever up is for the players current orientation.                
//                rigidbody version
//                rb.addforce(transform.up * mathf.sqrt(jumpspeed * -2f * gravity.y), forcemode.velocitychange);

//                / modified by sion
//                jumptimer = 0;
//                startcoroutine(jumping());
//            }
//        }

//         move the controller, and set grounded true or false depending on whether we're standing on something        
//        grounded = collisions.below;

//        collisioncheck();
//        move(movedirection);

//        updateanimator();
//    }

//    #endregion

//    public void collisioncheck()
//    {
//        fire a sphere collider at players feet, if hitting floor store in collisions.below
//        collisions.below = physics.spherecast(transform.position, .5f, -transform.up, out collisions.belowhit, .5f);
//    }

//    public ienumerator jumping()
//    {
//        yield return new waitforseconds(0.5f);
//        jumping = false;
//    }

//    / <summary>
//    / this function takes the currently set gravity and stands the character so that its feet is going with the gravity.
//    / </summary>
//    public void aligncharacterwithgravity()
//    {
//        just set this characters gravity
//        then tell the mouselook script's targetdirection to be a direction that best fits the gravity direction.
//    }

//    private void move(vector3 movedirection)
//    {

//        if (collisions.below)
//        {
//            if (!jumping)
//            {
//                rb.velocity = vector3.zero;
//            }
//        }
//        else
//        {
//            rb.addforce(gravity);
//        }

//        rb.addforce(gravity);

//        transform.position += movedirection * time.deltatime;
//        rb.moveposition(rb.position + movedirection * time.deltatime);
//        transform.position += movedirection;
//    }


//    void update()
//    {
//         if the run button is set to toggle, then switch between walk/run speed. (we use update for this...
//         fixedupdate is a poor place to use getbuttondown, since it doesn't necessarily run every frame and can miss the event)
//        if (togglerun && grounded && pinput.jump)
//            speed = (speed == walkspeed ? runspeed : walkspeed);
//    }

//    private void updateanimator()
//    {
//        anim.setbool("crouch", crouching);
//    }

//    public struct collisions
//    {
//        public raycasthit belowhit;
//        public bool below;
//    }

//    public bool inzerograv()
//    {
//        if (gravity == vector3.zero)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//     store point that we're in contact with for use in fixedupdate if needed
//    void oncontrollercolliderhit(controllercolliderhit hit)
//    {
//        contactpoint = hit.point;
//    }

//    private void ondrawgizmos()
//    {
//        if (debugview)
//        {
//            gizmos.color = color.yellow;
//            gizmos.drawwiresphere(collisions.belowhit.point, .5f);
//        }
//    }

//    #region getters_&_setters
//    getters & setters
//    public void disablemovement()
//    {
//        canmove = false;
//    }

//    public void enablemovement()
//    {
//        canmove = true;
//    }

//    public void setgravity(vector3 grav)
//    {
//        gravity = grav;
//    }

//    public bool grounded()
//    {
//        return grounded;
//    }

//    #endregion    
//}
