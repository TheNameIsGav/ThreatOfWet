using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rbs;
    public float pVert;
    public float pHori;
    public float flatten = -4;
    public static playerController instance;
    public Weapon rangedWeapon = new StarterSword();
    public Weapon meleeWeapon = new StarterSword();
    public Animator animator;
    public SpriteRenderer weaponHitbox;
    public int grav = 3;
    public bool grounded;
    public bool canDash;
    private int jumpBuffer = -1;
    private int dashBuffer = -1;
    private float absMax = 45f;
    public State state;
    private int universalBufferTime = 4;
    public bool jumpRelease = false;
    public bool jump = false;
    void Start()
    {
        instance = this;
        state = new IdleState();
        pHori = 0;
        pVert = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //gets the inputs
        pHori = Input.GetAxis("Horizontal");
        pVert = Input.GetAxis("Vertical");
        //this initiates the jump
        if ((Input.GetButtonDown("Jump") || jumpBuffer >= 0) && grounded)
        {
            //jumpSquat = jumpSquatVal;
            jump = true;
            jumpBuffer = -1;
            if(State.ReferenceEquals(playerController.instance.state.GetType(), new IdleState()))
            {
                state.JumpTrigger(-4f);
            }
            //shortHop = 1;
            //flatten = -4f;
            //transform.localScale = (new Vector3(1.4f, 0.8f, 1f));
            //jumpRelease = false;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            jumpBuffer = universalBufferTime;
        }

        //this the store that the player wants to short hop
        if (!Input.GetButton("Jump") && rbs.velocity.y > 0 && State.ReferenceEquals(state.GetType(),new IdleState()) && flatten == -4f)
        {
            jumpRelease = true;
        }
        //this actually initaites the shorthop

       /*
        if (shortHop >= minJumpTime && jumpRelease)
        {
            hold = (-1 * rb.velocity.y);
            rb.velocity = new Vector2(rb.velocity.x, 3f);
            shortHop = 0;
            flatten = 2f;
            jumpRelease = false;
        }
        */
        //this is the check for starting a dash
        if ((Input.GetButtonDown("Dash") || dashBuffer >= 0) && canDash)
        {
            state = new DashState();

            //rb.gravityScale = 0;
            //dashx = rb.velocity.x;
            //dashy = rb.velocity.y;
            canDash = false;
            //rb.velocity = new Vector2(0f, 0f);
            dashBuffer = -1;
            //transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
        else if (Input.GetButtonDown("Dash"))
        {
            dashBuffer = universalBufferTime;
        }
        //how to get a light melee input
        if (Input.GetButtonDown("Light Melee"))
        {
            weaponHitbox.enabled = true;
            weaponHitbox.transform.localScale = new Vector2(meleeWeapon.hitboxWidth * Mathf.Sign(rbs.velocity.x), meleeWeapon.hitboxHeight);
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
        //Debug.Log(state);
        state.Update();
    }

    private void FixedUpdate()
    {
        //the countdown timer for the jump buffer
        if (jumpBuffer >= 0)
        {
            jumpBuffer--;
        }
        //the countdownTimer for the dash buffer
        if (dashBuffer >= 0)
        {
            dashBuffer--;
        }
        //THE BIG CALL 
        //Debug.Log("fixed call mother");
        state.StateUpdate();

        //this is universal animation and no clip stuff
        if (Mathf.Abs(rbs.velocity.x) > absMax)
        {
            rbs.velocity = new Vector2(absMax * Mathf.Sign(rbs.velocity.x), rbs.velocity.y);
        }
        if (Mathf.Abs(rbs.velocity.y) > absMax)
        {
            rbs.velocity = new Vector2(rbs.velocity.x, absMax * Mathf.Sign(rbs.velocity.y));
        }
        if (rbs.velocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        animator.SetFloat("Speed", Mathf.Abs(rbs.velocity.x));
    }
}
