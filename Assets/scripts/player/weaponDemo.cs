using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponDemo : MonoBehaviour
{
    public int index;
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
        if(index == 0)
        {
            playerController.instance.meleeWeapon.element = Element.DEFAULT;
            playerController.instance.rangedWeapon.element = Element.DEFAULT;
        }
        else if(index == 1)
        {
            playerController.instance.meleeWeapon.element = Element.FIRE;
            playerController.instance.rangedWeapon.element = Element.FIRE;
        }
        else if (index == 2)
        {
            playerController.instance.meleeWeapon.element = Element.ELECTRIC;
            playerController.instance.rangedWeapon.element = Element.ELECTRIC;
        }
        else if (index == 3)
        {
            playerController.instance.meleeWeapon.element = Element.WATER;
            playerController.instance.rangedWeapon.element = Element.WATER;
        }
        else if (index == 4)
        {
            playerController.instance.meleeWeapon.element = Element.GROUND;
            playerController.instance.rangedWeapon.element = Element.GROUND;
        }
    }
}
