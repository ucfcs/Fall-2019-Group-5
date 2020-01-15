using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class FileLoad : MonoBehaviour
{
    public Text ArtifactFileName;
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

        Data.MapFileLoc = EditorUtility.OpenFilePanel("File Browser", "", "");

    }

    public void LoadVideoFile()
    {

        Data.CurrentVideoFileLoc = EditorUtility.OpenFilePanel("Select a Video", "", "mp4");
        Text my_text = GameObject.Find("Video Title").GetComponent<Text>();
        my_text.text = "Current Video: \n" + System.IO.Path.GetFileName(Data.CurrentVideoFileLoc);

    }

    public void LoadArtifactFile()
    {
        if(Data.CurrentVideoFileLoc != "")
        {
            Data.CurrentArtifactFileLoc = EditorUtility.OpenFilePanel("Select an image", "", "");
            Text txt = GameObject.Find("ArtifactFileName").GetComponent<Text>();
            if(Data.CurrentArtifactFileLoc != "")
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
