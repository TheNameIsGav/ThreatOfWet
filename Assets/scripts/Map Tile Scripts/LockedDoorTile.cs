using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorTile : BasicTile
{
    bool locked;
    BoxCollider2D triggerCollider;
    float elevate = 0f;
    Vector3 origPos;
    float height;

    // Start is called before the first frame update but after I want to cry
    protected override void Start()
    {
        base.Start();
        locked = true;
        triggerCollider = gameObject.AddComponent<BoxCollider2D>();
        triggerCollider.isTrigger = true;
        triggerCollider.size = new Vector2(1.2f, 1.2f);
        origPos = transform.position;
        height = hitbox.bounds.size.y;
    }

    void FixedUpdate()
    {
        if (!locked && elevate <= 5.5)
        {
            /*
            hitbox.enabled = false;
            triggerCollider.enabled = false;
            locked = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(219f, 172f, 148f);
            col.gameObject.GetComponent<TileTriggerInstruct>().useKey();
            */
            elevate += Time.fixedDeltaTime;
            if (elevate < 1)
            {
                // Do nothing, delay for half a second
            }
            else if (elevate < 3)
            {
                transform.position = origPos + new Vector3(0, (elevate - 1f) / 2f * height, 0);
            }
            else if (elevate < 4)
            {
                transform.position = origPos + new Vector3(0, height, 0);
            } // One More Delay for fun :)
            else
            {
                GameManager.instance.ResetCameraToPlayer();
                elevate = 5;
            }
        }
    }

    public void Unlock()
    {
        hitbox.enabled = false;
        triggerCollider.enabled = false;
        locked = false;
        GameManager.instance.ChangeCameraParent(this.gameObject);
            // gameObject.GetComponent<SpriteRenderer>().color = new Color(219f, 172f, 148f);
    }
    // 161, 93, 59 is the rgb 
}
