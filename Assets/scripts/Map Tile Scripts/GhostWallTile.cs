using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWallTile : BasicTile
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        hitbox.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
