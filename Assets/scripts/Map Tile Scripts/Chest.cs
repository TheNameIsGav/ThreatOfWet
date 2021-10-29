using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : BasicTile
{
    System.Object loot;
    public Sprite openedSprite;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // loot is either predefined or grab method will be grabbed or it will be generated upon retrieval
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        hitbox.isTrigger = true;
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
            gainLoot(collision.gameObject);
            gameObject.GetComponent<SpriteRenderer>().sprite = openedSprite;
            hitbox.enabled = false;
        }
    }

    void gainLoot(GameObject player)
    {
        player.GetComponent<MasonPlayerChanges>().gainLoot(loot);
    }
}
