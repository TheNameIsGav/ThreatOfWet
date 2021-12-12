using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    public Revolver()
    {
        name = "Revolver";
        desciption = "are ya feelin lucky punk";
        element = Element.DEFAULT;
        ranged = true;
        rhythm = false;

        lightStartup = 8;
        lightActive = 3;
        lightEndlag = 8;

        heavyStartup = 18;
        heavyActive = 3;
        heavyEndlag = 10;

        damageBase = 40f;
    }
}
