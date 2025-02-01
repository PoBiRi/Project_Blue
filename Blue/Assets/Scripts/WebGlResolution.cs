using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebGlResolution : MonoBehaviour
{
    void Start()
    {
        int width = Screen.width;
        int height = Mathf.RoundToInt(width * 9f / 16f);

        Screen.SetResolution(width, height, false);
    }
}
