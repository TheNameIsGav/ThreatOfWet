using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    public Staff()
    {
        name = "Staff";
        desciption = "sticky situation";
        element = Element.DEFAULT;
        ranged = false;
        rhythm = false;

        lightStartup = 8;
        lightActive = 3;
        lightEndlag = 6;

        heavyStartup = 20;
        heavyActive = 3;
        heavyEndlag = 20;

        damageBase = 35f;
    }
}
