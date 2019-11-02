using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Audio -> On Start")]

public class AudioOnStart : Audio
{
    private void Start()
    {
        Play(); 
    }
}
