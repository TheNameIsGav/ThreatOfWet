using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
   
    //this gives the element of the weapon
    enum Elements { Default, Fire, Eletric, Water, Earth}
    Elements element = Elements.Default;
    //these are in unity units, if the weapon is ranged then the height is the range of the weapon, and the width is how wide the beam is 
    public float hitboxWidth = 2f;
    public float hitboxHeight = 2f;
    //these are self explanitory
    public string name;
    public string desciption;
    //this determines weather or not the weapon is ranged or melee
    public bool ranged;
    //this is if its a rhythm weapon, rhythm is true, non-rhythm is false
    public bool rhythm;
    //these are the timing values for the light attack, in beats if rhythm, in fixed updates if not
    public int lightStartup;
    public int lightActive;
    public int lightEndlag;
    //these are the timing values for the light attack, in beats if rhythm, in fixed updates if not
    public int heavyStartup;
    public int heavyActive;
    public int heavyEndlag;
    //these store the sprite and animations for the weapon (if specific animations end up being used)
    public Animation lightSwing;
    public Animation heavySwing;
    public Sprite weaponSprite;



    // Update is called once per frame
}
