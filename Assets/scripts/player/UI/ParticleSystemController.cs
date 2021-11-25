using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    private static ParticleSystem system;
    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
    }

    public static void scaleSystem(float amt)
    {
        var emission = system.emission;
        emission.rateOverTime = 10 * amt;

        var velocityOverLifetime = system.velocityOverLifetime;
        velocityOverLifetime.speedModifier = amt * 2;

        var mainSystem = system.main;
        mainSystem.startLifetime = Mathf.Max(1.0f - (.05f * (amt/10)), .5f);
    }

    public static void ToggleSystem(bool val)
    {
        system.gameObject.SetActive(val);
    }
}
