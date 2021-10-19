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
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumpSquat = jumpSquatVal;
            jump = true;
            transform.localScale = (new Vector3(1.4f, 1.8f, 1f));
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpHeight);
        }
        //Debug.Log(rb.velocity.y.ToString());

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0 && !dashing)
        {
            hold = (-1 * rb.velocity.y);
            rb.velocity = new Vector2(rb.velocity.x, 3f);
            flatten = 2f;
        }
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
        if(state == States.idle)
        {
            
            
        }

      
    }
    private void FixedUpdate()
    {
        if (jump)
        {
            if (state == States.dash)
            {
                Debug.Log("superspeed");
                rb.gravityScale = grav;
                dashing = false;
                state = States.idle;
                jump = false;
                jumpSquat = 0f;
                dashTimer = -1;
                transform.localScale = new Vector3(0.9f, 2.1f, 1f);
                rb.velocity = new Vector2(rb.velocity.x + dashx, rb.velocity.y + jumpHeight);
            }
            else if (jumpSquat > 0f)
            {
                jumpSquat--;
            }
            else
            {
                jumpSquat = 0f;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpHeight);
                jump = false;
                transform.localScale = (new Vector3(.9f, 2.1f, 1f));
            }

        }
        else if (grounded && rb.velocity.y < 1f && state != States.dash)
        {
            transform.localScale = (new Vector3(1f, 2f, 1f));
            canDash = true;
        }
        if (state == States.dash)
        {
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
                        dashDirx = Mathf.Sign(hori);
                        dashDiry = Mathf.Sign(vert);
                        rb.velocity = new Vector2((Mathf.Abs(hori) / hori) * dashDist * (1f / Mathf.Sqrt(2)), (Mathf.Abs(vert) / vert) * dashDist * (1f / Mathf.Sqrt(2)));
                    }
                    else
                    {
                        dashDirx = Mathf.Sign(hori);
                        dashDiry = 0f;
                        rb.velocity = new Vector2((Mathf.Abs(hori) / hori) * dashDist, 0);
                    }
                }
                else
                {
                    if(vert != 0)
                    {
                        dashDirx = 0f;
                        dashDiry = Mathf.Sign(vert);
                        rb.velocity = new Vector2(0, (Mathf.Abs(vert) / vert) * dashDist);
                    }
                    else
                    {
                        dashDirx = 0f;
                        dashDiry = 0f;
                        rb.velocity = new Vector2(0, 0);
                    }
                }
                
                dashTimer = 10;
            }
            if(dashTimer > 0)
            {
                dashTimer--;
            }
            else if (dashTimer == 0)
            {
                rb.gravityScale = grav;
                dashing = false;
                state = States.idle;
                dashTimer = -1;
                transform.localScale = new Vector3(0.9f, 2.1f, 1f);
                if (hori == 0)
                {
                    rb.velocity = new Vector2(0f, 0f /*Mathf.Max(0f, dashy)*/);
                }
                else
                {
                    rb.velocity = new Vector2(Mathf.Sign(hori) * Mathf.Max(Mathf.Abs(rb.velocity.x), Mathf.Abs(dashx)), 0f /*Mathf.Max(0f, dashy)*/);
                }
            }
        }
        else if(state == States.attack)
        {

        }
        else if(state == States.idle)
        {
            if (flatten > -2f)
            {
                flatten--;
                rb.velocity = new Vector2(rb.velocity.x, flatten);
               // Debug.Log("flaots");
            }
            else if (flatten == -3f)
            {
                flatten = -4f;
                rb.velocity = new Vector2(rb.velocity.x, hold);
            }
            if (hori == 0)
            {
                desync = true;
                //Debug.Log(hori.ToString());
                if (Mathf.Abs(rb.velocity.x) > 1f)
                {
                    if (grounded)
                    {
                        //Debug.Log("we stop now");
                        rb.velocity = new Vector2(rb.velocity.x - (((1f) * Mathf.Sign(rb.velocity.x))), rb.velocity.y);
                    }
                    else
                    {
                       // Debug.Log("lazysdf?");
                        rb.velocity = new Vector2(rb.velocity.x - ((accell) * Mathf.Sign(rb.velocity.x)), rb.velocity.y);
                    }
                }
                else
                {
                    //Debug.Log("stop");
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                }
            }
            else if (hori != 0)
            {
                //Debug.Log(hori.ToString());
                if (Mathf.Abs(rb.velocity.x) < speedCap && (Mathf.Sign(rb.velocity.x) == Mathf.Sign(hori) || rb.velocity.x == 0f))
                {
                    rb.velocity = new Vector2(rb.velocity.x + (accell * (Mathf.Abs(hori) / hori)), rb.velocity.y);
                }
                else if (grounded && Mathf.Sign(rb.velocity.x) != Mathf.Sign(hori))
                {
                    //Debug.Log("turn");
                    rb.velocity = new Vector2(-1 * rb.velocity.x, rb.velocity.y);
                }
                else if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(hori))
                {
                    rb.velocity = new Vector2(rb.velocity.x + ((accell) * (Mathf.Abs(hori) / hori)), rb.velocity.y);
                }
            }
        }
    }
}
