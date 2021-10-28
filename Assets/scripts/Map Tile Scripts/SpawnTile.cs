using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : CheckpointTile
{
    //private override int progression;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        progression = 0;

        player.GetComponent<MasonPlayerChanges>().SetSpawn(this.GetComponent<SpawnTile>());
        player.transform.position = transform.position;
    }
}
