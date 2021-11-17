using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    public Revolver()
    {
        name = "Revolver";
        desciption = "are ya feelin lucky punk";
        element = Element.DEFAULT;
        ranged = true;
        rhythm = false;

        lightStartup = 8;
        lightActive = 6;
        lightEndlag = 8;

        heavyStartup = 18;
        heavyActive = 6;
        heavyEndlag = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
