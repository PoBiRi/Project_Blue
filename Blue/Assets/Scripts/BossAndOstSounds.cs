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
    private AudioSource pauseSource;

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
        pauseSource = GetComponents<AudioSource>()[5];
        MainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuManage.isGameStart && !isMusic)
        {
            OstSource.clip = audioClips[MenuManage.BossNum];
            OstSource.Play();
            pauseSource.clip = audioClips[MenuManage.BossNum + 15];
            pauseSource.Play();
            pauseSource.mute = true;
            ambienceSource.clip = audioClips[8];
            ambienceSource.Play();
            isMusic = true;
        }
        if (!MenuManage.isGameStart && isMusic)
        {
            OstSource.Stop();
            OstSource.mute = false;
            ambienceSource.Stop();
            pauseSource.Stop();
            pauseSource.mute = true;
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
        instance.ambienceSource.Stop();
        instance.OstSource.mute = false;
        instance.pauseSource.Stop();
        instance.pauseSource.mute = true;
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
        return instance.audioClips[10].length;
    }
    public static float GameClear()
    {
        instance.ambienceSource.clip = instance.audioClips[13];
        instance.ambienceSource.Play();
        return instance.audioClips[13].length;
    }
    public static void ClapForFlyers()
    {
        instance.ambienceSource.PlayOneShot(instance.audioClips[9]);
        //instance.ambienceSource.clip = instance.audioClips[9];
        //instance.ambienceSource.Play();
    }
    public static float Scripts()
    {
        //instance.audioSource.clip = instance.audioClips[14];
        //instance.audioSource.Play();
        instance.audioSource.PlayOneShot(instance.audioClips[14]);
        return instance.audioClips[14].length;
    }
    public static float BossDragged()
    {
        instance.audioSource.PlayOneShot(instance.audioClips[12]);
        return instance.audioClips[12].length;
    }
    public static void Pause()
    {
        instance.OstSource.mute = true;
        instance.pauseSource.mute = false;
    }
    public static void Resume()
    {
        instance.pauseSource.mute = true;
        instance.OstSource.mute = false;
    }
}
