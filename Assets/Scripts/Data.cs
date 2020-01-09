using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static float SlowUpdateRate = 0.15f;

    public static Sprite MapTexture = null;
    public static string MapFileLoc = "";
    public static ArrayList VideoFileLoc;

    public enum MathFunction
    {
        Add,
        Subtract,
        Multiply,
        Divide,
    }

   
}
