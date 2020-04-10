using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Compression;

public class ExportSherba : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExportSherbaProject()
    {
        Button my_button = GameObject.Find("Export").GetComponent<Button>();
        my_button.interactable = false;
        string fileName = "test.txt";
        string sourcePath = @"C:\Users\Public\";
        string targetPath = @"C:\Users\Public\Sherba";


        // Use Path class to manipulate file and directory paths.
        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);

        // To copy a folder's contents to a new location:
        // Create a new target folder. 
        // If the directory already exists, this method does not create a new directory.
        System.IO.Directory.CreateDirectory(targetPath);
        string basicObjTarget = targetPath + @"\" + "Introduction.mp4";
        System.IO.File.Copy(Data.IntroductionVideo, basicObjTarget);
        basicObjTarget = targetPath + @"\" + "map.png";
        System.IO.File.Copy(Data.MapFileLoc, basicObjTarget);
        basicObjTarget = targetPath + @"\" + "waypoint.png";
        System.IO.File.Copy(Data.WaypointFileLoc, basicObjTarget);
        foreach (List<string> item in Data.VideoFileAndArtifactLocs)
        {
            
            //For each item in VideoFileAndArtifactLocs grab the video file and add it.
            sourceFile = item[0];
            fileName = System.IO.Path.GetFileName(item[0]);
            string destFile = System.IO.Path.Combine(targetPath, fileName);
            System.IO.File.Copy(sourceFile, destFile, true);
            string newDirectory = targetPath + @"\" + System.IO.Path.ChangeExtension(fileName, null);
            System.IO.Directory.CreateDirectory(newDirectory);
            string itemFileName = newDirectory + @"\items.txt";
            // Check if file already exists. If yes, delete it.     
            if (File.Exists(itemFileName))
            {
                File.Delete(itemFileName);
            }
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(itemFileName))
            {
                foreach (string line in item)
                {
                    // If the line doesn't contain the word 'Second', write the line to the file.
                    if (!line.Equals(item[0]))
                    {
                        string[] splitLine = line.Split('|');
                        string writeMe = splitLine[1] + "|" + splitLine[2];
                        file.WriteLine(writeMe.Remove(0, 1));
                        string artifactDestination = newDirectory + @"\" + System.IO.Path.GetFileName(splitLine[0]);
                        System.IO.File.Copy(splitLine[0], artifactDestination);
                    }
                }
            }
            

        }


        
        string waypointFileName = targetPath + @"\waypoints.txt";
        // Check if file already exists. If yes, delete it. 
        if (File.Exists(waypointFileName))
        {
            File.Delete(waypointFileName);
        }
        using (System.IO.StreamWriter file2 =
                new System.IO.StreamWriter(waypointFileName))
        {
            int ctr = 0;
            foreach(Vector2 wantedVector in Data.MapLocations)
            {

                UnityEngine.Debug.Log("Location X: " + wantedVector.x.ToString());
                UnityEngine.Debug.Log("Location Y: " + (1 - wantedVector.y).ToString());
                string writeWaypoints = wantedVector.x.ToString() + " " + (1 - wantedVector.y).ToString() + " " + System.IO.Path.GetFileNameWithoutExtension(Data.VideoFileAndArtifactLocs[ctr][0]);
                file2.WriteLine(writeWaypoints);

                ctr++;//Increase the counter to designate which video waypoint we're at.
            }
        }




        // To copy a file to another location and 
        // overwrite the destination file if it already exists.
        //System.IO.File.Copy(sourceFile, destFile, true);
        ZipFile.CreateFromDirectory(targetPath, sourcePath + @"\SherbaProject.zip");
        my_button.interactable = true;
    }

}
