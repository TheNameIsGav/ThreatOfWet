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
    private float holdSpeed;
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
        holdSpeed = playerController.instance.rbs.velocity.x;
        //playerController.instance.rbs.gravityScale = 0f;
        playerController.instance.rbs.gravityScale = playerController.instance.grav;
        playerController.instance.rbs.sharedMaterial = playerController.instance.stop;
        playerController.instance.transform.localScale = new Vector3(1f, 1f, 1f);
        //playerController.instance.rbs.velocity = new Vector2(0f, 0f);
        if (playerController.instance.attackVal == 1)
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
        playerController.instance.rbs.sharedMaterial = playerController.instance.go;
        playerController.instance.weaponHitbox.enabled = false;
        playerController.instance.weaponHitbox.transform.localScale = new Vector2(0.1f, .5f);
        playerController.instance.transform.localScale = new Vector3(1f, 1f, 1f);
        
    }
    // Update is called once per frame
    public override void Update()
    {
        
    }
    public override void StateUpdate()
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
                Debug.Log("coudnd");
                count++;
            }
        }
        else if(phase == 1)
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
                Debug.Log("coudnd");
                count++;
            }
        }
        else if(phase == 2)
        {
            playerController.instance.transform.localScale = new Vector3(1.2f, 0.8f,1f);
            if (count == endlag)
            {
                phase = 0;
                count = 0;
                Debug.Log("left by normal means");
                if (playerController.instance.pHori != 0)
                {
                    playerController.instance.rbs.velocity = new Vector2(holdSpeed, playerController.instance.rbs.velocity.y);
                }
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
