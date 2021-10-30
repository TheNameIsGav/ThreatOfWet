using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    float health;
    int scale;
    int speed;

    float aggroRange = 5f;

    int Element; //Singular Integer Identifier of the element type of this enemy
    List<int> Enhancements; //List of Integer Identifiers of enhancements

    public float getAggroRange() { return aggroRange; }
}
