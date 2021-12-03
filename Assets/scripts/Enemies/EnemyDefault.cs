using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    Damage damEncap = new Damage(5);
    public Damage Damage { get { return damEncap; } set { damEncap = value; } }

    public void StartExclamationCountdown(float duration)
    {
        //Debug.Log("Duration " + duration);
        StartCoroutine(startExclamationPrivate(duration));
    }

    IEnumerator startExclamationPrivate(float duration)
    {
        //Debug.Log("e");
        GameObject a = transform.GetChild(0).transform.GetChild(2).gameObject;
        if (a != null)
        {
            a.SetActive(true);
            Image excl = a.GetComponent<Image>();
            yield return new WaitForSeconds(duration / 3.33f);
            excl.color = Color.magenta;
            yield return new WaitForSeconds(duration / 3.33f);
            excl.color = Color.red;
            yield return new WaitForSeconds(duration / 3.33f);
            a.SetActive(false);
            excl.color = Color.yellow;
        }
        else
        {
            yield return new WaitForSeconds(duration);
        }

    }

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

    void GenerateDamage()
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
            d.CritDam = .25f;
            d.CritPercent = 25;
        }

        if (enhance.Contains(Enhancements.UNBLOCK))
        {
            d.Heavy = true;
        }

        if (enhance.Contains(Enhancements.CHARGE))
        {
            d.Charge = true;
        }

        damEncap = d;
    }

    public float shouldAttack()
    {
        if (damEncap.Heavy)
        {
            playerController.instance.block = false;
        }

        return damEncap.getDamage();
    }

    public bool InRange()
    {
        GameObject p = GameObject.Find("player");

        //theoretically checks to see if we can attack the player with a favor towards horizontal angles.
        if((Mathf.Abs(p.transform.position.x - transform.position.x) <= attackRange && Mathf.Abs(p.transform.position.y - transform.position.y) <= attackRange/2) 
            ||
            (enhance.Contains(Enhancements.CHARGE) && Mathf.Abs(p.transform.position.x - transform.position.x) <= attackRange*3 && Mathf.Abs(p.transform.position.y - transform.position.y) <= attackRange / 2)) {
            return true;
        }
        return false;
    }


    //y = log_{b}x - 2 //Plug this into desmos to play with it
    private void GenerateEnhancements(float difficulty)
    {
        //Calculates the correct number of enhancements to generate onto this enemy
        //2.56 is the offset so that we can never have a negative number of enhancements
        difficulty = Mathf.Max(2.56f, difficulty);
        int num = Mathf.Min(Mathf.RoundToInt(Mathf.Log(difficulty, 1.6f) - 2), 9);
        List<Enhancements> t = new List<Enhancements>();
        for (int i = 0; i < 9; i++)
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
                case Enhancements.UNBLOCK:
                    eName = "Unblockable " + eName;
                    break;
                case Enhancements.CHARGE:
                    eName = "Charging " + eName;
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
        GenerateDamage();
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
