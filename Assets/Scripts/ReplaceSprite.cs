using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class ReplaceSprite : MonoBehaviour
{

    public Sprite GraphicTexture = null;
    public string GraphicLoc = "";
    public Image Renderer = null;
    private byte[] FileBytes;
    private Texture2D Tex;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateGraphic", 0f, Data.SlowUpdateRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadTexture()
    {
        if(GraphicLoc != Data.MapFileLoc)
        {
            GraphicLoc = Data.MapFileLoc;
            try
            {
                FileBytes = File.ReadAllBytes(GraphicLoc);
                Tex = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);
                Tex.LoadImage(FileBytes);
                GraphicTexture = Sprite.Create(Tex, new Rect(0, 0, Tex.width, Tex.height), new Vector2(0.5f, 0.5f));
            }
            catch (Exception ex)
            {
                Debug.Log("Graphic loading failed on file: "+GraphicLoc+"\n" + ex.StackTrace);
            }
        }
    }

    void UpdateGraphic()
    {
        LoadTexture();
        Renderer.sprite = GraphicTexture;
        
    }
}
