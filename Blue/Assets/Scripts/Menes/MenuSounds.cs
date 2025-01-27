using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    private static MenuSounds instance;
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
        if (audioSource == null)
        {
            audioSource = GetComponents<AudioSource>()[0];
        }
    }

    public static float MainGameStart()
    {
        instance.audioSource.PlayOneShot(instance.audioClips[0]);
        return instance.audioClips[0].length;
    }

    public void HoverSound()
    {
        instance.audioSource.PlayOneShot(instance.audioClips[1]);
    }

    public void ClickSound()
    {
        instance.audioSource.PlayOneShot(instance.audioClips[2]);
    }
    public static float PlayerDefeat()
    {
        instance.audioSource.PlayOneShot(instance.audioClips[3]);
        return instance.audioClips[3].length;
    }
}
