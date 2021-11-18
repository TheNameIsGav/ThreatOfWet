using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    float health = 100;
    public float Health { get { return health; } set { health = value; } }

    bool shouldDie = false;
    public bool Die { get { return shouldDie; } set { shouldDie = value; } }

    float spawnDifficulty;

    int speed;
    public int Speed { get { return speed; } set { speed = value; } }

    int baseDamage = 5;

    float aggroRange = 2.5f;
    public float Range { get { return aggroRange; } set { aggroRange = value; } }

    Element element; //Singular Integer Identifier of the element type of this enemy
    public Element Element { get { return element; } set { element = value; } }

    List<Enhancements> enhance = new List<Enhancements>();
    public List<Enhancements> Enhance { get { return enhance; } set { enhance = value; } }

    /// <summary>
    /// Takes in a positive float and subtracts that value from the enemies health
    /// </summary>
    /// <param name="inc"></param>
    public void TakeDamage(Damage dam)
    {
        if(enhance.Contains(Enhancements.SHIELDED))
        {
            if (!dam.Ranged)
            {
                health -= dam.getDamage();
            }

        } else if(enhance.Contains(Enhancements.SPIKY))
        {
            health -= dam.getDamage();
            GameObject p = GameObject.Find("player");
            if (p != null)
            {
                p.GetComponent<playerController>().ChangeHealth(dam.getDamage() * .25f);
            }

        } else if (enhance.Contains(Enhancements.ARMORED))
        {
            health -= dam.getDamage() * .5f;
        } else
        {
            health -= dam.getDamage();
        }
        
        //Debug.Log("Took damage from somewhere, now at " + health + " hp");
        if(health <= 0)
        {
            shouldDie = true;
        }
    }

    public Damage shouldAttack()
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

        return d;
    }

    //y = log_{b}x - 2 //Plug this into desmos to play with it
    private void GenerateEnhancements(float difficulty)
    {
        //Calculates the correct number of enhancements to generate onto this enemy
        //2.56 is the offset so that we can never have a negative number of enhancements
        difficulty = Mathf.Max(2.56f, difficulty);
        int num = Mathf.Min(Mathf.RoundToInt(Mathf.Log(difficulty, 1.6f) - 2), 7);
        for(int i = 0; i < num; i++)
        {
            int index = Random.Range(0, 7 - i);
            enhance.Add((Enhancements)index);
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
            gameObject.transform.localScale = new Vector3(2, 2);
        }
    }

}
