using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiMovementTile : BasicTile
{
    public Vector2[] positions;
    public float[] transitionsInSeconds;
    int currStartPoisition;
    float t = 0;
    public bool sticky;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (sticky)
        {
            if (collision.gameObject.Equals(GameManager.instance.Player))
            {
                collision.gameObject.transform.SetParent(transform);
                collision.gameObject.transform.localScale = new Vector3(1/(transform.localScale.x), 1 / (transform.localScale.y), 1);
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
                collision.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
