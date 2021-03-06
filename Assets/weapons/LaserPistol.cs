using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPistol : Weapon
{
    public LaserPistol()
    {
        name = "Laser Pistol";
        desciption = "a good blaster at your side";
        element = Element.ELECTRIC;
        ranged = true;
        rhythm = false;

        lightStartup = 10;
        lightActive = 3;
        lightEndlag = 8;

        heavyStartup = 18;
        heavyActive = 3;
        heavyEndlag = 14;

        damageBase = 15f;
    }
}
