using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : BasicTile
{
    public Sprite openedSprite;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // loot is either predefined or grab method will be grabbed or it will be generated upon retrieval
        //hitbox.isTrigger = true;
    }

    bool isOpened()
    {
        return !hitbox.enabled;
    }

    bool isClosed()
    {
        return hitbox.enabled;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            //gameObject.GetComponent<SpriteRenderer>().sprite = openedSprite;
            //hitbox.enabled = false;
        }
    }
}
