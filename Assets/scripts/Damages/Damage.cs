using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    float val;
    public float Value { get { return val; } set { val = value; } }

    Element elem;
    public Element Elem { get { return elem; } set { elem = value; } }

    bool isBleed;
    public bool Bleed { get { return isBleed; } set { isBleed = value; } }

    bool canCrit;
    public bool Crit { get { return canCrit; } set { canCrit = value; } }

    float critDamage;
    public float CritDam { get { return critDamage; } set { critDamage = value; } }

    bool onFire;
    public bool Fire { get { return onFire; } set { onFire = value; } }

    bool isNecrotic;
    public bool Necrotic { get { return isNecrotic; } set { isNecrotic = value; } }

    float critPercent;
    public float CritPercent { get { return critPercent; } set { critPercent = value; } }

    bool ranged;
    public bool Ranged { get { return ranged; } set { ranged = value; } }

    bool healing;
    public bool Heal { get { return healing; } set { healing = value; } }

    bool charged;
    public bool Charge { get { return charged; } set { charged = value; } }

    bool heavy;
    public bool Heavy { get { return heavy; } set { heavy = value; } }

    /// <summary>
    /// Constructor for a damage
    /// </summary>
    /// <param name="value"></param>
    public Damage(float value) { val = value; }

    /// <summary>
    /// Constructor for a damage that can crit
    /// </summary>
    /// <param name="value"></param>
    /// <param name="cC"></param>
    /// <param name="cD"></param>
    public Damage(float value, bool cC, float cD, float cP) { val = value; canCrit = cC; critDamage = cD; }

    /// <summary>
    /// Constructor for a damage that has one or more status effects with it
    /// </summary>
    /// <param name="value"></param>
    /// <param name="bleed"></param>
    /// <param name="fire"></param>
    /// <param name="necro"></param>
    public Damage(float value, bool bleed, bool fire, bool necro)
    {
        val = value;
        isBleed = bleed;
        onFire = fire;
        isNecrotic = necro;
    }

    /// <summary>
    /// Constructor for a damage that has one or more status effects and can crit
    /// </summary>
    /// <param name="value"></param>
    /// <param name="cC"></param>
    /// <param name="cD"></param>
    /// <param name="bleed"></param>
    /// <param name="fire"></param>
    /// <param name="necro"></param>
    public Damage(float value, bool cC, float cD, float cP, bool bleed, bool fire, bool necro)
    {
        val = value;
        isBleed = bleed;
        onFire = fire;
        isNecrotic = necro;
        canCrit = cC;
        critDamage = cD;
        critPercent = cP;
    }

    public float getDamage()
    {
        if (canCrit)
        {
            float cDam = critPercent > 100 ? critDamage + ((critPercent - 100)/100) : critDamage;
            if(Random.Range(0, 100) < critPercent) { return val * cDam; }
            else { return val; }
        } else
        {
            return val;
        }
    }
}
