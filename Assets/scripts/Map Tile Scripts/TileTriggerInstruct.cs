using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTriggerInstruct : MonoBehaviour
{
    bool keyGet = false;

    public void getKey()
    {
        keyGet = true;
    }

    public void useKey()
    {
        keyGet = false;
    }

    public bool hasKey()
    {
        return keyGet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() == null)
            return;
        else
            if (collision.gameObject.GetComponent<BasicTile>() == null)
            return;
        collision.gameObject.GetComponent<BasicTile>().updateBehavior(collision, GetComponent<Collider2D>(), false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() == null)
            return;
        else
            if (collision.gameObject.GetComponent<BasicTile>() == null)
            return;
        collision.gameObject.GetComponent<BasicTile>().updateBehavior(collision, GetComponent<Collider2D>(), true);
    }
}
