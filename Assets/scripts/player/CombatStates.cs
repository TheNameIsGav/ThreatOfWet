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
        playerController.instance.comboCount = 0;
        stun = 0;
        phase = 0;
        count = 0;
        comboCount = 0;
        ended = 0;
        guess = 0;
        playerController.instance.weaponHitbox.transform.localScale = new Vector2(0.1f, .5f);
        playerController.instance.transform.localScale = new Vector3(1f, 1f, 1f);
        
    }
    // Update is called once per frame
    public override void Update()
    {
        if (currVal != playerController.instance.attackVal && playerController.instance.attackVal != 0)
        {
            SetAttack();
            Debug.Log("shuld be zero");
            currVal = playerController.instance.attackVal;
            lastVal = currVal;
        }
        else if (playerController.instance.attackVal == 0)
        {
            currVal = 0;
        }
    }
    public override void StateUpdate()
    { if (ended != 0)
        {
            if(ended == 1)
            {
                if(count >= resetVal)
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
                    comboCount = 0;
                }
                else if (lastVal != 0)
                {
                    count = resetVal;
                }
            }
            count++;
        }
        else
        {
            if (phase == 0)
            {
                playerController.instance.transform.localScale = new Vector3(.8f, 1.2f, 1f);
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
            else if (phase == 1)
            {
                playerController.instance.transform.localScale = new Vector3(1f, 1f, 1f);
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
            else if (phase == 2)
            {
                playerController.instance.transform.localScale = new Vector3(1.2f, 0.8f, 1f);
                if (count == endlag)
                {

                    //phase = 0;
                    //count = 0;
                    //Debug.Log("left by normal means");
                    if (playerController.instance.combo)
                    {
                        if(comboCount >= 4 && light)
                        {
                            playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
                            playerController.instance.ChangeState(playerController.instance.idle);
                        }
                        else if(comboCount >= 4)
                        {
                         count = 0;
                         stun = 0;
                         Ender();
                        }
                        else
                        {
                            //count = 0;
                            phase = 3;
                        }
                    }
                    else
                    {
                        playerController.instance.ChangeState(playerController.instance.idle);
                    }
                    /*
                    if (playerController.instance.combo && count + stun == playerController.instance.hitstun)
                    {
                        playerController.instance.ChangeState(playerController.instance.idle);
                    }
                    else if (playerController.instance.combo && playerController.instance.comboCount >= 4)
                    {
                        if (light)
                        {
                            playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 30f, 10f);
                            playerController.instance.ChangeState(playerController.instance.idle);
                        }
                        else
                        {
                            count = 0;
                            stun = 0;
                            Ender();
                        }
                    }
                    else if (playerController.instance.combo)
                    {
                        stun++;
                    }
                    else
                    {
                        //playerController.instance.rbs.velocity = new Vector2(-1 * playerController.instance.dir * 25f, 10f);
                        playerController.instance.ChangeState(playerController.instance.idle);
                    }
                    */
                }    
                else
                {
                    //Debug.Log("coudnd");
                    count++;
                }
            }
            else if(phase == 3)
            {
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
    }
    public override void JumpTrigger()
    {
        //this can be empty I code poorly
    }
    private void Ender()
    {
        //this is code for default ender
        comboCount = 0;
        guess = Random.Range(1, 3);
        if(guess == 2)
        {
            Debug.Log("Gun");
            guess = 3;
        }
        else
        {
            Debug.Log("Sword");
        }
        Debug.Log(guess);
        ended = 1;
        lastVal = 0;
    }
    private void SetAttack()
    {
        if (ended == 0)
        {
            phase = 0;
            count = 0;
            //stun = 0;
        }
        if (playerController.instance.attackVal == 1)
        {
            light = true;
            startup = melee.lightStartup;
            active = melee.lightActive;
            endlag = melee.lightEndlag;
            activeWeapon = melee;
        }
        else if (playerController.instance.attackVal == 2)
        {
            light = false;
            startup = melee.heavyStartup;
            active = melee.heavyActive;
            endlag = melee.heavyEndlag;
            activeWeapon = melee;
        }
        else if (playerController.instance.attackVal == 3)
        {
            light = true;
            startup = ranged.lightStartup;
            active = ranged.lightActive;
            endlag = ranged.lightEndlag;
            activeWeapon = ranged;
        }
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
{
    // Start is called before the first frame update
    public MenuState()
    {

    }
    public override void OnEnter()
    {

    }
    public override void OnExit()
    {

    }
    // Update is called once per frame
    public override void Update()
    {

    }
    public override void StateUpdate()
    {

    }
    public override void JumpTrigger()
    {

    }
}
