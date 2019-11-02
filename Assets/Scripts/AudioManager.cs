using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Audio Manager")]

//Author: Alexander Scheiner

public class AudioManager : MonoBehaviour
{
    [Header("Audio Management")]
    
    public float AudioPlayerStandbyTime = 5f;
    [Space(20)]
    public int NumberOfMusicPlayers = 0;
    public int NumberOfAudioPlayers = 0;
    public int AudioInQueue = 0;

    [Header("Queue Configuration")]
    public int MaxAudioOfTypePlaying = 5;
    public int MaxAudioOfTypeQueued = 2;
    public int MaxMusicPlayers = 1;
    public int MaxSongsQueued = 5;

    
    

    [Header("Lists")]
    public ArrayList AudioPlayers = new ArrayList();
    public ArrayList QueuedAudio = new ArrayList();
    public ArrayList MusicPlayers = new ArrayList();

    [Header("Scene Music")]
    public AudioElement SceneMusic;

    [Header("Testing")]
    public AudioElement TestAddToQueue = null;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneMusic != null)
        {
            PlayAudio(SceneMusic);
        }
    }
    [ContextMenu("Clear Queue")]
    void ClearQueue()
    {
        QueuedAudio.RemoveRange(0, QueuedAudio.Count);
    }

    public bool PlayAudio(AudioElement element)
    { 
        return QueueAudio(element); ;
    }

    protected bool QueueAudio(AudioElement element)
    {
        int count = 0;

        if (element.isMusic)
        {
            foreach (AudioElement e in QueuedAudio)
            {
                if (e == element)
                {
                    return false;
                }
                if(e.isMusic && element.isMusic)
                {
                    count++;
                }
            }

            if (count < MaxSongsQueued)
            {
                QueuedAudio.Add(element);
                return true;
            }
        }
        else
        {
            foreach (AudioElement e in QueuedAudio)
            {
                if (e == element)
                {
                    count++;
                }
            }

            if (count < MaxAudioOfTypeQueued)
            {
                QueuedAudio.Add(element);
                return true;
            }
        }

        return false;
    }

    public  AudioPlayer  FindPlayer(AudioElement element)
    {
        AudioPlayer result = null;
        bool found = false;
        int AvailableMatches = 0;
        ArrayList Matches = new ArrayList();

        ArrayList SearchList;
        int tMax = 0;
        if (element.isMusic)
        {
            SearchList = MusicPlayers;
            tMax = MaxMusicPlayers;
        }
        else
        {
            SearchList = AudioPlayers;
            tMax = MaxAudioOfTypePlaying;
        }

        
        foreach(AudioPlayer e in SearchList)
        {
            if(e.audioElement == element || e.IsReady() && Matches.Count < tMax || (e.audioElement.isMusic && element.isMusic))
            {
                Matches.Add(e);
                AvailableMatches++;
            }
        }

        for(int i = 0; i<Matches.Count && !found; i++)
        {
            AudioPlayer temp = (Matches[i] as AudioPlayer);
            if (temp.IsReady())
            {
                found = true;
                temp.AssignAudioElement(element);
                result = temp;
            }
        }

        if (!found)
        {
            if (element.isMusic)
            {
                if (Matches.Count < MaxMusicPlayers)
                {
                    result = CreatePlayer(element);
                }
                else
                {

                    result = null;
                    //DON"T RUN THIS, TOO SLOW, BAD, NO. CAUSES MASSIVE SPIKES
                    //this.GetType().GetMethod("FindPlayer").Invoke(this, new object[] { element });
                }
            }
            else
            {
                if (Matches.Count < MaxAudioOfTypePlaying)
                {
                    result = CreatePlayer(element);
                }
                else
                {

                    result = null;
                    //DON"T RUN THIS, TOO SLOW, BAD, NO. CAUSES MASSIVE SPIKES
                    //this.GetType().GetMethod("FindPlayer").Invoke(this, new object[] { element });
                }
            }
        }


        


        return result;
    }

    public AudioPlayer CreatePlayer(AudioElement element)
    {
        AudioPlayer tempPlayer = new GameObject().AddComponent<AudioPlayer>();
        tempPlayer.transform.SetParent(this.transform);
        tempPlayer.transform.localPosition = Vector3.zero;
        tempPlayer.AssignManager(this);
        tempPlayer.AssignAudioElement(element);

        if (element.isMusic)
            MusicPlayers.Add(tempPlayer);
        else
            AudioPlayers.Add(tempPlayer);

        return tempPlayer;
    }

    public void RemovePlayer(AudioPlayer player)
    {
        AudioPlayers.Remove(player);
        Destroy(player.gameObject);

        return;
    }

    // Update is called once per frame
    void Update()
    {
        NumberOfAudioPlayers = AudioPlayers.Count;
        NumberOfMusicPlayers = MusicPlayers.Count;
        AudioInQueue = QueuedAudio.Count;
        if (QueuedAudio.Count > 0)
        {
            int i = 0;
            
            AudioPlayer p = FindPlayer(QueuedAudio[0] as AudioElement);
            if((QueuedAudio[i] as AudioElement).isMusic && p == null)
            {
                while(i < QueuedAudio.Count && (QueuedAudio[i] as AudioElement).isMusic)
                {
                    i++;
                }
                if (i >= QueuedAudio.Count)
                    i = QueuedAudio.Count -1;
                p = FindPlayer(QueuedAudio[i] as AudioElement);
            }

            if (p != null)
            {
                p.Play();

                QueuedAudio.RemoveAt(i);
            }
        }


        if(TestAddToQueue != null)
        {
            QueueAudio(TestAddToQueue);
            TestAddToQueue = null;
        }
    }
}
