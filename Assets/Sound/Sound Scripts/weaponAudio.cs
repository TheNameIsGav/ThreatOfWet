using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponAudio : MonoBehaviour
{

    [SerializeField]
    AudioClip[] LightWepSounds;

    [SerializeField]
    AudioClip[] HeavyWepSounds;

    GameObject mine;

    private void Start()
    {
        mine = gameObject;
    }

    public void SetAndPlaySound(int sound, bool heavy)
    {
        if (heavy)
        {
            mine.GetComponent<AudioSource>().clip = HeavyWepSounds[sound];
        } else
        {
            mine.GetComponent<AudioSource>().clip = LightWepSounds[sound];
        }
        mine.GetComponent<AudioSource>().Play();
    }   
}
