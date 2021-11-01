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
    new public string name = "Starter Sword";
    new public string desciption = "The sword that you start out with, i donno seems pretty basic";
    //this determines weather or not the weapon is ranged or melee
    new public bool ranged = false;
    //this is if its a rhythm weapon, rhythm is true, non-rhythm is false
    new public bool rhythm = false;
    //these are the timing values for the light attack, in beats if rhythm, in fixed updates if not
    new public int lightStartup = 6;
    new public int lightActive = 3;
    new public int lightEndlag = 6;
    //these are the timing values for the light attack, in beats if rhythm, in fixed updates if not
    new public int heavyStartup = 14;
    new public int heavyActive = 4;
    new public int heavyEndlag = 14;
    //these store the sprite and animations for the weapon (if specific animations end up being used)
    new public Animation lightSwing;
    new public Animation heavySwing;
    new public Sprite weaponSprite;

    public StarterSword()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
