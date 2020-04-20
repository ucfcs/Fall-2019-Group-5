using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

/*
 * 
 * This file should be attached to the Map GameObject within the WorldMap Scene
 * Loads the map file from the mod file, and then loads its waypoints
 * Read the Start() function at the end of this script
 * 
 */

public class mapLoader : MonoBehaviour
{
    //public string path = ModManager.instance.modFolderPath;
    private float wpScale = 0.5f;

    Texture2D LoadTexture(string path)
    {
        Texture2D texture;
        byte[] data;

        if (File.Exists(path))
        {
            data = File.ReadAllBytes(path);
            texture = new Texture2D(2, 2);
            if (texture.LoadImage(data))
                return texture;
        }
        return null;
    }

    private void loadMap()
    {
        string mapPath = ModManager.instance.modFolderPath + "map.png";

        //load map texture
        Texture2D mapTex = LoadTexture(mapPath);
        if (mapTex == null)
        {
            Debug.Log("file not found");
            return;
        }
        Sprite mapImg = Sprite.Create(mapTex, new Rect(0, 0, mapTex.width, mapTex.height), new Vector2(0, 0));

        //put map on scene
        Image img = this.GetComponent<Image>();
        img.sprite = mapImg;

        return;
    }

    private void loadWaypoints(Vector2 mapLoc)
    {
        //if we've already parsed waypoint file, skip this step
        if (ModManager.instance.wp.Count != 0)
            return;

        //Find the waypoint text file
        string wpPath = ModManager.instance.modFolderPath + "waypoints.txt";
        StreamReader reader = new StreamReader(wpPath);
        //Debug.Log(reader.ReadToEnd());

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] wpInfo = line.Split(" "[0]);

            //calculate where to put the waypoints relative to the map 
            //see design doc for a more detailed explanation
            float offsetX = float.Parse(wpInfo[0]);
            float offsetY = float.Parse(wpInfo[1]);
            float waypointLocX = mapLoc.x + ((Screen.width - mapLoc.x) * offsetX);
            float waypointLocY = mapLoc.y - (mapLoc.y * offsetY);
            //Debug.Log("Screen.width = " + Screen.width + "\tScreen.height = "+Screen.height);
            //Debug.Log("mapLoc.x = " + mapLoc.x + "\tmapLoc.y = " + mapLoc.y);
            //Debug.Log("waypointLocX = " + waypointLocX + "\twaypointLocY = " + waypointLocY);

            Vector2 offset = new Vector2(offsetX, offsetY);
            Vector2 loc = new Vector2(waypointLocX, waypointLocY);

            //The description starts when | is found
            string wpName = "";
            string wpDesc = "";
            bool findName = true;
            for (int i = 2; i < wpInfo.Length; i++)
            {
                if (wpInfo[i] == "|")
                {
                    findName = false;
                    continue;
                }
                if (findName)
                    wpName += wpInfo[i] + " ";
                else
                    wpDesc += wpInfo[i] + " ";
                
            }
                
            if (wpName.Length > 0)
            	wpName = wpName.Substring(0, wpName.Length - 1);       //Removes last space
        	if (wpDesc.Length > 0)
            	wpDesc = wpDesc.Substring(0, wpDesc.Length - 1);

            ModManager.instance.wp.Add(new ModManager.Waypoint(wpName, wpDesc, offset, loc));
        }

        reader.Close();

        return;
    }

    private void addWaypoints()
    {
        int i = 0;
        foreach(ModManager.Waypoint wp in ModManager.instance.wp)
        {
            //name the waypoint button after the its index in the array
            GameObject waypoint = new GameObject();
            waypoint.name = i.ToString();
            i++;

            waypoint.AddComponent<CanvasRenderer>();
            waypoint.AddComponent<RectTransform>();

            Button mbtn = waypoint.AddComponent<Button>();

            //Apply waypoint texture
            string wpPath = ModManager.instance.modFolderPath + "waypoint.png";
            Texture2D wpTex = LoadTexture(wpPath);
            Image wpImage = waypoint.AddComponent<Image>();
            wpImage.sprite = Sprite.Create(wpTex, new Rect(0, 0, wpTex.width, wpTex.height), new Vector2(0, 0));
            mbtn.targetGraphic = wpImage;

            //Set waypoint's parent to the map
            waypoint.transform.SetParent(this.transform);

            //Apply a scale so it isnt huge
            Vector3 waypointScale = waypoint.transform.localScale;
            waypoint.transform.localScale = new Vector3(waypointScale.x * wpScale, waypointScale.y * wpScale, 1);

            //Add waypoint to map
            waypoint.transform.position = new Vector3(wp.mapLoc.x, wp.mapLoc.y, 1);

            //Add onclick event, see below
            mbtn.onClick.AddListener(waypointOnClick);

            //add onHover event, see waypointHoverEvents.cs script file
            waypoint.AddComponent<waypointHoverEvents>();

        }
    }

    private void waypointOnClick()
    {
        //The name of the waypoint also corresponds to its index in the ModManager's waypoint list
        //Thus, we can use that to get the name of the waypoint
        //The name of the waypoint corresponds to the video that needs to play when we scene transition to the Lecture Screen

        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        int index = int.Parse(buttonName);
        string waypointName = ModManager.instance.wp[index].name;

        ModManager.instance.video = waypointName;
        ModManager.instance.si.level = waypointName;

        SceneManager.LoadScene(4);
    }

    private void printWaypoints()
    {
        int i = 0;
        foreach (ModManager.Waypoint wp in ModManager.instance.wp)
        {
            Debug.Log(i + ".\tName: " + wp.name + "\tDesc: " + wp.desc);
            Debug.Log("Offset: " + wp.offset + "\tMapLoc: " + wp.mapLoc);
            i++;
        }
    }

    void Start()
    {
        //Sets the flag dictating that the student has finished a lecture to false
        //Used for the world map button within Sherlock's Study
        ModManager.instance.flag = false;

        //Loads the map texture onto the scene
        loadMap();

        //Get the map's dimensions
        RectTransform rt = this.GetComponent<RectTransform>();

        //Create a vector using those dimensions
        Vector2 mapLoc = new Vector2(rt.position.x, rt.position.y);

        //Populate the ModManager's waypoints list
        loadWaypoints(mapLoc);

        if (ModManager.instance.debug)
            printWaypoints();

        //Add the waypoints to the map
        addWaypoints();
    }
}
