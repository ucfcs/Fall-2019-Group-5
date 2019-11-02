using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Audio Element")]

public class AudioElement : MonoBehaviour
{
    [Header("Audio Element")]
    public AudioClip Audio;
    public bool loop = false;
    public float pitch = 1f;
    public float volume = 1f;
    public bool isMusic = false;

    public AudioSource GetAudioSource(AudioPlayer player)
    {
        AudioSource source =  new GameObject("Audio Source").AddComponent<AudioSource>();
        source.transform.SetParent(player.transform);

        source.clip = Audio;
        source.loop = loop;
        source.pitch = pitch;
        source.volume = volume;

        return source;
    }

}
