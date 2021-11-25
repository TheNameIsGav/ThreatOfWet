using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public float accell = 1f;
    private float jumpSquat = 0f;
    private float jumpSquatVal = 3f;
    private int minJumpTime = 12;
    //private int playerController.instance.shortHop = 0;
    //private float flatten = -4f;
    private float hold = 0f;
    public float jumpHeight = 13f;
    public bool jumpRelease = false;
    //public Rigidbody2D rb;
    //private bool jumpRelease = false;
    //public Rigidbody2D rb;
    // Start is called before the first frame update
    public IdleState()
    {
        //.Log("heare");
       
        //rb = playerController.instance.rbs;
        //rb = playerController.instance.rb;

    }
    public override void OnEnter()
    {
        playerController.instance.rbs.gravityScale = playerController.instance.grav;
        eatShit = 2;
        jumpSquatVal = 3f;
    }
    public override void OnExit()
    {
        
    }
    // Update is called once per frame
    public override void Update()
    {
        ////Debug.Log.Log("ilde idle");
        hori = playerController.instance.pHori;
        vert = playerController.instance.pVert;
        jump = playerController.instance.jump;
        jumpRelease = playerController.instance.jumpRelease;
        if (playerController.instance.shortHop >= minJumpTime && jumpRelease)
        {
            ////Debug.Log.Log("if this calls I'm over it");
            hold = (-1 * playerController.instance.rbs.velocity.y);
            playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x, 3f);
            playerController.instance.shortHop = 0;
            playerController.instance.flatten = 2f;
            jumpRelease = playerController.instance.jumpRelease;
        }
    }

    public override void StateUpdate()
    {
        ////Debug.Log.Log("fixed update poser");
        if (playerController.instance.shortHop > 0)
        {
            playerController.instance.shortHop++;
        }
        if (jump)
        {
            //this is the check for wavedashing and superjumping
            
            //this is for the squash and stretch just for game feel
            if (jumpSquat > 0f)
            {
                //Debug.Log.Log("squat");
                jumpSquat--;
            }
            else
            {
                //this sets the actual jump
                jumpSquat = 0f;
                //Debug.Log.Log("up");
                if (playerController.instance.superJump)
                {
                    playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x, Mathf.Max(playerController.instance.rbs.velocity.y, 0f) + jumpHeight);
                }
                else
                {
                    playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x, 0f + jumpHeight);
                    //playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x, Mathf.Max(playerController.instance.rbs.velocity.y, 0f) + jumpHeight);
                }
            
                playerController.instance.superJump = false;
                //playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x, Mathf.Max(playerController.instance.rbs.velocity.y, 0f) + jumpHeight);
                playerController.instance.jump = false;
                //jumpBuffer = -1;

                if (playerController.instance.transform.parent == null)
                    playerController.instance.transform.localScale = (new Vector3(.9f, 1.1f, 1f));
                else
                    playerController.instance.transform.localScale = (new Vector3(.9f / (playerController.instance.transform.parent.transform.localScale.x),
                        1.1f / (playerController.instance.transform.parent.transform.localScale.y), 1f));
                
                playerController.instance.coyote = playerController.instance.universalBufferTime + 1;
            }

        }
        //this is essentially part 2 of the grounded check
        else if (playerController.instance.grounded && playerController.instance.rbs.velocity.y < 1f && playerController.instance.flatten == -4f)
        {
            if (playerController.instance.transform.parent == null)
                playerController.instance.transform.localScale = (new Vector3(1f, 1f, 1f));
            else
                playerController.instance.transform.localScale = (new Vector3(1f / (playerController.instance.transform.parent.transform.localScale.x),
                    1f / (playerController.instance.transform.parent.transform.localScale.y), 1f));

            //playerController.instance.shortHop = 0;
        }

        if (playerController.instance.flatten > -2f)
        {
            playerController.instance.flatten--;
            playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x, playerController.instance.flatten);
        }
        else if (playerController.instance.flatten == -3f)
        {
            playerController.instance.flatten = -4f;
            // jumpRelease = false;
            playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x, hold);
        }
        else
        {
            // jumpRelease = false;
        }
        if (hori == 0)
        {
            ////Debug.Log.Log("I love my own hatefull existance");
            //this is the code to stop accellerating if no input is held

            if (Mathf.Abs(playerController.instance.rbs.velocity.x) > 1f)
            {
                if (playerController.instance.grounded)
                {
                    //de-cellerate on the ground
                    playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x - (((1f) * Mathf.Sign(playerController.instance.rbs.velocity.x))), playerController.instance.rbs.velocity.y);
                }
                else
                {

                    //de-cellerate in the air
                    playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x - ((accell) * Mathf.Sign(playerController.instance.rbs.velocity.x)), playerController.instance.rbs.velocity.y);
                }
            }
            else
            {
                //stops the player once far enough along
                playerController.instance.rbs.velocity = new Vector2(0f, playerController.instance.rbs.velocity.y);
            }
        }
        else if (hori != 0)
        {
            ////Debug.Log.Log("some kinda piss wizard");
            //speed up if speed is less than speedcap
            if (Mathf.Abs(playerController.instance.rbs.velocity.x) < speedCap && (Mathf.Sign(playerController.instance.rbs.velocity.x) == Mathf.Sign(hori) || playerController.instance.rbs.velocity.x == 0f))
            {
                playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x + (accell * (Mathf.Abs(hori) / hori)), playerController.instance.rbs.velocity.y);
            }
            //instant pivot with no acceleration cooldown
            else if (playerController.instance.grounded && Mathf.Sign(playerController.instance.rbs.velocity.x) != Mathf.Sign(hori))
            {
                playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.rbs.velocity.x, playerController.instance.rbs.velocity.y);
            }
            //arial turnaround, a lot slower
            else if (Mathf.Sign(playerController.instance.rbs.velocity.x) != Mathf.Sign(hori))
            {
                playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x + ((accell) * (Mathf.Abs(hori) / hori)), playerController.instance.rbs.velocity.y);
            }
            //makes the player slow down to the speedcap if they were over it while on the ground
            else if (playerController.instance.grounded && Mathf.Abs(playerController.instance.rbs.velocity.x) > speedCap + accell && !jump)
            {
                ////Debug.Log.Log("MY OWN HUBRIS");
                playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x - (accell * Mathf.Sign(playerController.instance.rbs.velocity.x)), playerController.instance.rbs.velocity.y);
            }

        }

    }
    public override void JumpTrigger()
    {
        //playerController.instance.flatten = flat;
        ////Debug.Log.Log("WHO WANT S TO WATHC ME SCREAM");
        jumpSquat = jumpSquatVal;
        jumpRelease = false;
        if (playerController.instance.transform.parent == null)
            playerController.instance.transform.localScale = (new Vector3(1.4f, 0.8f, 1f));
        else
            playerController.instance.transform.localScale = (new Vector3(1.4f / (playerController.instance.transform.parent.transform.localScale.x),
                0.8f / (playerController.instance.transform.parent.transform.localScale.y), 1f));
        
        //jumpRelease = false;
        //playerController.instance.jump = true;
    }
  

}

public class DashState : State
{
    public float dashDist = 30f;
    private int dashBuffer = -1;
    private int dashTimer = -1;
    //these are used to control the direction the player dashes in
    private float dashx = 0f;
    private float dashy = 0f;
    private float dashDirx = 0f;
    private float dashDiry = 0f;
    //these 4 are used for the dash buffer
    private float holdx = 0f;
    private int holdxTime = 0;
    private float holdy = 0f;
    private int holdyTime = 0;
    private bool swinging = false;
    private bool swingRelease = false;
    //private bool rolling = false;
    //public Rigidbody2D rb;
    //private Rigidbody2D rb;

    public DashState()
    {
        //rb = playerController.instance.rbs;
        /*
        dashx = playerController.instance.rbs.velocity.x;
        dashy = playerController.instance.rbs.velocity.y;
        dashBuffer = 4;
        playerController.instance.rbs.gravityScale = 0;
        playerController.instance.rbs.velocity = new Vector2(0f, 0f);
        playerController.instance.transform.localScale = new Vector3(1f, 0.5f, 1f);
        */
    }

    public override void OnEnter()
    {
        swingRelease = false;
        dashx = playerController.instance.rbs.velocity.x;
        dashy = playerController.instance.rbs.velocity.y;
        dashBuffer = 4;
        playerController.instance.rbs.gravityScale = 0;
        ////Debug.Log.Log("no fucking way");
        playerController.instance.rbs.velocity = new Vector2(0f, 0f);

        // playerController.instance.transform.localScale = new Vector3(1f, 0.5f, 1f);
        if (playerController.instance.transform.parent == null)
            playerController.instance.transform.localScale = (new Vector3(1f, 0.5f, 1f));
        else
            playerController.instance.transform.localScale = (new Vector3(1f / (playerController.instance.transform.parent.transform.localScale.x),
                0.5f / (playerController.instance.transform.parent.transform.localScale.y), 1f));

        playerController.instance.coyote = playerController.instance.universalBufferTime;
        playerController.instance.superJump = true;
    }
    public override void OnExit()
    {
        swinging = false;
        swingRelease = false;
        playerController.instance.distJoint.enabled = false;
        playerController.instance.lineRender.enabled = false;
        //playerController.instance.canDash = false;
    }
    // Update is called once per frame
    public override void Update()
    {
        hori = playerController.instance.pHori;
        vert = playerController.instance.pVert;
        jump = playerController.instance.jump;
        if (hori != 0)
        {
            holdx = hori;
            holdxTime = 0;
        }
        else if (holdxTime < universalBufferTime)
        {
            holdxTime++;
        }
        else
        {
            holdx = 0f;
            holdxTime = 0;
        }
        //input buffer for vertical axis, used for dashes
        if (vert != 0)
        {
            holdy = vert;
            holdyTime = 0;
        }
        else if (holdyTime < universalBufferTime)
        {
            holdyTime++;
        }
        else
        {
            holdy = 0f;
            holdyTime = 0;
        }
        if (!Input.GetButton("Dash"))
        {
            swingRelease = true;
        }
        if (playerController.instance.lineRender.enabled)
        {
            playerController.instance.lineRender.SetPosition(1, playerController.instance.transform.position);
        }

    }

    public override void StateUpdate()
    {
        if (jump)
        {
            ////Debug.Log.Log("superspeed we go again");
            playerController.instance.rbs.gravityScale = playerController.instance.grav;
            //playerController.instance.state = new IdleState();
            playerController.instance.jumpRelease = false;
            //playerController.instance.jump = false;
            dashTimer = -1;
            
            // playerController.instance.transform.localScale = new Vector3(0.9f, 1.1f, 1f);

            if (playerController.instance.transform.parent == null)
                playerController.instance.transform.localScale = (new Vector3(.9f, 1.1f, 1f));
            else
                playerController.instance.transform.localScale = (new Vector3(.9f / (playerController.instance.transform.parent.transform.localScale.x),
                    1.1f / (playerController.instance.transform.parent.transform.localScale.y), 1f));
            
            //this is for if the player is wavedashing rather than superjumping, also sorry not sorry
            if (playerController.instance.rbs.velocity.y <= 0)
            {
                //playerController.instance.state.JumpTrigger();
                playerController.instance.flatten = -4f;
                playerController.instance.shortHop = 1;
                playerController.instance.canDash = true;
            }
            else
            {
                playerController.instance.flatten = -5f;
                ////Debug.Log.Log("hell cite hell cite");
                playerController.instance.canDash = false;
                //playerController.instance.state.JumpTrigger(-5);
                playerController.instance.shortHop = 0;
                
            }
            //Debug.Log("why grapps here");
            playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x + Mathf.Sign(dashx)*Mathf.Max(Mathf.Abs(dashx) - speedCap,0f), playerController.instance.rbs.velocity.y);
            //playerController.instance.shortHop = 1;
            //playerController.instance.flatten = -4f;
            playerController.instance.ChangeState(playerController.instance.idle);
            ////Debug.Log.Log("do I exist");
            playerController.instance.state.JumpTrigger();
            //playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x + dashx, Mathf.Max(jumpHeight, Mathf.Min(playerController.instance.rbs.velocity.y, jumpHeight * 2f)));
        }
        if (dashBuffer > 0)
        {
            dashBuffer--;
        }
        else if (dashBuffer == 0)
        {
           
            dashBuffer = -1;
            dashTimer = 10;
            if (playerController.instance.nDash) {
                if (playerController.instance.rolling)
                {
                    playerController.instance.rbs.gravityScale = playerController.instance.grav * 3;
                }
                if (holdx != 0)
            {
                if (holdy != 0)
                {
                    //dashing in a diagonal
                    dashDirx = Mathf.Sign(holdx);
                    dashDiry = Mathf.Sign(holdy);
                    playerController.instance.rbs.velocity = new Vector2((Mathf.Abs(holdx) / holdx) * dashDist * (1f / Mathf.Sqrt(2)), (Mathf.Abs(holdy) / holdy) * dashDist * (1f / Mathf.Sqrt(2)));
                }
                else
                {
                    //dashing horisonzally left or right
                    dashDirx = Mathf.Sign(holdx);
                    dashDiry = 0f;
                    playerController.instance.rbs.velocity = new Vector2((Mathf.Abs(holdx) / holdx) * dashDist, 0);
                }
            }
            else
            {
                ////Debug.Log.Log("HOW HOW HOW");
                if (holdy != 0)
                {
                    //dashing vertically
                    dashDirx = 0f;
                    dashDiry = Mathf.Sign(holdy);
                    playerController.instance.rbs.velocity = new Vector2(0, (Mathf.Abs(holdy) / holdy) * dashDist);
                }
                else
                {
                    //dashing in place
                    dashDirx = playerController.instance.dir;
                    dashDiry = 0f;
                    ////Debug.Log.Log("HOW HOW HOW");
                    playerController.instance.rbs.velocity = new Vector2(playerController.instance.dir * dashDist, 0);
                }
            }
        }
            else
            {
                //Debug.Log(Mathf.Cos(Mathf.Atan2(playerController.instance.rbs.velocity.y, playerController.instance.rbs.velocity.x)).ToString() + "  " + Mathf.Sin(Mathf.Atan2(playerController.instance.rbs.velocity.y, playerController.instance.rbs.velocity.x)));
                if (playerController.instance.grapple)
                {
                    dashTimer = -1;
                    swinging = true;
                    playerController.instance.rbs.velocity = new Vector2(dashx, dashy);
                    playerController.instance.rbs.gravityScale =4* playerController.instance.grav;
                    playerController.instance.distJoint.connectedAnchor =new Vector2 (playerController.instance.transform.position.x + ((dashDist / 4) * playerController.instance.dir * (1f / Mathf.Sqrt(2))), playerController.instance.transform.position.y + ((dashDist / 4)* (1f / Mathf.Sqrt(2))));
                    playerController.instance.lineRender.SetPosition(0, playerController.instance.distJoint.connectedAnchor);
                    playerController.instance.lineRender.SetPosition(1, playerController.instance.transform.position);
                    playerController.instance.distJoint.enabled = true;
                    playerController.instance.lineRender.enabled = true;
                }
                else
                {
                    dashDirx = Mathf.Sign(holdx);
                    dashDiry = Mathf.Sign(holdy);
                    //Debug.Log(Mathf.Atan2(playerController.instance.rbs.velocity.y, playerController.instance.rbs.velocity.x));
                    playerController.instance.rbs.velocity = new Vector2(dashDist * Mathf.Cos(Mathf.Atan2(dashy, dashx)), dashDist * Mathf.Sin(Mathf.Atan2(dashy, dashx)));
                }
            }

            
        }
        //this is how long the player dashes (moves with the set dash velocity)
        if (dashTimer > 0)
        {
            //Debug.Log.Log(playerController.instance.rbs.velocity.x.ToString() + "  " + playerController.instance.rbs.velocity.y.ToString() + "  " + dashTimer.ToString());
            dashTimer--;
        }
        //this resets the player back to an idle state, turns on gravity, etc.
        else if (dashTimer == 0)
        {
            //Debug.Log.Log(playerController.instance.rbs.velocity.x.ToString() + "  " + playerController.instance.rbs.velocity.y.ToString() + "  " + dashTimer.ToString());
            playerController.instance.rbs.gravityScale = playerController.instance.grav;
            playerController.instance.canDash = false;
            playerController.instance.superJump = false;
            playerController.instance.ChangeState(playerController.instance.idle);
            dashTimer = -1;

            // playerController.instance.transform.localScale = new Vector3(0.9f, 1.1f, 1f);

            if (playerController.instance.transform.parent == null)
                playerController.instance.transform.localScale = (new Vector3(.9f, 1.1f, 1f));
            else
                playerController.instance.transform.localScale = (new Vector3(.9f / (playerController.instance.transform.parent.transform.localScale.x),
                    1.1f / (playerController.instance.transform.parent.transform.localScale.y), 1f));

            if (hori == 0)
            {
                //stops momentum if no direction
                ////Debug.Log.Log("FOE THR LOV E OF GOD");
                playerController.instance.rbs.velocity = new Vector2(0f, 0f /*Mathf.Max(0f, dashy)*/);
            }
            else if(!playerController.instance.grapple)
            {
                ////Debug.Log.Log("HOW DID I EVEN GET HERE");
                //keeps old pre-dash momentum
                playerController.instance.rbs.velocity = new Vector2((Mathf.Sign(hori) * Mathf.Abs(dashx)), 0f /*Mathf.Max(0f, dashy)*/);
            }
            else
            {

            }
        }
        else if (swinging)
        {
            if(swingRelease)
            {
                dashTimer = 0;
                swinging = false;
                playerController.instance.rbs.gravityScale = playerController.instance.grav;
                playerController.instance.distJoint.enabled = false;
                playerController.instance.lineRender.enabled = false;
            }
            else
            {
                if(hori != 0)
                {
                    playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x + (.3f*hori), playerController.instance.rbs.velocity.y);
                }
            }
        }
    }
    public override void JumpTrigger() { }
}
