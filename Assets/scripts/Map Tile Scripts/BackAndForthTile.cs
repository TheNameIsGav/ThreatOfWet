using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthTile : BasicTile
{
    public Vector2 position1;
    public Vector2 position2;
    public float secondsBetween;
    float t;
    bool goingToTwo;
    public bool sticky;

    protected override void Start()
    {
        base.Start();
        t = 0;
        goingToTwo = true;
    }

    float transTime()
    {
        return t / secondsBetween;
    }

    void FixedUpdate()
    {
        if (goingToTwo)
        {
            float newX = Mathf.Lerp(position1.x, position2.x, transTime());
            float newY = Mathf.Lerp(position1.y, position2.y, transTime());
            transform.position = new Vector3(newX, newY, transform.position.z);
            
        } else
        {
            float newX = Mathf.Lerp(position2.x, position1.x, transTime());
            float newY = Mathf.Lerp(position2.y, position1.y, transTime());
            transform.position = new Vector3(newX, newY, transform.position.z);
        }
        t += Time.deltaTime;
        if (t > secondsBetween)
        {
            goingToTwo = !goingToTwo;
            t = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (sticky)
        {
            if (collision.gameObject.Equals(GameManager.instance.Player))
            {
                collision.gameObject.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (sticky)
        {
            if (collision.gameObject.Equals(GameManager.instance.Player))
            {
                collision.gameObject.transform.SetParent(null);
            }
        }
    }
}
