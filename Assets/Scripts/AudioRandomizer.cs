using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Audio -> Randomizer")]

public class AudioRandomizer : Audio
{

    public AudioElement[] AudioSelection;

    public float AudioRate = 1f;

    private void Start()
    {
        InvokeRepeating("PlayRandomAudio", 0.2f, AudioRate);
    }


    private void PlayRandomAudio()
    {    
        

        AudioElement = AudioSelection[System.DateTime.Now.Millisecond % AudioSelection.Length]; 

        Play();
    }
}
