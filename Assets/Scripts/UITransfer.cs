using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITransfer : MonoBehaviour
{
    [Header("Data")]
    public UIDisplay OwningDisplay = null;
    public string TargetIdentifier = "";
    // Start is called before the first frame update
    void Start()
    {
        FindDisplay();
    }

    protected void FindDisplay()
    {
        OwningDisplay = this.gameObject.GetComponentInParent<UIDisplay>();
        return;
    }

    public void ActivateTargetUI()
    {
        OwningDisplay.control.SwitchToDisplay(TargetIdentifier);
    }

    public void ActivateTargetUIVideoArtifacts()
    {
        if(Data.currentVideoFileAndArtifact != null)
        {
            //If there is something to add then continue, else just reset and leave.
            bool added = false;
            foreach (List<string> item in Data.VideoFileAndArtifactLocs)
            {
                /*
                * If there is a container in the list of video files/artifacts that contains the selected Video location, then that means that we've used this list previously.
                * Therefore we simply need to set our current list to the list we found.
                */
                if (item.Contains(Data.CurrentVideoFileLoc))
                {
                    foreach (string obj in Data.currentVideoFileAndArtifact)
                    {
                        if (!item.Contains(obj))
                        {
                            //If we find the proper list to add to, and it doesn't contain something from our list of artifacts to add, then add the artifact to the list.
                            item.Add(obj);
                        }
                    }
                    //After adding everything from our list of things to add, we simply need to note that we added something and move on.
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                //If we didn't 'add' something earlier, then we need to add the entire list since it hasn't been added yet.
                Data.VideoFileAndArtifactLocs.Add(Data.currentVideoFileAndArtifact);
            }


        }
        foreach(List<string> item in Data.VideoFileAndArtifactLocs)
        {
            foreach(string obj in item)
            {
                Debug.Log(obj);
            }
        }


        //After adding things properly, reset the video and artifact files and texts to their defaults.
        Data.CurrentArtifactFileLoc = "";
        Data.CurrentVideoFileLoc = "";
        Text video_text = GameObject.Find("Video Title").GetComponent<Text>();
        video_text.text = "Current Video:";
        Text txt = GameObject.Find("ArtifactFileName").GetComponent<Text>();
        txt.text = "Select an Artifact";
        Data.currentVideoFileAndArtifact = null;

        OwningDisplay.control.SwitchToDisplay(TargetIdentifier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
