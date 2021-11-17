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
    public float critDam { get { return critDamage; } set { critDamage = value; } }

    bool onFire;
    public bool Fire { get { return onFire; } set { onFire = value; } }

    bool isNecrotic;
    public bool Necrotic { get { return isNecrotic; } set { isNecrotic = value; } }

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
    public Damage(float value, bool cC, float cD) { val = value; canCrit = cC; critDamage = cD; }

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
    public Damage(float value, bool cC, float cD, bool bleed, bool fire, bool necro)
    {
        val = value;
        isBleed = bleed;
        onFire = fire;
        isNecrotic = necro;
        canCrit = cC;
        critDamage = cD;
    }

    public float getDamage()
    {
        if (canCrit)
        {
            return val + (val * critDamage);
        } else
        {
            return val;
        }
    }
}
