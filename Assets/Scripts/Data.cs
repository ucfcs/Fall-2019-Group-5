using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static float SlowUpdateRate = 0.15f;

    public static Sprite MapTexture = null;
    public static string MapFileLoc = "";
    public static string CurrentVideoFileLoc = "";
    public static string CurrentArtifactFileLoc = "";
    //For each list in the List, the first object will be the video file location, while the other objects will be represented by
    //Strings of the format: "imageLocation, timestamp, Supporting/Refuting/Irrelevant Num" 
    //As an example: "D:/Picture/pic1.png, 00:01, 1"
    public static List<List<string>> VideoFileAndArtifactLocs;
    

    public enum MathFunction
    {
        Add,
        Subtract,
        Multiply,
        Divide,
    }

   
}
