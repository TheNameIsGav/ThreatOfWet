using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rbs;
    public float pVert;
    public float pHori;
    public float flatten = -4;
    public float health = 100f;
    public  DashState dash = new DashState();
    public  IdleState idle = new IdleState();
    public MenuState menu = new MenuState();
    public AttackState attack = new AttackState();
    public PhysicsMaterial2D go;
    public PhysicsMaterial2D stop;
    //public static AttackState attack;
    public static playerController instance;
    public Weapon rangedWeapon = new StarterSword();
    public Weapon meleeWeapon = new StarterSword();
    public Animator animator;
    public SpriteRenderer weaponHitbox;
    public int grav = 3;
    public bool grounded;
    public int coyote = 100;
    public bool canDash;
    private int jumpBuffer = -1;
    private int dashBuffer = -1;
    private float absMax = 45f;
    public State state;
    public int universalBufferTime = 4;
    public bool jumpRelease = false;
    public bool jump = false;
    public int shortHop = 0;
    public int attackVal = 0;
    public bool superJump = false;
    public int dir = 1;
    public bool nDash = true;
    public bool rolling = false;
    public bool grapple = false;
    public LineRenderer lineRender;
    public DistanceJoint2D distJoint;
    public KeyCode[] inputs;
    //Debug.Log(meleeWeapon.lightActive);
       // meleeWeapon.lightActive;
       
    void Start()
    {
        instance = this;
        Debug.Log(meleeWeapon.lightActive);
        distJoint.enabled = false;
        lineRender.enabled = false;
        state = idle;
        weaponHitbox.enabled = false;
        pHori = 0;
        pVert = 0;
        if(GameObject.Find("ControlSaver") != null)
        {
            Debug.Log("helsinki");
            inputs = customControls.instance.inputLst;
        }
        else
        {
            inputs = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space, KeyCode.I, KeyCode.E, KeyCode.O, KeyCode.P, KeyCode.L, KeyCode.Semicolon };
            // (up , down, left, right, jump, dash, interact, light melee, heavy melee, light range, heavy range)
        }
    }

    // Update is called once per frame
    void Update()
    {
        pHori = 0;
        pVert = 0;
        //gets the inputs
        if (Input.GetKey(inputs[0]))
        {
            pVert++;
        }
        if (Input.GetKey(inputs[1]))
        {
            pVert--;
        }
        if (Input.GetKey(inputs[2]))
        {
            pHori--;
        }
        if (Input.GetKey(inputs[3]))
        {
            pHori++;
        }

        //pHori = Input.GetAxis("Horizontal");
        //pVert = Input.GetAxis("Vertical");
        //this initiates the jump
        if ((Input.GetKeyDown(inputs[4]) || jumpBuffer >= 0) && grounded)
        {
            //jumpSquat = jumpSquatVal;
            jump = true;
            jumpBuffer = -1;
            jumpRelease = false;
            //shortHop = 1;
            if (idle == state)
            {
                //Debug.Log("WE ARE ONE I SEWEWEWE");
                shortHop = 1;
                flatten = -4f;
                
                state.JumpTrigger();
            }
            //shortHop = 1;
            //flatten = -4f;
            //transform.localScale = (new Vector3(1.4f, 0.8f, 1f));
            //jumpRelease = false;
        }
        else if (Input.GetKeyDown(inputs[4]))
        {
            jumpBuffer = universalBufferTime;
        }

        //this the store that the player wants to short hop
        if (!Input.GetKey(inputs[4]) && rbs.velocity.y > 0 && state == idle && flatten == -4f)
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
        if ((Input.GetKeyDown(inputs[5]) || dashBuffer >= 0) && canDash)
        {
            //state = new DashState();
            ChangeState(dash);
            //rb.gravityScale = 0;
            //dashx = rb.velocity.x;
            //dashy = rb.velocity.y;
            canDash = false;
            //rb.velocity = new Vector2(0f, 0f);
            dashBuffer = -1;
            //transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
        else if (Input.GetKeyDown(inputs[5]))
        {
            dashBuffer = universalBufferTime;
        }
        //how to get a light melee input
        if(state != attack || state.phase == 2)
        if (Input.GetKeyDown(inputs[7]))
        {
            attackVal = 1;
            ChangeState(attack);
            //weaponHitbox.enabled = true;
            //weaponHitbox.transform.localScale = new Vector2(meleeWeapon.hitboxWidth * Mathf.Sign(rbs.velocity.x), meleeWeapon.hitboxHeight);
        }
        // how to get a heavy melee input
        else if (Input.GetKeyDown(inputs[8]))
        {
            attackVal = 2;
            ChangeState(attack);
            //weaponHitbox.enabled = false;
            //weaponHitbox.transform.localScale = new Vector2(0.1f, .5f);
        }
        //how to get a light ranged input
        else if (Input.GetKeyDown(inputs[9]))
        {
            attackVal = 3;
            ChangeState(attack);
        }
        //how to get a heavy ranged input
        else if (Input.GetKeyDown(inputs[10]))
        {
            attackVal = 4;
            ChangeState(attack);
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
        //Debug.Log(Mathf.Atan2(playerController.instance.rbs.velocity.y, playerController.instance.rbs.velocity.x));
        if (coyote == universalBufferTime)
        {
            coyote++;
            grounded = false;
        }
        else if (coyote < universalBufferTime)
        {
            coyote++;
        }
        //Debug.Log(state.shortHop);
        //Debug.Log(flatten);
        //Debug.Log(jumpRelease);
        //the countdown timer for the jump buffer
        //Debug.Log(rbs.velocity);
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
            //Debug.Log("FUCK FUCK FUCK");
            rbs.velocity = new Vector2(absMax * Mathf.Sign(rbs.velocity.x), rbs.velocity.y);
        }
        if (Mathf.Abs(rbs.velocity.y) > 30f)
        {
            rbs.velocity = new Vector2(rbs.velocity.x,30f * Mathf.Sign(rbs.velocity.y));
        }
        if(pHori != 0)
        {
            if(pHori == 1)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
        }
        if (dir == -1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if(rbs.velocity.y > 1f)
        {
            coyote = universalBufferTime;
        }
        if(pHori == 0 && Mathf.Abs(rbs.velocity.x) < 2f)
        {
            //Debug.Log("tokeyo no drift");
            //rbs.velocity = new Vector2(0f, rbs.velocity.y);
        }
        animator.SetFloat("Speed", Mathf.Abs(rbs.velocity.x));
        if(health < 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ChangeState(State newState)
    {
        state.OnExit();
        state = newState;
        state.OnEnter();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this makes dashing into a wall / ground feel better than it did before
        if (state ==  dash)
        {
            //rbs.velocity = new Vector2(rbs.velocity.x * 2, rbs.velocity.y * 2);
        }
        if (pHori == 0 && Mathf.Abs(rbs.velocity.x) < 2f && state != dash)
        {
            //Debug.Log("tokeyo no drift");
            //rbs.sharedMaterial = stop;
            //rbs.velocity = new Vector2(0f, rbs.velocity.y);
            //rbs.velocity = new Vector2(0f,0f);
        }
        else
        {
            //rbs.sharedMaterial = go;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (pHori == 0 && Mathf.Abs(rbs.velocity.x) < 2f && state != dash)
        {
            //Debug.Log("tokeyo noerist drift");
            //rbs.sharedMaterial = stop;
            //rbs.velocity = new Vector2(0f, rbs.velocity.y);
            //rbs.velocity = new Vector2(0f,0f);
        }
        else
        {
            //rbs.sharedMaterial = go;
        }
    }

    public void InCombat()
    {
        //yo we in hitting range
    }
    public void ChangeHealth(float change)
    {
        health += change;
    }
}
