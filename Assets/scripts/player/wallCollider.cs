using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("there is wall");
        playerController.instance.wall = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("still wal");
        playerController.instance.wall = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("there is no wall");
        playerController.instance.wall = false;
    }
}
