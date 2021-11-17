using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingGun : Weapon
{
    public GatlingGun()
    {
        name = "Gatling Gun";
        desciption = "needs more dakka";
        element = Element.DEFAULT;
        ranged = true;
        rhythm = false;

        lightStartup = 2;
        lightActive = 1;
        lightEndlag = 2;

        heavyStartup = 14;
        heavyActive = 20;
        heavyEndlag = 14;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
