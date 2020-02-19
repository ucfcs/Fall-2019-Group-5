using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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
