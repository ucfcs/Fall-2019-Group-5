using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Audio -> Randomizer Once")]

public class AudioRandomizerOnce : Audio
{

    public AudioElement[] AudioSelection;

    public float AudioRate = 1f;

    private void Start()
    {
        Invoke("PlayRandomAudio", AudioRate);
    }


    private void PlayRandomAudio()
    {    
        

        AudioElement = AudioSelection[System.DateTime.Now.Millisecond % AudioSelection.Length]; 

        Play();
    }
}
