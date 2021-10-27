using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTriggerInstruct : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() == null)
            return;
        else
            if (collision.gameObject.GetComponent<BasicTile>() == null)
            return;
        collision.gameObject.GetComponent<BasicTile>().updateBehavior(collision, false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() == null)
            return;
        else
            if (collision.gameObject.GetComponent<BasicTile>() == null)
            return;
        collision.gameObject.GetComponent<BasicTile>().updateBehavior(collision, true);
    }
}
