using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Audio --> Inherit From")]

public class Audio : MonoBehaviour
{

    [Header("Audio")]
    public AudioElement AudioElement;
    public AudioManager Manager;
    [ContextMenu("Play")]


    protected void Play()
    {
        Manager = FindObjectOfType<AudioManager>();
        Manager.PlayAudio(AudioElement);
    }
}
