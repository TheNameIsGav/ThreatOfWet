using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorTile : BasicTile
{
    bool locked;
    BoxCollider2D triggerCollider;
    // Start is called before the first frame update but after I want to cry
    protected override void Start()
    {
        base.Start();
        locked = true;
        triggerCollider = gameObject.AddComponent<BoxCollider2D>();
        triggerCollider.isTrigger = true;
        triggerCollider.size = new Vector2(1.2f, 1.2f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (locked &&
            col.gameObject.GetComponent<TileTriggerInstruct>() != null &&
            col.gameObject.GetComponent<TileTriggerInstruct>().hasKey())
        {
            hitbox.enabled = false;
            triggerCollider.enabled = false;
            locked = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(219f, 172f, 148f);
        }
    }
    // 161, 93, 59 is the rgb 
}
