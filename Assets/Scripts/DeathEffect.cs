using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip deathSound;
    void Start()
    {
        audioSource.PlayOneShot(deathSound, 0.2f);

    }


}
