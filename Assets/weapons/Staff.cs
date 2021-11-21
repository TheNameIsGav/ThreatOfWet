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
        lightActive = 8;
        lightEndlag = 6;

        heavyStartup = 20;
        heavyActive = 24;
        heavyEndlag = 20;

        damageBase = 35f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
