using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Weapon
{
    public SniperRifle()
    {
        name = "Sniper Rifle";
        desciption = "im not a crazed gunman im an assassin";
        element = Element.DEFAULT;
        ranged = true;
        rhythm = false;

        lightStartup = 16;
        lightActive = 10;
        lightEndlag = 14;

        heavyStartup = 26;
        heavyActive = 10;
        heavyEndlag = 24;

        damageBase = 75f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
