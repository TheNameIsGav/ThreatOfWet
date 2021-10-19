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
    private int grav = 2;
    private int dashTimer = -1;
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
        if (!dashing) { 
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumpSquat = jumpSquatVal;
            jump = true;
            transform.localScale = (new Vector3(1.4f, 1.8f, 1f));
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpHeight);
        }
        //Debug.Log(rb.velocity.y.ToString());

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            hold = (-1 * rb.velocity.y);
            rb.velocity = new Vector2(rb.velocity.x, 3f);
            flatten = 2f;
        }
        if (Input.GetButtonDown("Dash") && canDash)
        {
            dashing = true;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0f, 0f);
            dashBuffer = 3;
                transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

      
    }
    private void FixedUpdate()
    {
        if (jump)
        {
            if (jumpSquat > 0f)
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
        else if (grounded && rb.velocity.y < 1f)
        {
            transform.localScale = (new Vector3(1f, 2f, 1f));
            canDash = true;
        }
        if (dashing)
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
                        rb.velocity = new Vector2((Mathf.Abs(hori) / hori) * dashDist, (Mathf.Abs(vert) / vert) * dashDist);
                    }
                    else
                    {
                        rb.velocity = new Vector2((Mathf.Abs(hori) / hori) * dashDist, 0);
                    }
                }
                else
                {
                    if(vert != 0)
                    {
                        rb.velocity = new Vector2(0, (Mathf.Abs(vert) / vert) * dashDist);
                    }
                    else
                    {
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
                dashTimer = -1;
                transform.localScale = new Vector3(0.9f, 2.1f, 1f);
            }
        }
        else
        {
            if (flatten > -2f)
            {
                flatten--;
                rb.velocity = new Vector2(rb.velocity.x, flatten);
                Debug.Log("flaots");
            }
            else if (flatten == -3f)
            {
                flatten = -4f;
                rb.velocity = new Vector2(rb.velocity.x, hold);
            }
            if (hori != 0)
            {

                if (Mathf.Abs(rb.velocity.x) < speedCap && (Mathf.Sign(rb.velocity.x) == Mathf.Sign(hori) || rb.velocity.x == 0f))
                {
                    rb.velocity = new Vector2(rb.velocity.x + (accell * (Mathf.Abs(hori) / hori)), rb.velocity.y);
                }
                else if (grounded && Mathf.Sign(rb.velocity.x) != Mathf.Sign(hori))
                {
                    rb.velocity = new Vector2(-1 * rb.velocity.x, rb.velocity.y);
                }
                else if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(hori))
                {
                    rb.velocity = new Vector2(rb.velocity.x + ((accell) * (Mathf.Abs(hori) / hori)), rb.velocity.y);
                }
            }
            else
            {
                if (Mathf.Abs(rb.velocity.x) > 0.5f)
                {
                    if (grounded)
                    {
                        rb.velocity = new Vector2(rb.velocity.x - ((accell) * Mathf.Sign(rb.velocity.x)), rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x - ((accell / 2) * Mathf.Sign(rb.velocity.x)), rb.velocity.y);
                    }
                }
                else
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                }
            }
        }
    }
}
