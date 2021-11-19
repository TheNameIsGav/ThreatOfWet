using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAssault : Weapon
{
    public LaserAssault()
    {
        name = "LaserAssaultRifle";
        desciption = "for the republic";
        element = Element.FIRE;
        ranged = true;
        rhythm = false;

        lightStartup = 4;
        lightActive = 3;
        lightEndlag = 4;

        heavyStartup = 14;
        heavyActive = 3;
        heavyEndlag = 12;

        damageBase = 20f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
