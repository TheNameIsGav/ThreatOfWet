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
        playerController.instance.grounded = true;
        if(!State.ReferenceEquals(playerController.instance.state.GetType(), new DashState()) && playerController.instance.rbs.velocity.y < 1f)
        {
            playerController.instance.canDash = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController.instance.grounded = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerController.instance.grounded = true;
        if (!State.ReferenceEquals(playerController.instance.state.GetType(), new DashState()) && playerController.instance.rbs.velocity.y < 1f)
        {
            playerController.instance.canDash = true;
        }
    }
}
