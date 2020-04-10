using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static float SlowUpdateRate = 0.15f;

    public static string WaypointFileLoc = "";
    public static string IntroductionVideo = "";
    public static Sprite MapTexture = null;
    public static string MapFileLoc = "";
    public static string CurrentVideoFileLoc = "";
    public static string CurrentArtifactFileLoc = "";
    public static int CurrentVideoIndex = -1;
    //For each list in the List, the first object will be the video file location, while the other objects will be represented by
    //Strings of the format: "imageLocation, timestamp, Supporting/Refuting/Irrelevant Num" 
    //As an example: "D:/Picture/pic1.png, 00:01, 1"
    public static List<List<string>> VideoFileAndArtifactLocs = new List<List<string>>();
    public static List<string> currentVideoFileAndArtifact;
    public static List<Vector2> MapLocations;
    public static float map_height;
    public static float map_width;

    public enum MathFunction
    {
        Add,
        Subtract,
        Multiply,
        Divide,
    }

   
}
