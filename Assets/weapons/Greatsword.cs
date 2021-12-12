using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greatsword : Weapon
{
    public Greatsword()
    {
        name = "War Hammer";
        desciption = "stop, hammer time";
        element = Element.DEFAULT;
        ranged = false;
        rhythm = false;

        lightStartup = 16;
        lightActive = 3;
        lightEndlag = 14;

        heavyStartup = 28;
        heavyActive = 3;
        heavyEndlag = 28;

        damageBase = 70f;
    }
}
