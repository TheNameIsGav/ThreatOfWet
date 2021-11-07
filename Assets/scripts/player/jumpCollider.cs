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
        if (collision.gameObject.transform.position.y - ((collision.gameObject.transform.localScale.y / 2) * Mathf.Sign(collision.gameObject.transform.position.y)) < (playerController.instance.transform.position.y + ((playerController.instance.transform.localScale.y / 2) * Mathf.Sign(playerController.instance.transform.position.y))))
        {
            playerController.instance.grounded = true;
            playerController.instance.coyote = playerController.instance.universalBufferTime + 1;
            //playerController.instance.state.shortHop = 0;
            if (playerController.instance.state != playerController.instance.dash && playerController.instance.rbs.velocity.y < 1f && !(((playerController.instance.transform.position.x /*+ (playerController.instance.transform.localScale.x / 2)*/) <= (collision.gameObject.transform.position.x - (collision.gameObject.transform.localScale.x / 2))) || ((playerController.instance.transform.position.x /*- (playerController.instance.transform.localScale.x / 2)*/) >= (collision.gameObject.transform.position.x + (collision.gameObject.transform.localScale.x / 2)))))
            {
                //Debug.Log("fucking aasdasdasd");
                playerController.instance.canDash = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //playerController.instance.grounded = false;
        if(playerController.instance.rbs.velocity.y < 1f)
        {
            playerController.instance.coyote = 0;
        }
        else
        {
            playerController.instance.coyote = playerController.instance.universalBufferTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.transform.position.y - ((collision.gameObject.transform.localScale.y / 2) *Mathf.Sign(collision.gameObject.transform.position.y)) < (playerController.instance.transform.position.y + ((playerController.instance.transform.localScale.y / 2) * Mathf.Sign(playerController.instance.transform.position.y))))
        {
        playerController.instance.grounded = true;
        playerController.instance.coyote = playerController.instance.universalBufferTime + 1;
            //playerController.instance.state.shortHop = 0;
            //Debug.Log((playerController.instance.transform.position.x).ToString() + " lt " + (collision.gameObject.transform.position.x - (collision.gameObject.transform.localScale.x / 2)).ToString() + " " + (playerController.instance.transform.position.x).ToString() + " gt " + (collision.gameObject.transform.position.x + (collision.gameObject.transform.localScale.x / 2)).ToString());
            //Debug.Log((!((playerController.instance.transform.position.x /*+ (playerController.instance.transform.localScale.x / 2)*/) <= (collision.gameObject.transform.position.x - (collision.gameObject.transform.localScale.x / 2))) || ((playerController.instance.transform.position.x /*- (playerController.instance.transform.localScale.x / 2)*/) >= (collision.gameObject.transform.position.x + (collision.gameObject.transform.localScale.x / 2)))));
            if (playerController.instance.state != playerController.instance.dash && playerController.instance.rbs.velocity.y < 1f && !(((playerController.instance.transform.position.x /*+ (playerController.instance.transform.localScale.x / 2)*/) <= (collision.gameObject.transform.position.x - (collision.gameObject.transform.localScale.x / 2))) || ((playerController.instance.transform.position.x /*- (playerController.instance.transform.localScale.x / 2)*/) >= (collision.gameObject.transform.position.x + (collision.gameObject.transform.localScale.x / 2)))))
        {
                //Debug.Log((playerController.instance.transform.position.x ).ToString() + " lt " + (collision.gameObject.transform.position.x - (collision.gameObject.transform.localScale.x / 2)).ToString() + " " + (playerController.instance.transform.position.x ).ToString() + " gt " + (collision.gameObject.transform.position.x + (collision.gameObject.transform.localScale.x / 2)).ToString());
                //Debug.Log((!((playerController.instance.transform.position.x /*+ (playerController.instance.transform.localScale.x / 2)*/) <= (collision.gameObject.transform.position.x - (collision.gameObject.transform.localScale.x / 2))) || ((playerController.instance.transform.position.x /*- (playerController.instance.transform.localScale.x / 2)*/) >= (collision.gameObject.transform.position.x + (collision.gameObject.transform.localScale.x / 2)))));
                //Debug.Log("tpggled");
            playerController.instance.canDash = true;    
        }
    }
}
}
