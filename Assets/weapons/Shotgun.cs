using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public Shotgun()
    {
        name = "Shotgun";
        desciption = "shotgun rhinoplasty";
        element = Element.DEFAULT;
        ranged = true;
        rhythm = false;

        lightStartup = 14;
        lightActive = 8;
        lightEndlag = 10;

        heavyStartup = 24;
        heavyActive = 8;
        heavyEndlag = 20;

        damageBase = 70f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
