using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTile : BasicTile
{
    public int progression;
    protected GameObject player;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hitbox.isTrigger = true;
        player = GameObject.Find("Player");
    }

    public Vector2 getSpawn()
    {
        return transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            if (player.GetComponent<MasonPlayerChanges>().spawnProgressed(progression)) 
            { 
                player.GetComponent<MasonPlayerChanges>().SetSpawn(GetComponent<CheckpointTile>());
                hitbox.enabled = false;
            }
        }
    }
}
