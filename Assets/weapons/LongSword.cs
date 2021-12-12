using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword : Weapon
{
    public LongSword()
    {
        name = "Long Sword";
        desciption = "long boiye";
        element = Element.DEFAULT;
        ranged = false;
        rhythm = false;

        lightStartup = 12;
        lightActive = 3;
        lightEndlag = 10;

        heavyStartup = 24;
        heavyActive = 3;
        heavyEndlag = 24;
    }
}
