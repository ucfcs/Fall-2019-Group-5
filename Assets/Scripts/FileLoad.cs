using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FileLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFile()
    {

        Data.MapFileLoc = StandaloneFileBrowser.OpenFilePanel("File Browser", "", "",false)[0];

    }

    public void LoadVideoFile()
    {

        Data.CurrentVideoFileLoc = StandaloneFileBrowser.OpenFilePanel("Select a Video", "", "mp4",false)[0];
        Text my_text = GameObject.Find("Video Title").GetComponent<Text>();
        my_text.text = "Current Video: \n" + System.IO.Path.GetFileName(Data.CurrentVideoFileLoc);
        Data.CurrentVideoIndex = -1;
        if (Data.VideoFileAndArtifactLocs != null)
        {
            foreach (List<string> item in Data.VideoFileAndArtifactLocs)
            {
                /*
                * If there is a container in the list of video files/artifacts that contains the selected Video location, then that means that we've used this list previously.
                * Therefore we simply need to set our current list to the list we found.
                */
                Data.CurrentVideoIndex++;
                if (item.Contains(Data.CurrentVideoFileLoc))
                {
                    Data.currentVideoFileAndArtifact = item;
                    return;
                }
            }
        }
        Data.CurrentVideoIndex++;
        List<string> newList = new List<string>();
        newList.Add(Data.CurrentVideoFileLoc);
        Data.currentVideoFileAndArtifact = newList;

    }

    public void LoadArtifactFile()
    {
        if (Data.CurrentVideoFileLoc != "")
        {
            Data.CurrentArtifactFileLoc = StandaloneFileBrowser.OpenFilePanel("Select an image", "", "",false)[0];
            Text txt = GameObject.Find("ArtifactFileName").GetComponent<Text>();
            if (Data.CurrentArtifactFileLoc != "")
            {
                txt.text = "Current Artifact: " + System.IO.Path.GetFileName(Data.CurrentArtifactFileLoc);
            }
            else
            {
                txt.text = "Select a File";
            }

        }


    }
}
