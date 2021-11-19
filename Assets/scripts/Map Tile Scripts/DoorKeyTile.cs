using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyTile : BasicTile
{
    // Public set for door, set it to a door and it handles the rest
    public GameObject door;
    protected override void Start()
    {
        base.Start();
        hitbox.isTrigger = true;
    }

    // Update is called once per frame
    // But I call my mom every week to say hi
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == GameManager.instance.Player && door != null)
        {
            door.GetComponent<LockedDoorTile>().Unlock();
            gameObject.SetActive(false);
        } else
        {
            if (door == null)
            {
                throw new System.Exception("Door Not Defined for Key {"+name+"}");
            }
        }
    }
}
