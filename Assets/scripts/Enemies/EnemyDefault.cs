using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyDefault
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

    Element Element; //Singular Integer Identifier of the element type of this enemy
    List<int> Enhancements; //List of Integer Identifiers of enhancements

    abstract public void triggerDamage(bool b);

    abstract public void DealDamage();

    /// <summary>
    /// Takes in a positive float and subtracts that value from the enemies health
    /// </summary>
    /// <param name="inc"></param>
    abstract public void TakeDamage(Damage inc);

    /// <summary>
    /// Called to spawn an enemy with the arguments Position and Difficulty
    /// </summary>
    /// <param name="position"></param>
    /// <param name="difficulty"></param>
    abstract public void Spawn(Vector2 position, float difficulty);
}
