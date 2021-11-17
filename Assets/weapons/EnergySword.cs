using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySword : Weapon
{
    public EnergySword()
    {
        name = "Energy Sword";
        desciption = "not a lightsaber";
        element = Element.ELECTRIC;
        ranged = false;
        rhythm = false;

        lightStartup = 10;
        lightActive = 6;
        lightEndlag = 8;

        heavyStartup = 20;
        heavyActive = 24;
        heavyEndlag = 18;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
