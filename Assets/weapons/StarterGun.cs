using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterGun : Weapon
{
    public StarterGun()
    {
        name = "Starter Gun";
        desciption = "i keep that mf thang on me";
        element = Element.DEFAULT;
        ranged = true;
        rhythm = false;

        lightStartup = 6;
        lightActive = 4;
        lightEndlag = 6;

        heavyStartup = 16;
        heavyActive = 4;
        heavyEndlag = 12;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
