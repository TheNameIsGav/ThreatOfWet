using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTile : MonoBehaviour
{
    // Start is called before the first frame update
    protected Collider2D hitbox;

    protected virtual void Start()
    {
        hitbox = GetComponent<Collider2D>();
        if (hitbox == null)
            hitbox = this.gameObject.AddComponent<BoxCollider2D>();
    }

    public virtual void updateBehavior(Collider2D col, Collider2D other, bool exitingCollider)
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

    public bool CanSpawnEnemy()
    {
        // Physics2D.OverlapBoxAll(transform.position - new Vector3(.5f, 0, 0), new Vector2(1, 2), LayerMask.GetMask("Spawnable Tile"));
        Collider2D[] regularTiles = Physics2D.OverlapBoxAll(transform.position - new Vector3(.5f, 0, 0), 
            new Vector2(1, 2), LayerMask.GetMask("Tile"));
        Collider2D[] spawnableTiles = Physics2D.OverlapBoxAll(transform.position - new Vector3(.5f, 0, 0), 
            new Vector2(1, 2), LayerMask.GetMask("Spawnable Tile"));
        return true;
        // return regularTiles.Length <= 1 && spawnableTiles.Length <= 1;
    }
}
