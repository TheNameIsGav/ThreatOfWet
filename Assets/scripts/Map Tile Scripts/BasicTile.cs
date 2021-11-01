using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTile : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite sprite;
    protected Collider2D hitbox;

    protected virtual void Start()
    {
        hitbox = GetComponent<Collider2D>();
        if (hitbox == null)
            hitbox = this.gameObject.AddComponent<BoxCollider2D>();
    }

    public virtual void updateBehavior(Collider2D col, bool exitingCollider)
    {
        if (exitingCollider)
            updateExitBehavior(col);
        else
            updateEnterBehavior(col);
    }

    protected virtual void updateExitBehavior(Collider2D col)
    {
        // Does nothing, unless common behavior is required
    }

    protected virtual void updateEnterBehavior(Collider2D col)
    {
        // Does nothing, unless common behavior is required
    }
}
