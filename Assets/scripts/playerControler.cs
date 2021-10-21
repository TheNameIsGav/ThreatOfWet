using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{
    public bool grounded = false;
    public float jumpHeight;
    public Rigidbody2D rb;
    public static playerControler instance;
    private float flatten = -4f;
    private float hold = 0f;
    public float speedCap;
    public float accell;
    private float hori = 0f;
    private float vert = 0f;
    private float jumpSquat = 0f;
    private float jumpSquatVal = 3f;
    private bool jump = false;
    private bool canDash = false;
    private bool dashing = false;
    public float dashDist;
    private int dashBuffer = -1;
    private int grav = 3;
    private int dashTimer = -1;
    private float dashx = 0f;
    private float dashy = 0f;
    private float dashDirx = 0f;
    private float dashDiry = 0f;
    private bool desync = false;
    private float collx = 0f;
    private float colly = 0f;
    enum States {dash, idle, attack}
    private States state = States.idle;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumpSquat = jumpSquatVal;
            jump = true;
            flatten = -4f;
            transform.localScale = (new Vector3(1.4f, 1.8f, 1f));
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpHeight);
        }
        
        //this is for the short hop
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0 && !dashing && flatten == -4f)
        {
            hold = (-1 * rb.velocity.y);
            rb.velocity = new Vector2(rb.velocity.x, 3f);
            flatten = 2f;
        }
        //this is the check for starting a dash
        if (Input.GetButtonDown("Dash") && canDash)
        {
            dashing = true;
            state = States.dash;
            rb.gravityScale = 0;
            dashx = rb.velocity.x;
            dashy = rb.velocity.y;
            canDash = false;
            rb.velocity = new Vector2(0f, 0f);
            dashBuffer = 3;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    
    }
    private void FixedUpdate()
    {
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
                dashTimer = -1;
                flatten = -5f;
                transform.localScale = new Vector3(0.9f, 2.1f, 1f);
                if(rb.velocity.x == 0)
                {
                    flatten = -4f;
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
                transform.localScale = (new Vector3(.9f, 2.1f, 1f));
            }

        }
        //this is essentially part 2 of the grounded check
        else if (grounded && rb.velocity.y < 1f && state != States.dash)
        {
            transform.localScale = (new Vector3(1f, 2f, 1f));
            canDash = true;
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
                dashBuffer = -1;
                if (hori != 0)
                {
                    if(vert != 0)
                    {
                        //dashing in a diagonal
                        dashDirx = Mathf.Sign(hori);
                        dashDiry = Mathf.Sign(vert);
                        rb.velocity = new Vector2((Mathf.Abs(hori) / hori) * dashDist * (1f / Mathf.Sqrt(2)), (Mathf.Abs(vert) / vert) * dashDist * (1f / Mathf.Sqrt(2)));
                    }
                    else
                    {
                        //dashing horisonzally left or right
                        dashDirx = Mathf.Sign(hori);
                        dashDiry = 0f;
                        rb.velocity = new Vector2((Mathf.Abs(hori) / hori) * dashDist, 0);
                    }
                }
                else
                {
                    if(vert != 0)
                    {
                        //dashing vertically
                        dashDirx = 0f;
                        dashDiry = Mathf.Sign(vert);
                        rb.velocity = new Vector2(0, (Mathf.Abs(vert) / vert) * dashDist);
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
                transform.localScale = new Vector3(0.9f, 2.1f, 1f);
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
                rb.velocity = new Vector2(rb.velocity.x, hold);
            }
            if (hori == 0)
            {
                //this is the code to stop accellerating if no input is held
                desync = true;
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
                else if(grounded && Mathf.Abs(rb.velocity.x) > speedCap + accell)
                {
                    rb.velocity = new Vector2(rb.velocity.x - (accell * Mathf.Sign(rb.velocity.x)), rb.velocity.y);
                }
                
            }
        }
       // Debug.Log(rb.velocity.x.ToString() + "  " + rb.velocity.y.ToString());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("col");
        if(state == States.dash)
        {
            rb.velocity = new Vector2(rb.velocity.x * 2, rb.velocity.y * 2);
        }
    }
}
