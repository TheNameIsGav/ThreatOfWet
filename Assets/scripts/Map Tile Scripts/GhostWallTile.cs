using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWallTile : BasicTile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hitbox.isTrigger = true;
    }
}
