using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void MainGameStart()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
}
