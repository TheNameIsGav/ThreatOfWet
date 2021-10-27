using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorTile : BasicTile
{
    bool locked;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        locked = true;
    }

    // Update is called once per frame
    public virtual void UpdateBehavior(Collider2D col, bool exitingCollider)
    {
        Debug.Log("Door Locked Try ");
        if (!exitingCollider &&
            locked &&
            col.gameObject.GetComponent<TileTriggerInstruct>() != null && 
            col.gameObject.GetComponent<TileTriggerInstruct>().hasKey())
        {
            hitbox.enabled = false;
            locked = false;
        }
    }
}
