using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterGun : Weapon
{
    public StarterGun()
    {
        name = "Starter Gun";
        desciption = "i keep that mf thang on me";
        element = Element.DEFAULT;
        ranged = true;
        rhythm = false;

        lightStartup = 6;
        lightActive = 3;
        lightEndlag = 6;

        heavyStartup = 16;
        heavyActive = 3;
        heavyEndlag = 12;

        damageBase = 10f;
    }
}
