using UnityEngine;

public class DirectionCollisionTile : BasicTile
{
    public int blockDirection;
    readonly int UP = 1; // F
    readonly int RIGHT = 2; // R
    readonly int DOWN = 3; // E
    readonly int LEFT = 4; // E
    // That spells free
    BoxCollider2D extraHitbox;

    // CreditReport.com/baby
    protected override void Start()
    {
        base.Start();
        if (blockDirection == 0)
            blockDirection = 1;
        extraHitbox = gameObject.AddComponent<BoxCollider2D>();
        if (Up())
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x, extraHitbox.offset.y - .3f);
            // extraHitbox.size = new Vector2(1.2f, 1);
        } else if (Right())
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x - .3f, extraHitbox.offset.y);
            // extraHitbox.size = new Vector2(1, 1.2f);
        }
        else if (Down())
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x, extraHitbox.offset.y + .5f);
            // extraHitbox.size = new Vector2(1.2f, 1);
        }
        else // if (Left()) 
        {
            extraHitbox.offset = new Vector2(extraHitbox.offset.x + .3f, extraHitbox.offset.y);
            // extraHitbox.size = new Vector2(1, 1.2f);
        }
        extraHitbox.isTrigger = true;
        // this.extraHitbox = newCollider;
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

    public override void updateBehavior(Collider2D col, Collider2D other, bool exitingCollider)
    {
        if (other.Equals(GameManager.instance.Player)
            || other.Equals(GameManager.instance.Player.transform.GetChild(0).gameObject)
            || other.Equals(GameManager.instance.Player.transform.GetChild(1).gameObject))
            return;
        else
        {
            if (exitingCollider)
                updateExitBehavior(col);
            else
                updateEnterBehavior(col);
        }
    }



    protected override void updateExitBehavior(Collider2D col)
    {
        BoxCollider2D colBox = (BoxCollider2D) col;
        if (colBox == extraHitbox || Down())
        {
            if (Down())
            {
                hitbox.isTrigger = false;
                extraHitbox.enabled = true;
            } else
            {
                hitbox.enabled = true;
            }
            Debug.Log("Leaving block");
        }
    }

    protected override void updateEnterBehavior(Collider2D col)
    {
        if (col == extraHitbox || true)
        {
            if (Down())
            {
                hitbox.isTrigger = true;
                extraHitbox.enabled = false;
            } else
            {
                hitbox.enabled = false;
            }
            Debug.Log("Entering block");
        }
    }
}
