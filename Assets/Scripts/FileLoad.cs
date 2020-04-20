using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class FileLoad : MonoBehaviour
{
    private bool onCancel;

    // FileBrowser.OnCancel onCancel;
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

        StartCoroutine(LoadMapFile());
    }

    public IEnumerator LoadMapFile()
    {

        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);

        if (FileBrowser.Success)
        {
            Data.MapFileLoc = FileBrowser.Result;
        }

    }

    private void onSuccess(string obj)
    {
        return;
    }

    public void LoadWaypointIcon()
    {

        StartCoroutine(LoadWaypointIcon2());
    }

    public IEnumerator LoadWaypointIcon2()
    {

        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);

        if (FileBrowser.Success)
        {
            Data.WaypointFileLoc = FileBrowser.Result;
        }
    }
    public void LoadIntroductionVideo()
    {

        StartCoroutine(LoadIntroductionVideo2());
    }
    public IEnumerator LoadIntroductionVideo2()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);

        if (FileBrowser.Success)
        {
            Data.IntroductionVideo = FileBrowser.Result;
        }
        Text my_text = GameObject.Find("Intro Video Text").GetComponent<Text>();
        my_text.text = "Current Introduction Video:\n" + System.IO.Path.GetFileName(Data.IntroductionVideo);
        //Save the Intro Video into Data and then update the text on screen.
    }


    public void LoadVideoFile()
    {

        StartCoroutine(LoadVideoFile2());
    }
    public IEnumerator LoadVideoFile2()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);

        if (FileBrowser.Success)
        {
            Data.CurrentVideoFileLoc = FileBrowser.Result;
        }
        Text my_text = GameObject.Find("Video Title").GetComponent<Text>();
        my_text.text = "Current Video: \n" + System.IO.Path.GetFileName(Data.CurrentVideoFileLoc);
        //Save the video into data and update the text on screen.

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
                    break;
                }
            }
        }
        Data.CurrentVideoIndex++;
        List<string> newList = new List<string>();
        newList.Add(Data.CurrentVideoFileLoc);
        Data.currentVideoFileAndArtifact = newList;
        //If there isn't a container in the list of video files/artifacts that contains the video location, that means we haven't used this list and need to add it to the list.

    }
    public void LoadArtifactFile()
    {

        StartCoroutine(LoadArtifactFile2());
    }
    public IEnumerator LoadArtifactFile2()
    {
        if (Data.CurrentVideoFileLoc != "")
        {
            // Show a load file dialog and wait for a response from user
            // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
            yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

            // Dialog is closed
            // Print whether a file is chosen (FileBrowser.Success)
            // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
            Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);


            //Save the artifact file location and then update the code to reflect the fact that the file was loaded.
            if (FileBrowser.Success)
            {
                Data.CurrentArtifactFileLoc = FileBrowser.Result;
            }
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
