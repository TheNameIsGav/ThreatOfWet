using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCollisionTile : BasicTile
{
    public int blockDirection;
     int UP = 1; // F
     int RIGHT = 2; // R
     int DOWN = 3; // E
     int LEFT = 4; // E
    // That spells free
    Collider2D extraHitbox;

    // CreditReport.com/baby
    protected override void Start()
    {
        base.Start();
        if (blockDirection == 0)
            blockDirection = 1;
        extraHitbox = this.gameObject.AddComponent<BoxCollider2D>();
        if (Up())
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x, extraHitbox.offset.y-.2f);
        } else if (Right())
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x - .2f, extraHitbox.offset.y);
        }
        else if (Down())
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x, extraHitbox.offset.y + .2f);
        }
        else // if (Left()) 
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x + .2f, extraHitbox.offset.y);
        }
        extraHitbox.isTrigger = true;
    }

    bool Up()
    {
        return blockDirection == UP;
    }
    bool Right()
    {
        return blockDirection == RIGHT;
    }
    bool Down()
    {
        return blockDirection == DOWN;
    }
    bool Left()
    {
        return blockDirection == LEFT;
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
            hitbox.enabled = true;
        }
    }

    protected override void updateEnterBehavior(Collider2D col)
    {
        if (col == extraHitbox)
        {
            hitbox.enabled = false;
        }
    }
}
