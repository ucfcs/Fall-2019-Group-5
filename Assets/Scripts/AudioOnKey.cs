using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Audio/Audio -> On Key")]

public class AudioOnKey : Audio
{
    public KeyCode key;
    private void Update()

    {
        if(Input.GetKeyDown(key))
            Play();
    }
}
