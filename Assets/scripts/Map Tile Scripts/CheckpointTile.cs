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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.name.Equals("Player"))
            Debug.Log("Checkpoint Reached! "+progression);
        */
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Equals("Player"))
        {
            if (player.GetComponent<MasonPlayerChanges>().spawnProgressed(progression)) 
            { 
                Debug.Log("Checkpoint Reached!");
                player.GetComponent<MasonPlayerChanges>().SetSpawn(GetComponent<CheckpointTile>());
                hitbox.enabled = false;
            }
        }
    }
}
