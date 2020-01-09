using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

        Data.MapFileLoc = EditorUtility.OpenFilePanel("File Browser", "", "");

    }
}
