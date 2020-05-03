using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip stab, shoot, hurt;
    static AudioSource audioSrc;

    private void Start()
    {
        stab = Resources.Load<AudioClip>("Knife");
        shoot = Resources.Load<AudioClip>("Gunshot");
        hurt = Resources.Load<AudioClip>("Hurt");
        audioSrc = GetComponent<AudioSource>();

    }

    public static void playSound(string clip)
    {
        switch(clip)
        {
            case "Knife":
                audioSrc.PlayOneShot(stab);
                break;
            case "Shoot":
                audioSrc.PlayOneShot(shoot);
                break;
            case "Hurt":
                audioSrc.PlayOneShot(hurt);
                break;

        }
    }
}
