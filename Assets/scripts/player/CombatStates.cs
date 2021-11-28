using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AttackState : State
{
    // Start is called before the first frame update
    public Weapon melee;
    public Weapon ranged;
    public Weapon activeWeapon;
    private float startup;
    private float active;
    private float endlag;
    //public int phase = 0;
    private int count = 0;
    private int stun = 0;
    private float holdSpeed;
    public bool light;
    private int currVal = 0;
    private int lastVal = 0;
    public int comboCount = 0;
    public int resetVal = 50;
    public int delay = 0;
    public float scale = 1f;
    private bool water = false;
    private Queue buttons = new Queue();
    private Queue oldButtons = new Queue();
    public GameObject enemy;
    int dropCount = 0;
    bool shouldDrop = false;
    public bool dropped = false;
    public int early = 0;
    int lightFast = 0;
    public AttackState()
    {

    }
    public override void OnEnter()
    {
        //Debug.Log("we got here");
        melee = playerController.instance.meleeWeapon;
        
        //ranged = playerController.instance.rangedWeapon;
        phase = 0;
        count = 0;
        playerController.instance.weaponHitbox.enabled = false;
        playerController.instance.weaponHitbox.transform.localScale = new Vector2(0.1f, .5f);
        holdSpeed = playerController.instance.rbs.velocity.x;
        if (playerController.instance.comboTime < 1)
        {
            buttons.Clear();
            oldButtons.Clear();
        }
        //playerController.instance.rbs.gravityScale = 0f;
        SetAttack();
        if(activeWeapon.element == Element.WATER)
        {
            early = 10;
            playerController.instance.block = true;
            playerController.instance.blockTime = 10;

        }
        currVal = playerController.instance.attackVal;
        lastVal = currVal;
        playerController.instance.rbs.gravityScale = playerController.instance.grav;
        playerController.instance.rbs.sharedMaterial = playerController.instance.stop;
        playerController.instance.transform.localScale = new Vector3(1f, 1f, 1f);
        
        //playerController.instance.rbs.velocity = new Vector2(0f, 0f);
        //Debug.Log(startup);
        //Debug.Log(active);
        //Debug.Log(endlag);
    }
    public override void OnExit()
    {
       // Debug.Log("leaving the earth");
        //playerController.instance.pSprite.color = new Color(1, 1, 1, 1);
        //playerController.instance.pSprite.color = Color.white;
        playerController.instance.weaponHitbox.enabled = false;
        playerController.instance.weaponHitbox.transform.localScale = new Vector2(0.1f, .5f);
        playerController.instance.rbs.gravityScale = playerController.instance.grav;
        playerController.instance.rbs.sharedMaterial = playerController.instance.go;
        //playerController.instance.weaponHitbox.enabled = false;
        //playerController.instance.combo = false;
        //water = false;
        //playerController.instance.comboCount = 0;
        lastVal = 0;
        scale = 1f;
        phase = 0;
        count = 0;
        lightFast = 0;
        //GameObject.Find("PlayerUI").GetComponent<ComboCounter>().AdjustComboCounter(0, 0);
        //playerController.instance.weaponHitbox.transform.localScale = new Vector2(0.1f, .5f);
        playerController.instance.transform.localScale = new Vector3(1f, 1f, 1f);
        
    }
    // Update is called once per frame
    public override void Update()
    {/*
        if (currVal != playerController.instance.attackVal && playerController.instance.attackVal != 0)
        {
            SetAttack();
            //Debug.Log("shuld be zero");
            currVal = playerController.instance.attackVal;
            buttons.Enqueue(currVal);
            if(lastVal != currVal)
            {
                scale += .02f + playerController.instance.itemVals[2];
            }
            lastVal = currVal;
        }
        else if (playerController.instance.attackVal == 0)
        {
            currVal = 0;
        }
        */
    }
    public override void StateUpdate()
    {
        if(early > 0)
        {
            early--;
        }
        if (playerController.instance.jump)
        {
            playerController.instance.flatten = -4f;
            playerController.instance.shortHop = 1;
            playerController.instance.canDash = true;
            playerController.instance.jumpRelease = false;
            playerController.instance.ChangeState(playerController.instance.idle);
            playerController.instance.state.JumpTrigger();
        }
        //this is so player doesn't get dropped combo for killing enemy
        //
        //GIVE TO GABE TO HAVE ENEMY DROP ITEM ON DEATH
        //in callable function now, should be easy for gabe

        //this is the code for the enders
        if (false)
        {

        }
        else
        {
            //this is the start up on the attack
            if (phase == 0)
            {
                playerController.instance.transform.localScale = new Vector3(.8f, 1.2f, 1f);
                //this enables the attack hitbox
                if (count == Mathf.Max(3, (startup - (int) playerController.instance.itemVals[1]) - lightFast ))
                {
                    phase++;
                    count = 0;

                    playerController.instance.weaponHitbox.enabled = true;
                    playerController.instance.weaponHitbox.transform.localScale = new Vector2(activeWeapon.hitboxWidth * Mathf.Sign(playerController.instance.dir), activeWeapon.hitboxHeight);
                }
                else
                {
                    //Debug.Log("coudnd");
                    count++;
                }
            }
            //these are the active frames for the attack
            else if (phase == 1)
            {
                //swing animation
                playerController.instance.transform.localScale = new Vector3(1f, 1f, 1f);
                //this disables the attack hitbox
                if (count == active)
                {
                    phase++;
                    count = 0;

                    playerController.instance.weaponHitbox.enabled = false;
                    playerController.instance.weaponHitbox.transform.localScale = new Vector2(0.1f, .5f);
                }
                else
                {
                    //Debug.Log("coudnd");
                    count++;
                }
            }
            //this is the endlag for the attack
            else if (phase == 2)
            {
                playerController.instance.transform.localScale = new Vector3(1.2f, 0.8f, 1f);
                if (count == endlag)
                {  
                      playerController.instance.ChangeState(playerController.instance.idle);
                }
                else
                {
                    count++;
                }
            }
            //this is the time to chain the combo
        }
        //prevents weird combo shenanigans
    if(delay > 0)
        {
            delay--;
        }
    }
    public override void JumpTrigger()
    {
        //this can be empty I code poorly
    }
    private void SetAttack()
    {
            phase = 0;
            count = 0;
   
        //light melee
        if (playerController.instance.attackVal == 1)
        {
            light = true;
            startup = melee.lightStartup;
            active = melee.lightActive;
            endlag = melee.lightEndlag;
            activeWeapon = melee;
        }
        //heavy melee
        else if (playerController.instance.attackVal == 2)
        {
            light = false;
            startup = melee.heavyStartup;
            active = melee.heavyActive;
            endlag = melee.heavyEndlag;
            activeWeapon = melee;
        }
        if(buttons.Count >= 2)
        {
            
            if(oldButtons.Count != 0)
            {
                if(buttons.ToArray()[0].Equals(oldButtons.ToArray()[0]) && buttons.ToArray()[1].Equals(oldButtons.ToArray()[1]))
                {
                    //Debug.Log("same");
                    playerController.instance.comboDown += 20;
                }
                else
                {
                    //Debug.Log("Different");
                    //Debug.Log(buttons.ToArray()[0].ToString() + " " + buttons.ToArray()[1].ToString() + " " + oldButtons.ToArray()[0].ToString() + " " + oldButtons.ToArray()[1].ToString());
                    playerController.instance.comboUp += 20;
                }
            }
            oldButtons.Clear();
            oldButtons.Enqueue(buttons.ToArray()[0]);
            oldButtons.Enqueue(buttons.ToArray()[1]);
            buttons.Clear();
            buttons.Enqueue(playerController.instance.attackVal);
        }
        else
        {
            buttons.Enqueue(playerController.instance.attackVal);
        }
        if(activeWeapon.element == Element.ELECTRIC)
        {
            lightFast = 2;
        }
        if(playerController.instance.attackVal <= 1)
        {
            playerController.instance.weaponHitbox.sprite = playerController.instance.meleeSp;
        }
        else
        {
            playerController.instance.weaponHitbox.sprite = playerController.instance.rangedSp;
        }
    }
    public void ComboDrop()
    {
        playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
        dropped = true;
        //GameObject.Find("PlayerUI").GetComponent<ComboCounter>().AdjustComboCounter(0, 1);
        //playerController.instance.ChangeHealth(-1f * enemy.GetComponent<EnemyDefault>().shouldAttack());
        playerController.instance.ChangeState(playerController.instance.idle);
    }

    public void DropItem()
    {
        playerController.instance.comboUp += 50;
        playerController.instance.comboTime += playerController.instance.comboBaseTime + (int)playerController.instance.itemVals[2];
        float rand = Random.Range(0, 100);
        Debug.Log(rand.ToString() + "  " + playerController.instance.itemVals[8] + "  " + (playerController.instance.transform.position.x + 2f * playerController.instance.dir).ToString() + " " + playerController.instance.transform.position.y.ToString());
        if (playerController.instance.itemVals[8] > rand || shouldDrop)
        {
            dropCount = 0;
            shouldDrop = false;
            playerController.Instantiate(playerController.instance.items, new Vector3(playerController.instance.transform.position.x + 2f * playerController.instance.dir, playerController.instance.transform.position.y + 1f, 0f), Quaternion.identity);
        }
        else
        {
            dropCount++;
            if (dropCount >= Mathf.Ceil(100f / playerController.instance.itemVals[8]) - 1)
            {
                dropCount = 0;
                shouldDrop = true;
            }
        }
    }
}

public class MenuState : State
{/*
  * what items should be done? like if I'm doing em
  * crit chance up
  * crit dmg up?
  * dmg up
  * hp up
  * armor?
  * lifesteal?
  * attackspeed
  * having zany items isn't the goal,
  * want intresting builds -Force intresting desisions.
  * 
  * like defence / health up with thorns
  * attackspeed high crit
  * attack damage high lifesteal?
  * 
  * crit works like IE?
  * 
  * so AS, AD, combo scale increase,, OMNI, HP, DEF === KEY 6, Easy to implement, FORCE people to pick, ala dead cells
  * dash dist? ms? jump height? - each chest give one of each? 3 categories, pick one??
  * 
  * every item class has one "rare" / gimicky upgrade?
  * Def = thorns?
  * Attack = poison / bleed (DOT)
  * Speed = rolling dash / grapple hook? how do you increase (can increase thorns and status, rolling but w/ normal dash?)
  * will maybe talk about in standup?
  * like rare drop will have all 3 and upgrade them?
  * time will tell.
  * each boss drops rare upgrade. multiple jumps / dashes as rare upgrade? multple dashes ??? rolling first, then multiple?
  * this is hard. will talk
  * 
  * 
  * 
  * AS, AD, RAMP, OMNI, HP, DEF, CRIT, DODGE, DROP
  * */
    public int att = 0;
    public int def = 0;
    public int luck = 0;
    int timer = 10;
    State future;
    float oldX = 0f;
    float oldY = 0f;
    bool move = false;
    public int weaponPos = 0;
    bool itemEx = false;
    bool weaponEx = false;
    bool enter;
    public int index = 1;
    int delay = 0;
    int startDelay = 0;

    public string[] descrip = new string[] 
    { "Attack Speed", "Attack Damage", "Combo Time", "Life Steal", "HP Up", "Defence Up", "Crit Chance", "Dodge Chance", "Drop Chance"};

    public Weapon[] weaponList = new Weapon[]
    {
        new ArmBlade(), new BFG(), new EnergySword(), new GatlingGun(), new LaserAssault(), new LaserPistol(), new LongSword(), new OrbitalCannon(),
        new Revolver(), new ShortSword(), new Shotgun(), new SniperRifle(), new Staff(), new StarterGun(), new StarterSword(), new UberKnuckles(),
        new WarHammer() 
    };
// Start is called before the first frame update
    public MenuState()
    {

    }
    public override void OnEnter()
    {
        index = 1;
        startDelay = 5;
        future = playerController.instance.oldState;
        if (playerController.instance.item)
        {
            att = Random.Range(0, 3);
            def = Random.Range(0, 3) + 3;
            luck = Random.Range(0, 3) + 6;
            itemEx = true;
            weaponEx = false;
            //Debug.Log(descrip[att] + ", " + descrip[def] + ", " + descrip[luck]);
            SceneManager.LoadScene("itemScene", LoadSceneMode.Additive);
            playerController.Destroy(playerController.instance.activeItem);
        }
        else if (playerController.instance.weapon)
        {
            itemEx = false;
            weaponEx = true;
            SceneManager.LoadScene("weaponScene", LoadSceneMode.Additive);
            index = 1;
            weaponPos = Random.Range(0, weaponList.Length);
            int elem = Random.Range(1, 5);
            if(elem == 0)
            {
                weaponList[weaponPos].element = Element.DEFAULT;
            }
            else if(elem == 1)
            {
                weaponList[weaponPos].element = Element.ELECTRIC;
            }
            else if (elem == 2)
            {
                weaponList[weaponPos].element = Element.WATER;
            }
            else if (elem == 3)
            {
                weaponList[weaponPos].element = Element.FIRE;
            }
            else if (elem == 4)
            {
                weaponList[weaponPos].element = Element.GROUND;
            }
            playerController.Destroy(playerController.instance.activeChest);
        }
        else
        {
            itemEx = false;
        }
        oldX = playerController.instance.rbs.velocity.x;
        oldY = playerController.instance.rbs.velocity.y;
        playerController.instance.rbs.velocity = new Vector2(0, 0);
        playerController.instance.rbs.gravityScale = 0;
        enter = false;
        timer = 50;
    }
    public override void OnExit()
    {
        itemEx = false;
        enter = false;
        playerController.instance.rbs.velocity = new Vector2(oldX, oldY);
        playerController.instance.rbs.gravityScale = playerController.instance.grav;
    }
    // Update is called once per frame
    public override void Update()
    {
        hori = playerController.instance.pHori;
        vert = playerController.instance.pVert;

        if (enter)
        {
            if (delay <= 0)
            {
                enter = false;
            }
        }
        else
        {
            enter = (Input.GetButtonDown("Interact") || Input.GetKeyDown(playerController.instance.inputs[6]));
            if (enter)
            {
                delay = 3;
            }
        }
        if(startDelay > 0)
        {
            enter = false;
        }
        
        //Debug.Log(enter);
    }
    public override void StateUpdate()
    {
        if(delay > 0)
        {
            delay--;
        }
        if(startDelay > 0)
        {
            startDelay--;
        }
        playerController.instance.invuln = true;
        playerController.instance.invulCount = 1;

        if (timer == 0)
        {
            Debug.Log("we got here");
            if (itemEx)
            {
                SceneManager.UnloadSceneAsync("itemScene");
            }
            else if (weaponEx)
            {
                SceneManager.UnloadSceneAsync("weaponScene");
            }
            playerController.instance.ChangeState(future);
        }
        else if (!itemEx && !weaponEx)
        {
            //Debug.Log("no item");
            timer--;
        }
        else
        {
           
            if (!move && hori == 0)
            {
                move = true;
            }
            else if (move && hori != 0)
            {
                index += (int)hori;
                if (itemEx)
                {
                    if (index < 0)
                    {
                        index = 2;
                    }
                    else if (index > 2)
                    {
                        index = 0;
                    }
                }
                else
                {
                    if (index < 0)
                    {
                        index = 1;
                    }
                    else if (index > 1)
                    {
                        index = 0;
                    }
                }
                Debug.Log(index);
                move = false;
            }

            if (enter)
            {
                if (itemEx)
                {
                    Debug.Log("key has been pressed");
                    if (index == 0)
                    {
                        if (att == 0)
                        {
                            playerController.instance.itemVals[att] += 0.25f;
                        }
                        else if (att == 1)
                        {
                            playerController.instance.itemVals[att] += 1f;
                        }
                        else
                        {
                            playerController.instance.itemVals[att] += 1f;
                        }
                    }
                    else if (index == 1)
                    {
                        if (def == 0 + 3)
                        {
                            playerController.instance.itemVals[def] += 1f;
                        }
                        else if (def == 1 + 3)
                        {
                            playerController.instance.itemVals[def] += 5f;
                            playerController.instance.ChangeHealth(5);
                        }
                        else
                        {
                            playerController.instance.itemVals[def] += 2f;
                        }
                    }
                    else
                    {
                        if (luck == 0 + 6)
                        {
                            playerController.instance.itemVals[luck] += 1f;
                        }
                        else if (luck == 1 + 6)
                        {
                            playerController.instance.itemVals[luck] += 1f;
                        }
                        else
                        {
                            playerController.instance.itemVals[luck] += 1f;
                        }
                    }
                    if (GameObject.Find("ControlSaver") != null)
                    {
                        customControls.instance.pItems = playerController.instance.itemVals;
                    }
                }
                else
                {
                    if(index == 0)
                    {

                    }
                    else
                    {
                        if (weaponList[weaponPos].ranged)
                        {
                            playerController.instance.rangedWeapon = weaponList[weaponPos];
                            if (GameObject.Find("ControlSaver") != null)
                            {
                                customControls.instance.pRange = playerController.instance.rangedWeapon;
                            }
                        }
                        else
                        {
                            playerController.instance.meleeWeapon = weaponList[weaponPos];
                            if (GameObject.Find("ControlSaver") != null)
                            {
                                customControls.instance.pMelee = playerController.instance.meleeWeapon;
                            }
                        }
                    }
                }
                
                timer = 0;
            }
        }
    }
    public override void JumpTrigger()
    {

    }
}
