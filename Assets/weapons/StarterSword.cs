using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterSword : Weapon
{
    enum Elements { Default, Fire, Eletric, Water, Earth }
    Elements element = Elements.Default;
    //these are in unity units, if the weapon is ranged then the height is the range of the weapon, and the width is how wide the beam is 
    //public float hitboxWidth = 2f;
    //public float hitboxHeight = 2f;
    //these are self explanitory
    //this determines weather or not the weapon is ranged or melee
    //new public bool ranged = false;
    //this is if its a rhythm weapon, rhythm is true, non-rhythm is false
    //new public bool rhythm = false;
    //these are the timing values for the light attack, in beats if rhythm, in fixed updates if not
    //new public int lightStartup = 8;
    //new public int lightActive = 60;
    //new public int lightEndlag = 8;
    //these are the timing values for the light attack, in beats if rhythm, in fixed updates if not
   // heavyStartup = 30;
    //heavyActive = 60;
    //heavyEndlag = 30;
    //these store the sprite and animations for the weapon (if specific animations end up being used)
    new public Animation lightSwing;
    new public Animation heavySwing;
    new public Sprite weaponSprite;

    public StarterSword()
    {
        name = "Starter Sword";
        desciption = "The sword that you start out with, i donno seems pretty basic";
        ranged = false;
        rhythm = false;

        lightStartup = 8;
        lightActive = 4;
        lightEndlag = 8;

        heavyStartup = 20;
        heavyActive = 8;
        heavyEndlag = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
