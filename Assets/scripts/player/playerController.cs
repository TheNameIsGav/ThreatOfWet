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
    public float maxHealth = 100f;
    public DashState dash = new DashState();
    public IdleState idle = new IdleState();
    public MenuState menu = new MenuState();
    public AttackState attack = new AttackState();
    public PhysicsMaterial2D go;
    public PhysicsMaterial2D stop;
    //public static AttackState attack;
    public static playerController instance;
    public Weapon rangedWeapon;
    public Weapon meleeWeapon;
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
    public State oldState;
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
    public bool combo = false;
    public bool invuln = false;
    public int comboCount = 0;
    public int comboTime = 0;
    public int hitstun = 30;
    public LineRenderer lineRender;
    public DistanceJoint2D distJoint;
    public KeyCode[] inputs;
    public int invulCount = 0;
    public bool item = false;
    public GameObject items;
    public GameObject activeItem;
    public float[] itemVals;
    public bool weapon;
    public GameObject activeChest;
    public SpriteRenderer pSprite;
    public Sprite meleeSp;
    public Sprite rangedSp;
    public bool block = false;
    public int blockTime = 0;
    // attack speed, attack damage, scaling, lifesteal, hp, def, crit, dodge, drop
    //Debug.Log(meleeWeapon.lightActive);
    // meleeWeapon.lightActive;
    private void Awake()
    {
        if (GameObject.Find("ControlSaver") != null)
        {
            //Debug.Log("helsinki");
            inputs = customControls.instance.inputLst;
            itemVals = customControls.instance.pItems;
            meleeWeapon = customControls.instance.pMelee;
            rangedWeapon = customControls.instance.pRange;
        }
        else
        {
            inputs = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space, KeyCode.I, KeyCode.E, KeyCode.O, KeyCode.P, KeyCode.L, KeyCode.Semicolon };
            itemVals = new float[] { 0f, 0f, 0f, 0f, 0f, 0f, 10f, 0f, 35f };
            meleeWeapon = new StarterSword();
            rangedWeapon = new StarterGun();

            // (up , down, left, right, jump, dash, interact, light melee, heavy melee, light range, heavy range)
        }
    }
    void Start()
    {
        instance = this;
        //Debug.Log(meleeWeapon.lightActive);
        distJoint.enabled = false;
        lineRender.enabled = false;
        state = idle;
        weaponHitbox.enabled = false;
        pHori = 0;
        pVert = 0;
        if (GameObject.Find("ControlSaver") != null)
        {
            //Debug.Log("helsinki");
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
        if (Input.GetAxis("Horizontal") != 0)
        {
            pHori = Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            pVert = -1 * Input.GetAxis("Vertical");
        }

        //pHori = Input.GetAxis("Horizontal");
        //pVert = Input.GetAxis("Vertical");
        //this initiates the jump
        if ((Input.GetKeyDown(inputs[4]) || Input.GetButtonDown("Jump") || jumpBuffer >= 0) && grounded)
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
        else if (Input.GetKeyDown(inputs[4]) || Input.GetButtonDown("Jump"))
        {
            jumpBuffer = universalBufferTime;
        }

        //this the store that the player wants to short hop
        if (!Input.GetKey(inputs[4]) && !Input.GetButton("Jump") && rbs.velocity.y > 0 && state == idle && flatten == -4f)
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
        if ((Input.GetKeyDown(inputs[5]) || dashBuffer >= 0 || Input.GetButtonDown("Dash")) && canDash)
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
        else if (Input.GetKeyDown(inputs[5]) || Input.GetButtonDown("Dash"))
        {
            dashBuffer = universalBufferTime;
        }
        //how to get a light melee input
        if (state != attack || attack.phase >= 1)
        {
            if (Input.GetKeyDown(inputs[7]) || Input.GetButtonDown("Light Melee"))
            {
                Debug.Log("lpldsdl");
                attackVal = 1;
                if (state != attack)
                {
                    ChangeState(attack);
                }

                //weaponHitbox.enabled = true;
                //weaponHitbox.transform.localScale = new Vector2(meleeWeapon.hitboxWidth * Mathf.Sign(rbs.velocity.x), meleeWeapon.hitboxHeight);
            }
            // how to get a heavy melee input
            else if (Input.GetKeyDown(inputs[8]) || Input.GetAxis("Heavy Melee") > 0)
            {
                //Debug.Log("lpldsdl");
                attackVal = 2;
                if (state != attack)
                {
                    ChangeState(attack);
                }
                //ChangeState(attack);
                //weaponHitbox.enabled = false;
                //weaponHitbox.transform.localScale = new Vector2(0.1f, .5f);
            }
            //THIS IS NOW BLOCK INPUT, WE LOVE TO SEE IT
            else if (Input.GetKeyDown(inputs[9]) || Input.GetButtonDown("Light Range"))
            {
                //Debug.Log("lpldsdl");
                //attackVal = 3;
                //if (state != attack)
                //{
                //ChangeState(attack);
                //}
                //ChangeState(attack);

                //magic number but only set here?
                blockTime = 15;
                block = true;

            }
            else
            {
                attackVal = 0;
            }
        }
        // this is also the button to pick up
        if ((Input.GetButtonDown("Interact") || Input.GetKeyDown(inputs[6])) && state != menu)
        {
            Debug.Log(itemVals[0].ToString() + " " + itemVals[1].ToString() + " " + itemVals[2].ToString());
            Debug.Log(itemVals[3].ToString() + " " + itemVals[4].ToString() + " " + itemVals[5].ToString());
            Debug.Log(itemVals[6].ToString() + " " + itemVals[7].ToString() + " " + itemVals[8].ToString());
            ChangeState(menu);
        }
        //Debug.Log(state);
        state.Update();
    }

    private void FixedUpdate()
    {
        if (invulCount > 0)
        {
            invulCount--;
        }
        else
        {
            invuln = false;
        }
        if(blockTime > 0 && state != attack)
        {
            blockTime--;
        }
        else
        {
            block = false;
            blockTime = 0;
        }
        if(comboTime > 0)
        {
            comboTime--;
        }
        else
        {
            comboCount = 0;
            attack.scale = 1f;
            PlayerUIScript.ScaleCombo(0);
        }
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
            rbs.velocity = new Vector2(rbs.velocity.x, 30f * Mathf.Sign(rbs.velocity.y));
        }
        if (pHori != 0)
        {
            if (pHori == 1)
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
        if (rbs.velocity.y > 1f)
        {
            coyote = universalBufferTime;
        }
        if (pHori == 0 && Mathf.Abs(rbs.velocity.x) < 2f)
        {
            //Debug.Log("tokeyo no drift");
            //rbs.velocity = new Vector2(0f, rbs.velocity.y);
        }
        animator.SetFloat("Speed", Mathf.Abs(rbs.velocity.x));
        if (health < 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ChangeState(State newState)
    {
        if (newState != menu)
        {
            Debug.Log("call exit");
            state.OnExit();
        }
        oldState = state;
        state = newState;
        if (oldState != menu)
        {
            Debug.Log("call enter");
            state.OnEnter();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this makes dashing into a wall / ground feel better than it did before
        if (state == dash)
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
        float rand = Random.Range(1, 101);
        if (itemVals[7] <= rand)
        {   
                if (change < 1)
                {
                    if (!block && !invuln)
                    {
                        health += Mathf.Max(change + itemVals[5], 0f);
                        invuln = true;
                        invulCount = 25;
                    }
                }
                else
                {
                    health += change;
                }
            }
            if (health > (maxHealth + itemVals[4]))
            {
                health = (maxHealth + itemVals[4]);
            }
            //GameObject.Find("PlayerUI").GetComponent<ComboCounter>().UpdatePlayerHealthBar(health, maxHealth + itemVals[4]);
        }
    
}
