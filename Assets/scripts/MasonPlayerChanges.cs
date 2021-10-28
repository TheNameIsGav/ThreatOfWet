using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasonPlayerChanges : MonoBehaviour
{
    CheckpointTile currSpawn;

    public void SetSpawn(CheckpointTile spawn)
    {
        currSpawn = spawn;
        Debug.Log("New Spawn: "+currSpawn);
    }

    bool isDead()
    {
        return transform.position.y < -10;
    }

    public bool spawnProgressed(int progression)
    {
        return currSpawn.progression < progression;
    }

    void Update()
    {
        if (isDead()) // RESPAWN
        {
            transform.position 
                = currSpawn.getSpawn();
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
