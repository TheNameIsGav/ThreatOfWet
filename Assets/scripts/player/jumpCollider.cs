using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("this please god please");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            playerController.instance.item = true;
            //playerController.instance.items = collision.gameObject;
            playerController.instance.activeItem = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("Chest"))
        {
            playerController.instance.weapon = true;
            playerController.instance.activeChest = collision.gameObject;
        }
        //Debug.Log(collision.GetType().ToString());
        if (!collision.gameObject.CompareTag("Slippery"))
        {
            playerController.instance.grounded = true;
            playerController.instance.coyote = playerController.instance.universalBufferTime + 1;
            if (!playerController.instance.wall)
            {
                playerController.instance.canDash = true;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //playerController.instance.grounded = false;
        if (playerController.instance.rbs.velocity.y < 1f)
        {
            playerController.instance.coyote = 0;
        }
        else
        {
            playerController.instance.coyote = playerController.instance.universalBufferTime;
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            playerController.instance.item = false;
            //playerController.instance.items = null;
            playerController.instance.activeItem = null;
        }
        else if (collision.gameObject.CompareTag("Chest"))
        {
            playerController.instance.weapon = false;
            playerController.instance.activeChest = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            playerController.instance.item = true;
            //playerController.instance.items = collision.gameObject;
            playerController.instance.activeItem = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("Chest"))
        {
            playerController.instance.weapon = true;
            playerController.instance.activeChest = collision.gameObject;
        }

            if (!collision.gameObject.CompareTag("Slippery"))
            {
                playerController.instance.grounded = true;
                 playerController.instance.coyote = playerController.instance.universalBufferTime + 1;
                if (!playerController.instance.wall)
                {
                        playerController.instance.canDash = true;
                }
            }

    }
}