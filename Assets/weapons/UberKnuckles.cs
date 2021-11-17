using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UberKnuckles : Weapon
{
    public UberKnuckles()
    {
        name = "Uber Knuckles";
        desciption = "hands for everyone";
        element = Element.ELECTRIC;
        ranged = false;
        rhythm = false;

        lightStartup = 2;
        lightActive = 2;
        lightEndlag = 2;

        heavyStartup = 8;
        heavyActive = 8;
        heavyEndlag = 8;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
