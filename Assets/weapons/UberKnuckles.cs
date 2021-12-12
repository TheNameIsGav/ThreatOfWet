using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UberKnuckles : Weapon
{
    public UberKnuckles()
    {
        name = "Uber Knuckles";
        desciption = "hands for everyone";
        element = Element.ELECTRIC;
        ranged = false;
        rhythm = false;

        lightStartup = 2;
        lightActive = 3;
        lightEndlag = 2;

        heavyStartup = 8;
        heavyActive = 3;
        heavyEndlag = 8;

        damageBase = 25f;
    }
}
