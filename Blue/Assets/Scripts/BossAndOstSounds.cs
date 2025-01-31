using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAndOstSounds : MonoBehaviour
{
    private static BossAndOstSounds instance;
    private bool isMusic = false;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private AudioSource OstSource;
    private AudioSource ambienceSource;

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
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponents<AudioSource>()[2];
        OstSource = GetComponents<AudioSource>()[3];
        ambienceSource = GetComponents<AudioSource>()[4];
        MainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuManage.isGameStart && !isMusic)
        {
            OstSource.clip = audioClips[MenuManage.BossNum];
            OstSource.Play();
            ambienceSource.clip = audioClips[8];
            ambienceSource.Play();
            isMusic = true;
        }
        if (!MenuManage.isGameStart && isMusic)
        {
            OstSource.Stop();
            ambienceSource.Stop();
            isMusic = false;
        }
    }

    public static void DefeatMenu()
    {
        instance.OstSource.clip = instance.audioClips[3];
        instance.OstSource.Play();
    }
    public static void StopOst()
    {
        instance.OstSource.Stop();
        instance.ambienceSource.Stop();
    }
    public static void MainMenu()
    {
        instance.OstSource.clip = instance.audioClips[11];
        instance.OstSource.Play();
    }
    public static void RageSound()
    {
        instance.audioSource.PlayOneShot(instance.audioClips[4]);
    }
    public static void HitSound()
    {
        int randomIndex = Random.Range(5, 8);
        instance.audioSource.PlayOneShot(instance.audioClips[randomIndex]);
    }
    public static float DrumForFlyers()
    {
        instance.OstSource.PlayOneShot(instance.audioClips[10]);
        instance.ambienceSource.PlayOneShot(instance.audioClips[9]);
        return instance.audioClips[10].length;
    }
    public static void ClapForFlyers()
    {
        instance.ambienceSource.PlayOneShot(instance.audioClips[9]);
        //instance.ambienceSource.clip = instance.audioClips[9];
        //instance.ambienceSource.Play();
    }
}
