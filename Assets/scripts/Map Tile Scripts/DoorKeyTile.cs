using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyTile : BasicTile
{

    protected override void Start()
    {
        base.Start();
        hitbox.isTrigger = true;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<TileTriggerInstruct>() != null)
        {
            col.gameObject.GetComponent<TileTriggerInstruct>().getKey();
            gameObject.SetActive(false);
        }
    }
}
