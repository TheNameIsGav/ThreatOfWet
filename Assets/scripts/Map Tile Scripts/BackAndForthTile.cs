using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthTile : BasicTile
{
    public Vector2 position1;
    public Vector2 position2;
    public float secondsBetween;
    Vector2 velocity;
    bool goingToTwo;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        secondsBetween *= 60;
        // Debug.Log(secondsBetween);
        velocity = new Vector2((position2.x - position1.x) / secondsBetween, 
            (position2.y - position1.y) / secondsBetween
            );
        // Debug.Log("("+velocity.x+", "+velocity.y+")");
        transform.position = position1;
        goingToTwo = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + velocity.x, transform.position.y + velocity.y, transform.position.z);
        if ((goingToTwo && didItPass(transform.position, position2, velocity)) 
            || (!goingToTwo && didItPass(transform.position, position1, velocity)))
        {
            velocity.x = velocity.x * -1;
            velocity.y = velocity.y * -1;
            goingToTwo = !goingToTwo;
        } 
    }

    private bool didItPass(Vector3 pos, Vector2 dest, Vector2 velocity)
    {
        bool xPassed = (pos.x <= dest.x && velocity.x < 0) 
            || (pos.x >= dest.x && velocity.x > 0) 
            || (velocity.x == 0);
        bool yPassed = (pos.y <= dest.y && velocity.y < 0)
            || (pos.y >= dest.y && velocity.y > 0)
            || (velocity.y == 0);
        return xPassed && yPassed;
    }
}
