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
            if (!collision.gameObject.CompareTag("Slippery") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
            {
                if (collision.gameObject.transform.position.y - ((collision.gameObject.GetComponent<BoxCollider2D>().size.y / 2) /* * Mathf.Sign(collision.gameObject.transform.position.y)*/) < (playerController.instance.transform.position.y + ((playerController.instance.transform.localScale.y / 2) /* * Mathf.Sign(playerController.instance.transform.position.y)*/)))
                {
                    playerController.instance.grounded = true;
                    playerController.instance.coyote = playerController.instance.universalBufferTime + 1;
                    //playerController.instance.state.shortHop = 0;
                    if (playerController.instance.state != playerController.instance.dash && playerController.instance.rbs.velocity.y < 1f && !(((playerController.instance.transform.position.x /*+ (playerController.instance.transform.localScale.x / 2)*/) <= (collision.gameObject.transform.position.x - (Mathf.Sign(collision.gameObject.transform.position.x) * collision.gameObject.GetComponent<BoxCollider2D>().size.x / 2))) || ((playerController.instance.transform.position.x /*- (playerController.instance.transform.localScale.x / 2)*/) >= (collision.gameObject.transform.position.x + (collision.gameObject.GetComponent<BoxCollider2D>().size.x / 2)))))
                    {
                    //Debug.Log("fucking aasdasdasd");
                    playerController.instance.wall = false;
                        playerController.instance.canDash = true;
                    }
                else
                {
                    playerController.instance.wall = true;
                }
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

            if (!collision.gameObject.CompareTag("Slippery") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
            {
                if (collision.gameObject.transform.position.y - ((collision.gameObject.GetComponent<BoxCollider2D>().size.y / 2) /* *Mathf.Sign(collision.gameObject.transform.position.y)*/) < (playerController.instance.transform.position.y + ((playerController.instance.transform.localScale.y / 2)/* * Mathf.Sign(playerController.instance.transform.position.y)*/)))
                {
                    playerController.instance.grounded = true;
                    playerController.instance.coyote = playerController.instance.universalBufferTime + 1;
                    //playerController.instance.state.shortHop = 0;
                    //Debug.Log((playerController.instance.transform.position.x).ToString() + " lt " + (collision.gameObject.transform.position.x - (collision.gameObject.transform.localScale.x / 2)).ToString() + " " + (playerController.instance.transform.position.x).ToString() + " gt " + (collision.gameObject.transform.position.x + (collision.gameObject.transform.localScale.x / 2)).ToString());
                    //Debug.Log((!((playerController.instance.transform.position.x /*+ (playerController.instance.transform.localScale.x / 2)*/) <= (collision.gameObject.transform.position.x - (collision.gameObject.transform.localScale.x / 2))) || ((playerController.instance.transform.position.x /*- (playerController.instance.transform.localScale.x / 2)*/) >= (collision.gameObject.transform.position.x + (collision.gameObject.transform.localScale.x / 2)))));
                    if (playerController.instance.state != playerController.instance.dash && playerController.instance.rbs.velocity.y < 1f && !(((playerController.instance.transform.position.x /*+ (playerController.instance.transform.localScale.x / 2)*/) <= (collision.gameObject.transform.position.x - (collision.gameObject.GetComponent<BoxCollider2D>().size.x / 2))) || ((playerController.instance.transform.position.x /*- (playerController.instance.transform.localScale.x / 2)*/) >= (collision.gameObject.transform.position.x + (collision.gameObject.GetComponent<BoxCollider2D>().size.x / 2)))))
                    {
                        //Debug.Log((playerController.instance.transform.position.x ).ToString() + " lt " + (collision.gameObject.transform.position.x - (collision.gameObject.transform.localScale.x / 2)).ToString() + " " + (playerController.instance.transform.position.x ).ToString() + " gt " + (collision.gameObject.transform.position.x + (collision.gameObject.transform.localScale.x / 2)).ToString());
                        //Debug.Log((!((playerController.instance.transform.position.x /*+ (playerController.instance.transform.localScale.x / 2)*/) <= (collision.gameObject.transform.position.x - (collision.gameObject.transform.localScale.x / 2))) || ((playerController.instance.transform.position.x /*- (playerController.instance.transform.localScale.x / 2)*/) >= (collision.gameObject.transform.position.x + (collision.gameObject.transform.localScale.x / 2)))));
                        //Debug.Log("tpggled");
                        playerController.instance.canDash = true;
                    playerController.instance.wall = false;
                }
                else
                {
                    playerController.instance.wall = true;
                    //playerController.instance.rbs.velocity = new Vector2(playerController.instance.rbs.velocity.x - (playerController.instance.dir * 15f), playerController.instance.rbs.velocity.y);
                }
                }
            }
             else if (!collision.gameObject.CompareTag("Slippery"))
        {
            playerController.instance.grounded = true;
            playerController.instance.coyote = playerController.instance.universalBufferTime + 1;
        }

    }
}