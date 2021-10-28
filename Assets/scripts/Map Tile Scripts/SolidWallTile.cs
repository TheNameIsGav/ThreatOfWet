using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidWallTile : BasicTile
{
    Rigidbody2D rgb;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (hitbox == null)
            hitbox = this.gameObject.AddComponent<BoxCollider2D>();
        // rgb = this.gameObject.AddComponent<Rigidbody2D>();
    }
}
