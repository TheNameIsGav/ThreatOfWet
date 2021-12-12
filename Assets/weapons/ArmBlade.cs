using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBlade : Weapon
{
    public ArmBlade()
    {
        name = "Arm Blade";
        desciption = "stand back im armed";
        element = Element.DEFAULT;
        ranged = false;
        rhythm = false;

        lightStartup = 4;
        lightActive = 3;
        lightEndlag = 4;

        heavyStartup = 16;
        heavyActive = 3;
        heavyEndlag = 12;

        damageBase = 30f;
    }

}
