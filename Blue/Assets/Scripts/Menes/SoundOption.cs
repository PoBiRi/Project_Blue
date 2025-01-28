using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    public Slider SFX;
    public Slider Sounds;

    private AudioSource[] AudioSources;

    // Start is called before the first frame update
    void Start()
    {
        AudioSources = GameObject.Find("Sounds").GetComponents<AudioSource>();
    }

    public void ChangeSFX()
    {
        float value = SFX.value;
        AudioSources[0].volume = value;
        AudioSources[1].volume = value;
        AudioSources[2].volume = value;
    }

    public void ChangeSounds()
    {
        float value = Sounds.value;
        AudioSources[3].volume = value;
        AudioSources[4].volume = value;
    }
}
