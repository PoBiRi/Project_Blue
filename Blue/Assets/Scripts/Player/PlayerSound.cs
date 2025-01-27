using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private static PlayerSound instance;
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(audioSource == null)
        {
            audioSource = GetComponents<AudioSource>()[1];
        }
    }

    public static void DashSound()
    {
        instance.audioSource.PlayOneShot(instance.audioClips[0]);
    }

    public static void GunSound()
    {
        int randomIndex = Random.Range(1, 4);
        instance.audioSource.PlayOneShot(instance.audioClips[randomIndex]);
    }

    public static void HitSound()
    {
        int randomIndex = Random.Range(4, 6);
        instance.audioSource.PlayOneShot(instance.audioClips[randomIndex]);
    }

    public static void MaleAttack()
    {
        instance.audioSource.PlayOneShot(instance.audioClips[6]);
    }
    public static void BulletDestroy()
    {
        instance.audioSource.PlayOneShot(instance.audioClips[7]);
    }
}
