using UnityEngine;

public class DirectionCollisionTile : BasicTile
{
    public string blockDirection;
    readonly int UP = 1; // F
    readonly int RIGHT = 2; // R
    readonly int DOWN = 3; // E
    readonly int LEFT = 4; // E
    // That spells free
    EdgeCollider2D extraHitbox;
    bool generatingEdge = false;
    bool playerColliding = false;

    // CreditReport.com/baby
    protected override void Start()
    {
        base.Start();
        extraHitbox = gameObject.GetComponent<EdgeCollider2D>();
        Debug.Log("Before: " + extraHitbox);
        if (extraHitbox == null)
        {
            // extraHitbox = gameObject.AddComponent<EdgeCollider2D>();
            generatingEdge = true;
        }
        Debug.Log("After: " + extraHitbox);
        /*
        if (generatingEdge)
        {
            if (Up())
            {
                Bounds boxBounds = hitbox.bounds;
                extraHitbox.points = new Vector2[] {new Vector2(boxBounds.center.x - boxBounds.extents.x, boxBounds.center.y + boxBounds.extents.y),
                    new Vector2(boxBounds.center.x + boxBounds.extents.x, boxBounds.center.y + boxBounds.extents.y)};
            }
            else if (Right())
            {
                extraHitbox.points = new Vector2[] {new Vector2(transform.position.x + hitbox.bounds., transform.position.y + boxBounds.extents.y),
                    new Vector2(transform.position.x + boxBounds.extents.x, transform.position.y - boxBounds.extents.y)};
            }
            else if (Down())
            {
                Bounds boxBounds = hitbox.bounds;
                extraHitbox.points = new Vector2[] {new Vector2(boxBounds.center.x - boxBounds.extents.x, boxBounds.center.y - boxBounds.extents.y),
                    new Vector2(boxBounds.center.x + boxBounds.extents.x, boxBounds.center.y - boxBounds.extents.y)};
            }
            else // if (Left()) 
            {
                Bounds boxBounds = hitbox.bounds;
                extraHitbox.points = new Vector2[] {new Vector2(boxBounds.center.x - boxBounds.extents.x, boxBounds.center.y + boxBounds.extents.y),
                    new Vector2(boxBounds.center.x - boxBounds.extents.x, boxBounds.center.y - boxBounds.extents.y)};
            }
        }
        extraHitbox.isTrigger = false;
        */
        hitbox.isTrigger = true;
    }

    bool Up()
    {
        return blockDirection.ToUpper().Contains("UP");
    }
    bool Right()
    {
        return blockDirection.ToUpper().Contains("RIGHT");
    }
    bool Down()
    {
        return blockDirection.ToUpper().Contains("DOWN");
    }
    bool Left()
    {
        return blockDirection.ToUpper().Contains("LEFT");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.Player
            && PlayerDirectionMatches())
            hitbox.isTrigger = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.instance.Player)
            hitbox.isTrigger = true;
    }

    private bool PlayerDirectionMatches()
    {
        Vector3 ppos = GameManager.instance.Player.transform.position;
        float pw = GameManager.instance.Player.GetComponent<BoxCollider2D>().bounds.size.x / 2f;
        float ph = GameManager.instance.Player.GetComponent<BoxCollider2D>().bounds.size.y / 2f;
        Vector3 tpos = transform.position;
        if (Up())
        {
            if (ppos.y > tpos.y // see if its up
                && ((ppos.x - pw <= tpos.x + (hitbox.bounds.size.x / 2f)// see if its actually up
                && ppos.x + pw >= tpos.x - (hitbox.bounds.size.x / 2f))
                || (ppos.x <= tpos.x + (hitbox.bounds.size.x / 2f)
                && ppos.x >= tpos.x - (hitbox.bounds.size.x / 2f))))
                return true;
        } 
        if (Right())
        {
            if (ppos.x > tpos.x // see if its to the right
                && ((ppos.y - ph <= tpos.y + (hitbox.bounds.size.y / 2f)
                && ppos.y + ph >= tpos.y - (hitbox.bounds.size.y / 2f))
                || (ppos.y <= tpos.y + (hitbox.bounds.size.y / 2f)
                && ppos.y >= tpos.y - (hitbox.bounds.size.y / 2f)))) // see if its actually to the right;
                return true;
        } 
        if (Down())
        {
            if (ppos.y < tpos.y // see if its down
                && (ppos.x - pw <= tpos.x + (hitbox.bounds.size.x / 2f)
                && ppos.x + pw >= tpos.x - (hitbox.bounds.size.x / 2f)
                || (ppos.x <= tpos.x + (hitbox.bounds.size.x / 2f)
                && ppos.x >= tpos.x - (hitbox.bounds.size.x / 2f)))) // see if its actually down
                return true;
        } 
        if (Left())
        {
            if (ppos.x < tpos.x // see if its to the left
                && ((ppos.y - ph <= tpos.y + (hitbox.bounds.size.y / 2f)
                && ppos.y + ph >= tpos.y - (hitbox.bounds.size.y / 2f))
                || (ppos.y <= tpos.y + (hitbox.bounds.size.y / 2f)
                && ppos.y >= tpos.y - (hitbox.bounds.size.y / 2f)))) // see if its actually to the left
                return true;
        } 
        if (!Up() && !Down() && !Right() && !Left())
            Debug.LogError("One Way Collider '" + name + "' does not have a valid direction (" + blockDirection + ")");
        return false;
    }

    /*

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
    */
}
