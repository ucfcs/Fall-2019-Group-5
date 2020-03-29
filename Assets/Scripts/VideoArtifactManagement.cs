using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoArtifactManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArtifactInfoAdd()
    {
        InputField Timestamp = GameObject.Find("Artifact Timestamp").GetComponent<InputField>();
        Text TimestampText = GameObject.Find("Artifact Timestamp Text").GetComponent<Text>();
        Dropdown Supporting = GameObject.Find("Supporting Dropdown").GetComponent<Dropdown>();
        InputField Tooltip = GameObject.Find("Artifact Tooltip").GetComponent<InputField>();
        Text TooltipText = GameObject.Find("Artifact Tooltip Text").GetComponent<Text>();
        if((Data.CurrentArtifactFileLoc != "") && (TimestampText.text != ""))
        {
            string artifactName = System.IO.Path.GetFileNameWithoutExtension(Data.CurrentArtifactFileLoc);
            string ArtifactData = Data.CurrentArtifactFileLoc + "| " + TimestampText.text + " " + Supporting.value.ToString() + " " + artifactName + " | " + TooltipText.text;
            Data.currentVideoFileAndArtifact.Add(ArtifactData);
            Timestamp.text = "";
            Tooltip.text = "";
            Supporting.value = 0;
            Data.CurrentArtifactFileLoc = "";
            Text txt = GameObject.Find("ArtifactFileName").GetComponent<Text>();
            txt.text = "Artifact Added\n Select new Artifact File";
        }
        
        //txt.text = Data.currentVideoFileAndArtifact[1];
    }

    public void finishAddingToVideo()
    {
        if((Data.VideoFileAndArtifactLocs[Data.CurrentVideoIndex] == null) && (Data.currentVideoFileAndArtifact != null))
        {
            Data.VideoFileAndArtifactLocs.Add(Data.currentVideoFileAndArtifact);
        }
        Data.CurrentArtifactFileLoc = "";
        Data.CurrentVideoFileLoc = "";
        Text video_text = GameObject.Find("Video Title").GetComponent<Text>();
        video_text.text = "Current Video:";
        Text txt = GameObject.Find("ArtifactFileName").GetComponent<Text>();
        txt.text = "Select an Artifact";
        Data.currentVideoFileAndArtifact = null;


    }


}
