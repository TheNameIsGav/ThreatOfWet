using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyTile : BasicTile
{

    void Start()
    {
        base.Start();
        hitbox.isTrigger = true;
    }
    // Start is called before the first frame update
    public virtual void UpdateBehavior(Collider2D col, bool exitingCollider)
    {
        Debug.Log("Key Collided");
        if (!exitingCollider && col.gameObject.GetComponent<TileTriggerInstruct>() != null)
        {
            col.gameObject.GetComponent<TileTriggerInstruct>().getKey();
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
