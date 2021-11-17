using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCannon : Weapon
{
    public OrbitalCannon()
    {
        name = "Orbital Cannon";
        desciption = "ima firin ma lazar";
        element = Element.ELECTRIC;
        ranged = true;
        rhythm = false;

        lightStartup = 60;
        lightActive = 40;
        lightEndlag = 40;

        heavyStartup = 100;
        heavyActive = 40;
        heavyEndlag = 60;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
