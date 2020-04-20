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
        //Set the export button to false so they don't attempt to export the same project multiple times.
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
        //Grab the file location of the Introdcution video and copy it into the Sherba folder under the name "Introduction.mp4"

        basicObjTarget = targetPath + @"\" + "map.png";
        System.IO.File.Copy(Data.MapFileLoc, basicObjTarget);
        //Grab the file location of the map image and copy it into the Sherba folder under the name "map.png"

        basicObjTarget = targetPath + @"\" + "waypoint.png";
        System.IO.File.Copy(Data.WaypointFileLoc, basicObjTarget);
        //Grab the location of the waypoint icon and copy it into the Sherba folder 

        foreach (List<string> item in Data.VideoFileAndArtifactLocs)
        {
            
            //For each item in VideoFileAndArtifactLocs grab the video file and add it.
            sourceFile = item[0];
            fileName = System.IO.Path.GetFileName(item[0]);
            string destFile = System.IO.Path.Combine(targetPath, fileName);
            System.IO.File.Copy(sourceFile, destFile, true);
            //Copy the lecture video into the Sherba folder

            string newDirectory = targetPath + @"\" + System.IO.Path.ChangeExtension(fileName, null);
            System.IO.Directory.CreateDirectory(newDirectory);
            //Create a directory named after the video to store the artifact info in.

            string itemFileName = newDirectory + @"\items.txt";
            // Check if an items text file in the newly created directory already exists. If yes, delete it.     
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
                        //Split the artifact string into the correct pieces and format it to fit the Student Build's mod loader format.
                        file.WriteLine(writeMe.Remove(0, 1));
                        //Write the artifact info into the items text file
                        string artifactDestination = newDirectory + @"\" + System.IO.Path.GetFileName(splitLine[0]);
                        System.IO.File.Copy(splitLine[0], artifactDestination);
                        //Copy the Artifact picture into the items text file.
                    }
                }
            }
            

        }


        
        string waypointFileName = targetPath + @"\waypoints.txt";
        // Check if a waypoints file already exists. If yes, delete it. 
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
                //Debug to show which locations are being written
                string writeWaypoints = wantedVector.x.ToString() + " " + (1 - wantedVector.y).ToString() + " " + System.IO.Path.GetFileNameWithoutExtension(Data.VideoFileAndArtifactLocs[ctr][0]) + " | " + Data.ToolTipList[ctr];
                file2.WriteLine(writeWaypoints);
                //Write the waypoint info to waypoints.txt

                ctr++;//Increase the counter to designate which video waypoint we're at.
            }
        }



        //Zip the Sherba Directory and name it SherbaProject
        ZipFile.CreateFromDirectory(targetPath, sourcePath + @"\SherbaProject.zip");
        my_button.interactable = true;
    }

}
