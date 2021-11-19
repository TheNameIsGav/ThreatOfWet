using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool light;
    private int guess = 0;
    private int ended = 0;
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
    public AttackState()
    {

    }
    public override void OnEnter()
    {
        //Debug.Log("we got here");
        melee = playerController.instance.meleeWeapon;
        ranged = playerController.instance.rangedWeapon;
        phase = 0;
        count = 0;
        comboCount = 0;
        holdSpeed = playerController.instance.rbs.velocity.x;
        //playerController.instance.rbs.gravityScale = 0f;
        SetAttack();
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
        playerController.instance.rbs.gravityScale = playerController.instance.grav;
        playerController.instance.rbs.sharedMaterial = playerController.instance.go;
        playerController.instance.weaponHitbox.enabled = false;
        playerController.instance.combo = false;
        water = false;
        playerController.instance.comboCount = 0;
        lastVal = 0;
        scale = 1f;
        stun = 0;
        phase = 0;
        count = 0;
        comboCount = 0;
        ended = 0;
        guess = 0;
        buttons.Clear();
        oldButtons.Clear();
        playerController.instance.weaponHitbox.transform.localScale = new Vector2(0.1f, .5f);
        playerController.instance.transform.localScale = new Vector3(1f, 1f, 1f);
        
    }
    // Update is called once per frame
    public override void Update()
    {
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
    }
    public override void StateUpdate()
    {
        //this is so player doesn't get dropped combo for killing enemy
        if(playerController.instance.combo && enemy == null)
        {
            float rand = Random.Range(0, 100);
            Debug.Log(rand.ToString() + "  " + playerController.instance.itemVals[8] + "  " + (playerController.instance.transform.position.x + 2f * playerController.instance.dir).ToString() + " " + playerController.instance.transform.position.y.ToString());
            if(playerController.instance.itemVals[8] > rand || shouldDrop)
            {
                dropCount = 0;
                shouldDrop = false;
                playerController.Instantiate(playerController.instance.items, new Vector3 (playerController.instance.transform.position.x + 2f*playerController.instance.dir, playerController.instance.transform.position.y + 1f, 0f), Quaternion.identity);
            }
            else
            {
                dropCount++;
                if(dropCount >= Mathf.Ceil(100f / playerController.instance.itemVals[8]) - 1)
                {
                    dropCount = 0;
                    shouldDrop = true;
                }
            }
            playerController.instance.ChangeState(playerController.instance.idle);
        }
        //this is the code for the enders
        if (ended != 0)
        {
            //this is for the ground element
            if(ended == 1)
            {
                if(count >= Mathf.Max(3,Mathf.Floor(resetVal - playerController.instance.itemVals[0])))
                {
                    playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
                    playerController.instance.ChangeState(playerController.instance.idle);
                }
                else if(lastVal == guess)
                {
                    ended = 0;
                    phase = 0;
                    count = 0;
                    guess = 0;
                    scale *= 1.2f;
                    comboCount = 0;
                }
                else if (lastVal != 0)
                {
                    count = resetVal;
                }
            }
            //this is for fire element
            else if(ended == 2)
            {
                //enemy.transform.position = new Vector2(10f*  playerController.instance.dir + enemy.transform.position.x, 5f + enemy.transform.position.y);
                playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
                enemy.GetComponent<EnemyDefault>().TakeDamage(new Damage(4f,false,true,false));
                playerController.instance.ChangeState(playerController.instance.idle);
            }
            //this is for water element
            else if(ended == 3)
            {
                if(count >= resetVal)
                {
                    //have player take damage
                    playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
                    playerController.instance.ChangeState(playerController.instance.idle);
                }
                Debug.Log(count);
                if (lastVal != 0)
                {
                    
                    if (count > 15 && count < 45)
                    {
                        ended = 0;
                        phase = 0;
                        count = 0;
                        guess = 0;
                        comboCount = 0;
                        water = true;
                        Debug.Log("parried");
                    }
                    else
                    {
                        count = resetVal;
                    }
                }
            }
            //this is for lightning element
            else if(ended == 4)
            {
                if(count >= resetVal)
                {
                    playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
                    playerController.instance.ChangeState(playerController.instance.idle);
                }
                else if(lastVal != 0)
                {
                    if(lastVal % 2 != 0)
                    {
                        comboCount = -3;
                        ended = 0;
                        phase = 0;
                        count = 0;
                        guess = 0;
                    }
                    else
                    {
                        count = resetVal;
                    }
                }
            }
            //this is for the neutral element
            else if (ended == 5)
            {
                //dropped combo
                if (count >= resetVal)
                {
                    playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
                    playerController.instance.ChangeState(playerController.instance.idle);
                }
                else if (lastVal != 0)
                {
                    if (lastVal % 2 != 0)
                    {
                        comboCount = 0;
                        ended = 0;
                        phase = 0;
                        count = 0;
                        guess = 0;  
                    }
                    else
                    {
                        count = resetVal;
                    }
                }
            }
            //this is for when elem of ender == enemy elem
            else
            {
                playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
                playerController.instance.ChangeState(playerController.instance.idle);
            }
            count++;
            
        }
        else
        {
            //this is the start up on the attack
            if (phase == 0)
            {
                playerController.instance.transform.localScale = new Vector3(.8f, 1.2f, 1f);
                //this enables the attack hitbox
                if (count == startup)
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
                    if (playerController.instance.combo)
                    {
                        Debug.Log("how are we here");
                        //when the combo ender is a light
                        if(comboCount >= 4 && light)
                        {
                            playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
                            playerController.instance.ChangeState(playerController.instance.idle);
                        }
                        //when the combo ender is a heavy
                        else if(comboCount >= 4 || (!light && !water))
                        {
                            count = 0;
                            stun = 0;
                            Ender();
                        }
                        //when the combo peice is a linker
                        else
                        {
                            count = 0;
                            phase = 3;
                        }
                    }
                    //if the player didn't hit anything with attack
                    else
                    {
                        playerController.instance.ChangeState(playerController.instance.idle);
                    }
                }    
                else
                {
                    //this makes the ender feel better / happen quicker
                    if ((comboCount >= 4 || (!light && !water)) && playerController.instance.combo)
                    {
                        //if combo ender is a light
                        if (light)
                        {
                            playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
                            playerController.instance.ChangeState(playerController.instance.idle);
                        }
                        //if combo ender is a heavy
                        else
                        {
                            count = 0;
                            stun = 0;
                            Ender();
                        }
                    }
                    count++;
                }
            }
            //this is the time to chain the combo
            else if(phase == 3)
            {
                //this is when the player drops the combo
                if(count >= playerController.instance.hitstun)
                {
                    playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
                    playerController.instance.ChangeState(playerController.instance.idle);
                }
                else
                {
                    count++;
                }
            }
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
    private void Ender()
    {
        //this shit stuff is for combo scaling
        bool help = true;
        if (buttons.Count == oldButtons.Count)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if(buttons.ToArray()[i].Equals(oldButtons.ToArray()[i]))
                {
                    
                }
                else
                {
                    help = false;
                    i = buttons.Count;
                }
            }
        }
        else
        {
            help = false;
        }
        if(help)
        {
            //Debug.Log(buttons.ToArray().ToString() + "  " + oldButtons.ToArray().ToString());
            //Debug.Log(buttons.Peek().ToString() + "  " + oldButtons.Peek().ToString());
            Debug.Log("them equat");
            scale *= .9f;
        }
        else if(scale < 1f)
        {
            scale = 1f;
        }
        oldButtons.Clear();
        
        while(buttons.Count > 0)
        {
            oldButtons.Enqueue(buttons.Dequeue());
        }

        //buttons.CopyTo(oldButtons.ToArray(),0);
        
        //oldButtons = buttons;
        buttons.Clear();
        //combo scaling part ended
        water = false;
        comboCount = 0;
        guess = Random.Range(1, 3);
        //this is for the default ender, its the guess
        if(guess == 2)
        {
            Debug.Log("Gun");
            guess = 3;
        }
        else
        {
            Debug.Log("Sword");
        }
        //assigns the proper ender for the attack
        if(activeWeapon.element == Element.DEFAULT)
        {
            ended = 5;
        }
        else if(activeWeapon.element == Element.FIRE)
        {
            ended = 2;
        }
        else if (activeWeapon.element == Element.WATER)
        {
            ended = 3;
        }
        else if (activeWeapon.element == Element.ELECTRIC)
        {
            ended = 4;
        }
        else if (activeWeapon.element == Element.GROUND)
        {
            ended = 1;
        }
        
        if(activeWeapon.element == enemy.GetComponent<EnemyDefault>().Element && activeWeapon.element != Element.DEFAULT)
        {
            ended = 6;
        }

        lastVal = 0;
    }
    private void SetAttack()
    {
        //prevents skipping out of enders
        if (ended == 0)
        {
            phase = 0;
            count = 0;
            //stun = 0;
        }
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
        //light ranged
        else if (playerController.instance.attackVal == 3)
        {
            light = true;
            startup = ranged.lightStartup;
            active = ranged.lightActive;
            endlag = ranged.lightEndlag;
            activeWeapon = ranged;
        }
        //heavy ranged
        else if (playerController.instance.attackVal == 4)
        {
            light = false;
            startup = ranged.heavyStartup;
            active = ranged.heavyActive;
            endlag = ranged.heavyEndlag;
            activeWeapon = ranged;
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
    int att = 0;
    int def = 0;
    int luck = 0;
    int timer = 10;
    State future;
    float oldX = 0f;
    float oldY = 0f;
    bool move = false;
    bool itemEx = false;
    bool enter;
    int index = 1;
    int delay = 0;
    int startDelay = 0;

    public string[] descrip = new string[] 
    { "Attack Speed", "Attack Damage", "Combo Damage", "Life Steal", "HP Up", "Defence Up", "Crit Chance", "Dodge Chance", "Drop Chance"};
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
            Debug.Log(descrip[att] + ", " + descrip[def] + ", " + descrip[luck]);
        }
        else
        {
            itemEx = false;
        }
        oldX = playerController.instance.rbs.velocity.x;
        oldY = playerController.instance.rbs.velocity.y;
        playerController.instance.rbs.velocity = new Vector2(0, 0);
        playerController.instance.rbs.gravityScale = 0;
        playerController.Destroy(playerController.instance.activeItem);
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
            playerController.instance.ChangeState(future);
        }
        else if (!itemEx)
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
                if (index < 0)
                {
                    index = 2;
                }
                else if (index > 2)
                {
                    index = 0;
                }
                Debug.Log(index);
                move = false;
            }

            if (enter)
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
                        playerController.instance.itemVals[att] += 0.02f;
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
                timer = 0;
            }
        }
    }
    public override void JumpTrigger()
    {

    }
}
