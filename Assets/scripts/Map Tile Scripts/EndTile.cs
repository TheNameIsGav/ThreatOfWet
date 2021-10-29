using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTile : CheckpointTile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        progression = int.MaxValue;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            Debug.Log("Level Over! End Tile Reached");
            // collision.gameObject.GetComponent<playerControler>().nextLevel();
        }
    }
}
