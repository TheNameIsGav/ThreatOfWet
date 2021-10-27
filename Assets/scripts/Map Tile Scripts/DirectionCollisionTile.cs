using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCollisionTile : BasicTile
{
    public int direction;
     int UP = 1;
     int RIGHT = 2;
     int DOWN = 3;
     int LEFT = 4;
    bool oneWay = false;
    Collider2D extraHitbox;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        extraHitbox = this.gameObject.AddComponent<BoxCollider2D>();
        if (Up())
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x, extraHitbox.offset.y-.5f);
        } else if (Right())
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x + .5f, extraHitbox.offset.y);
        }
        else if (Down())
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x, extraHitbox.offset.y + .5f);
        }
        else // if (Left()) 
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x - .5f, extraHitbox.offset.y);
        }
        extraHitbox.isTrigger = true;
    }

    bool Up()
    {
        return direction == UP;
    }
    bool Right()
    {
        return direction == RIGHT;
    }
    bool Down()
    {
        return direction == DOWN;
    }
    bool Left()
    {
        return direction == LEFT;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void updateBehavior(Collider2D col, bool exitingCollider)
    {
        if (exitingCollider)
            updateExitBehavior(col);
        else
            updateEnterBehavior(col);
    }

    protected override void updateExitBehavior(Collider2D col)
    {
        if (col == extraHitbox)
        {
            hitbox.isTrigger = false;
        }
    }

    protected override void updateEnterBehavior(Collider2D col)
    {
        if (col == extraHitbox)
        {
            hitbox.isTrigger = true;
        }
    }
}
