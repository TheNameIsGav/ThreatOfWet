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
    private int phase = 0;
    private int count = 0;
    public AttackState()
    {

    }
    public override void OnEnter()
    {
        Debug.Log("we got here");
        melee = playerController.instance.meleeWeapon;
        ranged = playerController.instance.rangedWeapon;
        phase = 0;
        count = 0;
        playerController.instance.rbs.gravityScale = 0f;
        playerController.instance.rbs.velocity = new Vector2(0f, 0f);
        if(playerController.instance.attackVal == 1)
        {
            startup = melee.lightStartup;
            active = melee.lightActive;
            endlag = melee.lightEndlag;
            activeWeapon = melee;
        }
        else if(playerController.instance.attackVal == 2)
        {
            startup = melee.heavyStartup;
            active = melee.heavyActive;
            endlag = melee.heavyEndlag;
            activeWeapon = melee;
        }
        else if (playerController.instance.attackVal == 3)
        {
            startup = ranged.lightStartup;
            active = ranged.lightActive;
            endlag = ranged.lightEndlag;
            activeWeapon = ranged;
        }
        else if (playerController.instance.attackVal == 4)
        {
            startup = ranged.heavyStartup;
            active = ranged.heavyActive;
            endlag = ranged.heavyEndlag;
            activeWeapon = ranged;
        }
        Debug.Log(startup);
        Debug.Log(active);
        Debug.Log(endlag);
    }
    public override void OnExit()
    {
        playerController.instance.rbs.gravityScale = playerController.instance.grav;
    }
    // Update is called once per frame
    public override void Update()
    {
        
    }
    public override void StateUpdate()
    {
        if (phase == 0)
        {
            if(count == startup)
            {
                phase++;
                count = 0;
                
                playerController.instance.weaponHitbox.enabled = true;
                playerController.instance.weaponHitbox.transform.localScale = new Vector2(activeWeapon.hitboxWidth * Mathf.Sign(playerController.instance.rbs.velocity.x), activeWeapon.hitboxHeight);
            }
            else
            {
                Debug.Log("coudnd");
                count++;
            }
        }
        else if(phase == 1)
        {
            if (count == active)
            {
                phase++;
                count = 0;

                playerController.instance.weaponHitbox.enabled = false;
                playerController.instance.weaponHitbox.transform.localScale = new Vector2(0.1f, .5f);
            }
            else
            {
                Debug.Log("coudnd");
                count++;
            }
        }
        else if(phase == 2)
        {
            if (count == endlag)
            {
                phase = 0;
                count = 0;
                Debug.Log("left by normal means");
                playerController.instance.ChangeState(playerController.instance.idle);
            }
            else
            {
                Debug.Log("coudnd");
                count++;
            }
        }
    }
    public override void JumpTrigger()
    {
        //this can be empty I code poorly
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
