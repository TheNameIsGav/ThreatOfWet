using UnityEngine;

public class DirectionCollisionTile : BasicTile
{
    public string blockDirection;

    protected override void Start()
    {
        base.Start();
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
                && (ppos.x < tpos.x + (hitbox.bounds.size.x / 2f)
                && ppos.x > tpos.x - (hitbox.bounds.size.x / 2f)))
                return true;
        } 
        if (Right())
        {
            if (ppos.x > tpos.x // see if its to the right
                && (ppos.y < tpos.y + (hitbox.bounds.size.y / 2f)
                && ppos.y > tpos.y - (hitbox.bounds.size.y / 2f)))
                return true;
        } 
        if (Down())
        {
            if (ppos.y < tpos.y // see if its down
                && (ppos.x < tpos.x + (hitbox.bounds.size.x / 2f)
                && ppos.x > tpos.x - (hitbox.bounds.size.x / 2f)))
                return true;
        } 
        if (Left())
        {
            if (ppos.x < tpos.x // see if its to the left
                && (ppos.y < tpos.y + (hitbox.bounds.size.y / 2f)
                && ppos.y > tpos.y - (hitbox.bounds.size.y / 2f)))
                return true;
        } 
        if (!Up() && !Down() && !Right() && !Left())
            Debug.LogError("One Way Collider '" + name + "' does not have a valid direction (" + blockDirection + ")");
        return false;
    }
}
