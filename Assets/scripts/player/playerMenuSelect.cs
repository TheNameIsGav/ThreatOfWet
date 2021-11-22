using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMenuSelect : MonoBehaviour
{
    public SpriteRenderer select;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        select.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.instance.menu.index == index)
        {
            select.enabled = true;
        }
        else
        {
            select.enabled = false;
        }
    }
}
