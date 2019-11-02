using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Audio Player")]

public class AudioPlayer : MonoBehaviour
{
    [Header("Audio Element")]
    public AudioElement audioElement;
    protected AudioManager manager;
    protected AudioSource audioSource;
    public bool playOnAwake = false;
    public bool destroyOnComplete = false;
    public float LifetimeRemaining;
    public bool isPlaying = false;
    

    // Start is called before the first frame update
    void Start()
    {
        AssignAudioElement(audioElement);
        ResetStandby();
        if (playOnAwake)
        {
            Play();
        }

        return;
    }

    public bool IsReady()
    {
        if (audioSource.isPlaying)
        {
            return false;
        }
        if (audioSource == null || audioElement == null || audioElement.Audio == null)
        {
            return false;
        }

        ResetStandby();

        return true;
    }

    public AudioElement GetElement()
    {
        return audioElement;
    }

    public void AssignAudioElement(AudioElement e)
    {
        if(audioSource != null)
            Destroy(audioSource.gameObject);
        audioElement = e;
        
        audioSource = audioElement.GetAudioSource(this);

        AssignName(e.gameObject.name);

        Play();

        return;
    }

    public void AssignManager(AudioManager am)
    {
        manager = am;
        ResetStandby();
        return;
    }

    protected void AssignName(string name)
    {
        this.gameObject.name = "Audio Player: " + name;
    }

    public void Play()
    {
        
        audioSource.Play();
        isPlaying = true;
        InvokeRepeating("CheckIfPlaying", 0f, Data.SlowUpdateRate);
        return;
    }

    protected void ResetStandby()
    {
        LifetimeRemaining = manager.AudioPlayerStandbyTime;
        return;
    }

    // Update is called once per frame
    void CheckIfPlaying()
    {
        if (!audioSource.isPlaying)
        {
            isPlaying = false;
            AssignName("Idle"); 
            if (destroyOnComplete)
            {
                manager.RemovePlayer(this);
            }
        }
        else
        {
            ResetStandby();
        }
    }

    private void Update()
    {
        if(!audioSource.isPlaying)
            LifetimeRemaining += -Time.deltaTime;

        if (LifetimeRemaining <= 0f)
            manager.RemovePlayer(this);
    }
}
