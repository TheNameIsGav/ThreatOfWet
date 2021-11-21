using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortSword : Weapon
{
    public ShortSword()
    {
        name = "Short Sword";
        desciption = "me when woamn ive been following for three miles at night stabs me 32 times in the chest";
        element = Element.DEFAULT;
        ranged = false;
        rhythm = false;

        lightStartup = 4;
        lightActive = 3;
        lightEndlag = 3;

        heavyStartup = 12;
        heavyActive = 10;
        heavyEndlag = 12;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
