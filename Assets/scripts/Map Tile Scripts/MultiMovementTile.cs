using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiMovementTile : BasicTile
{
    public Vector2[] positions;
    public float[] transitionsInSeconds;
    int currStartPoisition;
    float t = 0;

    protected override void Start()
    {
        base.Start();
        currStartPoisition = 0;
        t = 0;
    }

    Vector2 currPos()
    {
        return positions[currStartPoisition];
    }

    Vector2 nextPos()
    {
        return positions[(currStartPoisition + 1) % positions.Length];
    }

    void next()
    {
        currStartPoisition = (currStartPoisition + 1) % positions.Length;
    }

    float currTime()
    {
        return transitionsInSeconds[currStartPoisition % transitionsInSeconds.Length];
    }

    float transTime()
    {
        return t / currTime();
    }

    void FixedUpdate()
    {
        float newX = Mathf.Lerp(currPos().x, nextPos().x, transTime());
        float newY = Mathf.Lerp(currPos().y, nextPos().y, transTime());
        transform.position = new Vector3(newX, newY, transform.position.z);
        t += Time.deltaTime;
        if (t > currTime())
        {
            t = 0;
            next();
        }
    }
}
