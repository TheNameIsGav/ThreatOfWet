using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    float health = 100;
    public float Health { get { return health; } set { health = value; } }

    public float maxHealth = 100;
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    bool shouldDie = false;
    public bool Die { get { return shouldDie; } set { shouldDie = value; } }

    float spawnDifficulty;

    public float speed = .01f;
    public float Speed { get { return speed; } set { speed = value; } }

    public float attackSpeed = 1.5f;
    public float AtkSpd { get { return attackSpeed; } set { attackSpeed = value; } }

    public int baseDamage = 5;

    public float attackRange = 2.5f;
    public float Range { get { return attackRange; } set { attackRange = value; } }

    List<Enhancements> enhance = new List<Enhancements>();
    public List<Enhancements> Enhance { get { return enhance; } set { enhance = value; } }

    public string eName = "";
    public string EName { get { return name; } set { name = value; } }

    public bool canHeavy;
    public bool Heavy { get { return canHeavy; } set { canHeavy = value; } }

    public bool canCharge;
    public bool Charge { get { return canCharge; } set { canCharge = value; } }

    /// <summary>
    /// Takes in a positive float and subtracts that value from the enemies health
    /// </summary>
    /// <param name="inc"></param>
    public void TakeDamage(Damage dam)
    {
        float val = dam.getDamage();
        if (enhance.Contains(Enhancements.SHIELDED) && dam.Ranged)
        {
            return;
        }

        if (enhance.Contains(Enhancements.SPIKY))
        {
            GameObject p = GameObject.Find("player");
            if (p != null)
            {
                p.GetComponent<playerController>().ChangeHealth(val * .25f);
            }
        }

        if (enhance.Contains(Enhancements.ARMORED))
        {
            health -= val * .5f;
            transform.GetChild(0).GetComponent<EnemyNameGenerator>().UpdateEnemyHealthBar(health, maxHealth);

            //Debug.Log("Took damage from somewhere, now at " + health + " hp");
        } else
        {
            health -= val;
            transform.GetChild(0).GetComponent<EnemyNameGenerator>().UpdateEnemyHealthBar(health, maxHealth);
            //Debug.Log("Took damage from somewhere, now at " + health + " hp");
        }
        
        if(health <= 0)
        {
            shouldDie = true;
        }
    }

    public float shouldAttack()
    {
        Damage d = new Damage(baseDamage);

        if (enhance.Contains(Enhancements.NECROTIC))
        {
            d.Necrotic = true;
        }

        if (enhance.Contains(Enhancements.SERRATED))
        {
            d.Bleed = true;
        }

        if (enhance.Contains(Enhancements.PIERCING))
        {
            d.Crit = true;
            d.CritDam = .5f;
            d.CritPercent = 75;
        }

        return d.getDamage();
    }

    bool InRange()
    {
        GameObject p = GameObject.Find("player");

        //theoretically checks to see if we can attack the player with a favor towards horizontal angles.
        if(Mathf.Abs(p.transform.position.x - transform.position.x) <= attackRange && Mathf.Abs(p.transform.position.y - transform.position.y) <= attackRange/2) {
            return true;
        }
        return false;
    }

    public void flashExclamation(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    IEnumerator EnemyAttack()
    {
        Debug.Log("Entering into attack method");
        //Alert Color
        if (canCharge && UnityEngine.Random.Range(0, 1) <= .33f) //If this enemy can charge attack
        {
            flashExclamation(Color.yellow);
        }
        else if (canHeavy && UnityEngine.Random.Range(0, 1) <= .33f) //If this enemy can heavy attack
        {
            flashExclamation(Color.magenta);
        }
        else //We normal attack
        {
            flashExclamation(Color.red);
        }
        yield return new WaitForSeconds(attackSpeed);

        //Make Attack
        if (InRange())
        {
            GameObject.Find("player").GetComponent<playerController>().ChangeHealth(-1f * shouldAttack());            
        }
        flashExclamation(Color.white);
        yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 10f));
        StartCoroutine(EnemyAttack());
    }

    bool shouldStartAttacking = true;
    public void StartAttackCycle()
    {
        Debug.Log("starting attack cycle");
        if(shouldStartAttacking) StartCoroutine(EnemyAttack());
        shouldStartAttacking = false;
    }

    public void StopAttackCycle()
    {
        Debug.Log("stopping attack cycle");
        StopCoroutine(EnemyAttack());
        shouldStartAttacking = true;
    }

    //y = log_{b}x - 2 //Plug this into desmos to play with it
    private void GenerateEnhancements(float difficulty)
    {
        //Calculates the correct number of enhancements to generate onto this enemy
        //2.56 is the offset so that we can never have a negative number of enhancements
        difficulty = Mathf.Max(2.56f, difficulty);
        int num = Mathf.Min(Mathf.RoundToInt(Mathf.Log(difficulty, 1.6f) - 2), 7);
        List<Enhancements> t = new List<Enhancements>();
        for (int i = 0; i < 7; i++)
        {
            t.Add((Enhancements)i);
        }

        for(int i = 0; i < num; i++)
        {
            int index = UnityEngine.Random.Range(0, t.Count);
            Enhancements e = t[index];
            enhance.Add(e);
            t.Remove(e);

            switch (e)
            {
                case Enhancements.ARMORED:
                    eName = "Armored " + eName;
                    break;
                case Enhancements.SPIKY:
                    eName = "Pointy " + eName;
                    break;
                case Enhancements.SHIELDED:
                    eName = "Shielded " + eName;
                    break;
                case Enhancements.PIERCING:
                    eName = "Piercing " + eName;
                    break;
                case Enhancements.SERRATED:
                    eName = "Cutting " + eName;
                    break;
                case Enhancements.NECROTIC:
                    eName = "Diseased " + eName;
                    break;
                case Enhancements.BIG:
                    eName = "Healthy " + eName;
                    break;
            }
        }
    }

    /// <summary>
    /// Called to spawn an enemy with the arguments Position and Difficulty
    /// </summary>
    /// <param name="position"></param>
    /// <param name="difficulty"></param>
    public void Spawn(Vector2 position, float difficulty) {
        GenerateEnhancements(difficulty);
        spawnDifficulty = difficulty;
        if (enhance.Contains(Enhancements.BIG))
        {
            maxHealth *= 2;
            
        }
        health = maxHealth;
        transform.GetChild(0).GetComponent<EnemyNameGenerator>().GenerateEnemyName(eName);

        transform.position = position;
    }

}
