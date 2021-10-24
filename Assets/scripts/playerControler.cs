using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer weaponHitbox;
    public Weapon rangedWeapon = new StarterSword();
    public Weapon meleeWeapon = new StarterSword();
    //these are the base jump variables
    public bool grounded = false;
    public float jumpHeight = 13f;
    //these are two global refrences for the player and others
    public Rigidbody2D rb;
    public static playerControler instance;
    //this is for the shorthop, hold is to make the fall feel good, flatten is to make a good jump arc
    private float flatten = -4f;
    private float hold = 0f;
    //the player grounded speed cap and accelleration
    public float speedCap = 13f;
    public float accell = 1f;
    //store left / right input
    private float hori = 0f;
    private float vert = 0f;
    //how long the player crouches before jumping
    private float jumpSquat = 0f;
    private float jumpSquatVal = 3f;
    //the big jump bool and what counts the jump buffer
    private bool jump = false;
    private int jumpBuffer = -1;
    //these three control the short hop / variable jump height
    private int minJumpTime = 12;
    private bool jumpRelease = false;
    private int shortHop = 0;
    //these 5 control the feel / vibe of the dash, also the distance
    private bool canDash = false;
    private bool dashing = false;
    public float dashDist = 30f;
    private int dashBuffer = -1;
    private int dashTimer = -1;
    //I had already used dash buffer like a dumbass so whoops shitty variable name
    private int realDashBuffer = -1;
    //just gravity storage variable cause it gets set to zero sometimes
    private int grav = 3;
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
    //this is for gabe's combat roll
    private bool rolling = false;
    //this is the buffer time for all variables
    private int universalBufferTime = 4;
    enum States {dash, idle, attack}
    private States state = States.idle;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        weaponHitbox.enabled = false;
        //animator.SetActive(true);
        
        hori = 0f;
        vert = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //gets the inputs
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        //this initiates the jump
        if ((Input.GetButtonDown("Jump") || jumpBuffer >= 0) && grounded)
        {
            jumpSquat = jumpSquatVal;
            jump = true;
            jumpBuffer = -1;
            shortHop = 1;
            flatten = -4f;
            transform.localScale = (new Vector3(1.4f, 0.8f, 1f));
            jumpRelease = false;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            jumpBuffer = universalBufferTime;
        }
        
        //this the store that the player wants to short hop
        if (!Input.GetButton("Jump") && rb.velocity.y > 0 && !dashing && flatten == -4f)
        {
            jumpRelease = true;
        }
        //this actually initaites the shorthop
        if(shortHop >= minJumpTime && jumpRelease)
        {
            hold = (-1 * rb.velocity.y);
            rb.velocity = new Vector2(rb.velocity.x, 3f);
            shortHop = 0;
            flatten = 2f;
            jumpRelease = false;
        }
        //this is the check for starting a dash
        if ((Input.GetButtonDown("Dash") || realDashBuffer >=0) && canDash)
        {
            dashing = true;
            state = States.dash;
            rb.gravityScale = 0;
            dashx = rb.velocity.x;
            dashy = rb.velocity.y;
            canDash = false;
            rb.velocity = new Vector2(0f, 0f);
            dashBuffer = 4;
            transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
        else if (Input.GetButtonDown("Dash"))
        {
            realDashBuffer = universalBufferTime;
        }
        //how to get a light melee input
        if (Input.GetButtonDown("Light Melee"))
        {
            weaponHitbox.enabled = true;
            weaponHitbox.transform.localScale = new Vector2(meleeWeapon.hitboxWidth * Mathf.Sign(rb.velocity.x), meleeWeapon.hitboxHeight);
        }
        // how to get a heavy melee input
        if (Input.GetButtonDown("Heavy Melee"))
        {
            weaponHitbox.enabled = false;
            weaponHitbox.transform.localScale = new Vector2(0.1f, .5f);
        }
        //how to get a light ranged input
        if (Input.GetButtonDown("Light Range"))
        {

        }
        //how to get a heavy ranged input
        if (Input.GetButtonDown("Heavy Range"))
        {

        }
        // this is also the button to pick up
        if (Input.GetButtonDown("Interact"))
        {

        }
    
    }
    private void FixedUpdate()
    {
        //the countdown timer for the jump buffer
        if(jumpBuffer >= 0)
        {
            jumpBuffer--;
        }
        //the countdownTimer for the dash buffer
        if(realDashBuffer >= 0)
        {
            realDashBuffer--;
        }
        //imput buffer for horizontal axis, used for dashes
        if(hori !=0)
        {
            holdx = hori;
            holdxTime = 0;
        }
        else if(holdxTime < universalBufferTime)
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
        //counter to enforce a minimum jump height
        if(shortHop > 0)
        {
            shortHop++;
        }
        if (jump)
        {
            //this is the check for wavedashing and superjumping
            if (state == States.dash)
            {
                Debug.Log("superspeed");
                rb.gravityScale = grav;
                dashing = false;
                state = States.idle;
                jump = false;
                jumpSquat = 0f;
                jumpBuffer = -1;
                dashTimer = -1;
                flatten = -5f;
                shortHop = 0;
                transform.localScale = new Vector3(0.9f, 1.1f, 1f);
                //this is for if the player is wavedashing rather than superjumping, also sorry not sorry
                if(rb.velocity.y <= 0)
                {
                    
                    canDash = true;
                    flatten = -4f;
                    shortHop = 1;

                }
                rb.velocity = new Vector2(rb.velocity.x + dashx, Mathf.Max(jumpHeight, Mathf.Min(rb.velocity.y, jumpHeight*2f) ));
            }
            //this is for the squash and stretch just for game feel
            else if (jumpSquat > 0f)
            {
                jumpSquat--;
            }
            else
            {
                //this sets the actual jump
                jumpSquat = 0f;
                rb.velocity = new Vector2(rb.velocity.x,Mathf.Max(rb.velocity.y,0f) + jumpHeight);
                jump = false;
                jumpBuffer = -1;
                transform.localScale = (new Vector3(.9f, 1.1f, 1f));
            }

        }
        //this is essentially part 2 of the grounded check
        else if (grounded && rb.velocity.y < 1f && state != States.dash)
        {
            transform.localScale = (new Vector3(1f, 1f, 1f));
            canDash = true;
            shortHop = 0;
        }
        //dash part of state machine
        if (state == States.dash)
        {
            //dashBuffer will be used for input buffer/game juice, game feel thing
            if(dashBuffer > 0)
            {
                dashBuffer--;
            }
            else if (dashBuffer == 0)
            {
                if (rolling)
                {
                    rb.gravityScale = grav*3;
                }
                dashBuffer = -1;
                if (holdx != 0)
                {
                    if(holdy != 0)
                    {
                        //dashing in a diagonal
                        dashDirx = Mathf.Sign(holdx);
                        dashDiry = Mathf.Sign(holdy);
                        rb.velocity = new Vector2((Mathf.Abs(holdx) / holdx) * dashDist * (1f / Mathf.Sqrt(2)), (Mathf.Abs(holdy) / holdy) * dashDist * (1f / Mathf.Sqrt(2)));
                    }
                    else
                    {
                        //dashing horisonzally left or right
                        dashDirx = Mathf.Sign(holdx);
                        dashDiry = 0f;
                        rb.velocity = new Vector2((Mathf.Abs(holdx) / holdx) * dashDist, 0);
                    }
                }
                else
                {
                    if(holdy != 0)
                    {
                        //dashing vertically
                        dashDirx = 0f;
                        dashDiry = Mathf.Sign(holdy);
                        rb.velocity = new Vector2(0, (Mathf.Abs(holdy) / holdy) * dashDist);
                    }
                    else
                    {
                        //dashing in place
                        dashDirx = 0f;
                        dashDiry = 0f;
                        rb.velocity = new Vector2(0, 0);
                    }
                }
                
                dashTimer = 10;
            }
            //this is how long the player dashes (moves with the set dash velocity)
            if(dashTimer > 0)
            {
                Debug.Log(rb.velocity.x.ToString() + "  " + rb.velocity.y.ToString() + "  " + dashTimer.ToString());
                dashTimer--;
            }
            //this resets the player back to an idle state, turns on gravity, etc.
            else if (dashTimer == 0)
            {
                Debug.Log(rb.velocity.x.ToString() + "  " + rb.velocity.y.ToString() + "  " + dashTimer.ToString());
                rb.gravityScale = grav;
                dashing = false;
                state = States.idle;
                dashTimer = -1;
                transform.localScale = new Vector3(0.9f, 1.1f, 1f);
                if (hori == 0)
                {
                    //stops momentum if no direction
                    rb.velocity = new Vector2(0f, 0f /*Mathf.Max(0f, dashy)*/);
                }
                else
                {
                    //keeps old pre-dash momentum
                    rb.velocity = new Vector2((Mathf.Sign(hori) * speedCap), 0f /*Mathf.Max(0f, dashy)*/);
                }
            }
        }
        else if(state == States.attack)
        {

        }
        else if(state == States.idle)
        {
            //flatten smooths out the short hop to make it feel better (flattens the curve in a smooth way)
            if (flatten > -2f)
            {
                flatten--;
                rb.velocity = new Vector2(rb.velocity.x, flatten);
            }
            else if (flatten == -3f)
            {
                flatten = -4f;
               // jumpRelease = false;
                rb.velocity = new Vector2(rb.velocity.x, hold);
            }
            else
            {
               // jumpRelease = false;
            }
            if (hori == 0)
            {
                //this is the code to stop accellerating if no input is held
                
                if (Mathf.Abs(rb.velocity.x) > 1f)
                {
                    if (grounded)
                    {
                        //de-cellerate on the ground
                        rb.velocity = new Vector2(rb.velocity.x - (((1f) * Mathf.Sign(rb.velocity.x))), rb.velocity.y);
                    }
                    else
                    {
                       
                       //de-cellerate in the air
                        rb.velocity = new Vector2(rb.velocity.x - ((accell) * Mathf.Sign(rb.velocity.x)), rb.velocity.y);
                    }
                }
                else
                {
                    //stops the player once far enough along
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                }
            }
            else if (hori != 0)
            {
                //speed up if speed is less than speedcap
                if (Mathf.Abs(rb.velocity.x) < speedCap && (Mathf.Sign(rb.velocity.x) == Mathf.Sign(hori) || rb.velocity.x == 0f))
                {
                    rb.velocity = new Vector2(rb.velocity.x + (accell * (Mathf.Abs(hori) / hori)), rb.velocity.y);
                }
                //instant pivot with no acceleration cooldown
                else if (grounded && Mathf.Sign(rb.velocity.x) != Mathf.Sign(hori))
                {
                    rb.velocity = new Vector2(-1 * rb.velocity.x, rb.velocity.y);
                }
                //arial turnaround, a lot slower
                else if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(hori))
                {
                    rb.velocity = new Vector2(rb.velocity.x + ((accell) * (Mathf.Abs(hori) / hori)), rb.velocity.y);
                }
                //makes the player slow down to the speedcap if they were over it while on the ground
                else if(grounded && Mathf.Abs(rb.velocity.x) > speedCap + accell)
                {
                    rb.velocity = new Vector2(rb.velocity.x - (accell * Mathf.Sign(rb.velocity.x)), rb.velocity.y);
                }
                
            }
        }
        if (Mathf.Abs(rb.velocity.x) > dashDist)
        {
            rb.velocity = new Vector2(dashDist * Mathf.Sign(rb.velocity.x), rb.velocity.y);
        }
        if (Mathf.Abs(rb.velocity.y) > dashDist)
        {
            rb.velocity = new Vector2(rb.velocity.x, dashDist * Mathf.Sign(rb.velocity.y));
        }
        if(rb.velocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this makes dashing into a wall / ground feel better than it did before
        if(state == States.dash)
        {
            rb.velocity = new Vector2(rb.velocity.x * 2, rb.velocity.y * 2);
        }
    }
}
