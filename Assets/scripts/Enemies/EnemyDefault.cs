using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    float health = 100;
    public float Health { get { return health; } set { health = value; } }

    bool shouldDie = false;
    public bool Die { get { return shouldDie; } set { shouldDie = value; } }

    int scale;

    int speed;
    public int Speed { get { return speed; } set { speed = value; } }

    float pathRange = 15f;
    public float PathR { get { return pathRange; } set { pathRange = value; } }

    int baseDamage = 5;

    float aggroRange = 2.5f;
    public float Range { get { return aggroRange; } set { aggroRange = value; } }

    int Element; //Singular Integer Identifier of the element type of this enemy
    List<int> Enhancements; //List of Integer Identifiers of enhancements

    public void triggerDamage(bool b)
    {
        if (b)
        {
            InvokeRepeating("DealDamage", .01f, 2f);
        } else
        {
            CancelInvoke();
        }
    }

    private void DealDamage()
    {
        //TODO Get player and scaled damage component from Game Manager
        GameObject.Find("player").GetComponent<playerController>().ChangeHealth(-baseDamage);
        transform.GetChild(1).GetComponent<ParticleSystem>().Play();
    }

    /// <summary>
    /// Takes in a positive float and subtracts that value from the enemies health
    /// </summary>
    /// <param name="inc"></param>
    public void TakeDamage(float inc)
    {
        health -= inc;
        Debug.Log("Took damage from somewhere, now at " + health + " hp");
        transform.GetChild(2).GetComponent<ParticleSystem>().Play();

        if(health <= 0)
        {
            shouldDie = true;
        }
    }
}
