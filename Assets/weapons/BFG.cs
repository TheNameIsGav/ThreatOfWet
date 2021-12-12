using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFG : Weapon
{
    public BFG()
    {
        name = "BFG";
        desciption = "you know what it stands for";
        element = Element.FIRE;
        ranged = true;
        rhythm = false;

        lightStartup = 40;
        lightActive = 3;
        lightEndlag = 40;

        heavyStartup = 80;
        heavyActive = 3;
        heavyEndlag = 60;

        damageBase = 200f;

    }
}
