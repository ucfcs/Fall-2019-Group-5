using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;


/*
 * 
 * While this script is called the ModManager, it is intended to work as the Game Manager
 * This script deals with parameters that need to persist across the whole game across different scenes
 * This includes the video name that needs to be played, the list of waypoints, the POST request URL, and other things
 * See the code for more detail
 * 
 * To call a variable or function, be sure to call ModManager.instance
 * For example, the video name is stored at ModManager.instance.video
 * 
 */



public class ModManager : MonoBehaviour
{
    //The ModManager keeps an instance of itself to ensure its singleton property
    //and for access to the various variables, classes, and functions
    public static ModManager instance = null;

    //Path to the Extracted Mod File
    public string modFolderPath;

    //Next video that needs to play
    public string video = "introduction";

    //Debug bool, should be set to false when not debugging
    public bool debug = false;

    //Student Info to send to the database
    public StudentInfo si;

    //List of Waypoints that will be populated by mapLoader.cs
    public List<Waypoint> wp = new List<Waypoint>();

    //List of completed videos
    public HashSet<string> videoNamesAlreadyAccessed = new HashSet<string>();

    //Message box Canvas
    public GameObject msgCanvas;

    //used by the World Map button in Sherlock's Study
    public bool flag = false;   

    //URL to send POST requests to the database
    private static string postURL = "http://projectsherba.com/scores/addscore";


    //Class for all Items
    public class Item : IComparable<Item>
    {
        public string name;
        public string desc;
        public int category;
        public string time;
        public bool collected;

        public Sprite sprite;

        private int timeAsInt;


        public Item(string n, int c, string t)
        {
            this.name = n;
            this.category = c;
            this.time = t;
            this.collected = false;

            this.desc = "";
            this.sprite = null;

            SetIntTime();
        }

        public Item(ModManager.Item item)
        {
            this.name = item.name;
            this.desc = item.desc;
            this.category = item.category;
            this.time = item.time;
            this.collected = item.collected;
            this.sprite = item.sprite;
            this.timeAsInt = item.timeAsInt;
        }

        private int SetIntTime()
        {
            string t = this.time;
            int timeAsInt;
            string[] timeArray = t.Split(':');

            t = timeArray[0] + timeArray[1];
            timeAsInt = Int32.Parse(t);

            this.timeAsInt = timeAsInt;

            return timeAsInt;
        }

        //Sets the image of the object
        public Sprite setSpriteFromPath(string path)
        {
            Texture2D texture;
            byte[] data;

            if (File.Exists(path))
            {
                data = File.ReadAllBytes(path);
                texture = new Texture2D(2, 2);
                if (texture.LoadImage(data))
                {
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
                    this.sprite = sprite;
                    return sprite;
                }
            }
            this.sprite = null;
            return null;
        }

        public Sprite getSprite()
        {
            return this.sprite;
        }

        public string setDesc(string desc)
        {
            this.desc = desc;
            return this.desc;
        }

        //Compares time; if an item occurs later in the video, it'll appear later in a list
        public int CompareTo(Item i)
        {
            if (i == null)
                return 1;

            if (this.timeAsInt > i.timeAsInt)
                return 1;
            else if (this.timeAsInt == i.timeAsInt)
                return 0;
            else
                return -1;
        }
    }

    //Class that hold Student Info to send to the database
    public class StudentInfo
    {
        public string name;
        public string nid;
        public string cc;   //class code
        public string mod;
        public string level;

        public StudentInfo(string n, string nid, string cc)
        {
            this.name = n;
            this.nid = nid;
            this.cc = cc;
        }

        public StudentInfo(string n, string nid, string cc, string mod)
        {
            this.name = n;
            this.nid = nid;
            this.cc = cc;
            this.mod = mod;

            this.level = mod;
        }

        public StudentInfo(ModManager.StudentInfo si)
        {
            this.name = si.name;
            this.nid = si.nid;
            this.cc = si.cc;
            this.mod = si.mod;
            this.level = si.level;
        }
        
        //PostRequest function
        public IEnumerator postRequest()
        {
            WWWForm form = new WWWForm();
            form.AddField("student", this.name);
            form.AddField("nid", this.nid);
            form.AddField("class", this.cc);
            form.AddField("gamename", this.mod);
            form.AddField("level", this.level);

            UnityWebRequest uwr = UnityWebRequest.Post(postURL, form);
            Debug.Log("Attempting to post to database");
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
                Debug.Log("Error While Sending: " + uwr.error);
            else
                Debug.Log("Received: " + uwr.downloadHandler.text);
        }

    }

    //Class for the buttons that represent Waypoints
    //See mapLoader.cs for more details
    public class Waypoint
    {
        public string name;
        public string desc;
        public Vector2 offset;
        public Vector2 mapLoc;

        public Waypoint(string n, string d, Vector2 o, Vector2 m)
        {
            this.name = n;
            this.desc = d;
            this.offset = o;
            this.mapLoc = m;
        }

        public void changeOffset(float x, float y)
        {
            this.offset = new Vector2(x, y);
        }

        public void changeMapLoc(float x, float y)
        {
            this.mapLoc = new Vector2(x, y);
        }

        public string getName()
        {
            return this.name;
        }
    }


    //Send a message using the MessageCanvas that DOES NOT have a close button
    //Call using ModManager.instance.sendMsgWithoutBtn(<string>)
    //Note: IF YOU USE THIS FUNCTION, BE SURE TO SET THE CANVAS ACTIVE STATE TO FALSE ELSEWHERE
    public void sendMsgWithoutBtn(string m)
    {
        
        Debug.Log("Sending message: " + m);
        instance.msgCanvas.SetActive(true);
        MsgBox.btn.SetActive(false);
        MsgBox.instance.setMsg(m);
    }

    //Send a message using the MessageCanvas that DOES have a close button
    //Call using ModManager.instance.sendMsgWithBtn(<string>)
    public void sendMsgWithBtn(string m)
    {
        //Sends a messege with a close button
        Debug.Log("Sending message: " + m);
        instance.msgCanvas.SetActive(true);
        MsgBox.btn.SetActive(true);
        MsgBox.instance.setMsg(m);
    }

    //Function that begins the process to POST to the database
    //Call using ModManager.instance.postStart()
    public void postStart()
    {
        StartCoroutine(si.postRequest());
    }
    private void Start()
    {
        msgCanvas = GameObject.FindGameObjectWithTag("msgCanvas");
        msgCanvas.SetActive(false);
    }

    //creates the "singleton" functionality of the ModManager
    //this function will ensure only one ModManager exists
    //as well as ensure that the one will never be deleted upon moving scenes
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
         //  DontDestroyOnLoad(Inventory);
            instance = this;

            if (debug)
                unzipModFolderDebug();
        }
        else if (instance != this)
            Destroy(gameObject);
    }


    //Extract mod folder
    public void unzipModFolder(string modName)
    {
        if (debug)
        {
            Debug.Log("Debug is true, skipping unzip process for Login screen...");
            return;
        }

        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);    //Gets AppData\Roaming in Windows
        string extractLoc = path + "\\SHERBA\\";

        string modNameZip = modName + ".zip";

        //check if the mod is already unzipped
        extractLoc += modName + "\\";
        if (Directory.Exists(extractLoc))
        {
            modFolderPath = extractLoc;
            Debug.Log("Mod already unzipped at: " + modFolderPath);
            return;
        }

        //unzip modfolder and set modFolderPath
        Directory.CreateDirectory(extractLoc);
        ZipUtil.Unzip("./mods/" + modNameZip, extractLoc);
        modFolderPath = extractLoc;
    }

    //Extracts mod folder AND creates a sample student
    //used for debugging purposes only
    void unzipModFolderDebug()
    {
        si = new StudentInfo("Test", "te123456", "ABC123");

        //if on windows, use AppData for extract location
        //if windows:
        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);    //Gets AppData\Roaming in Windows
        string extractLoc = path + "\\SHERBA\\";


        //check mods folder and stores first found .zip file
        string modFolder = "./mods/";
        DirectoryInfo directorySelected = new DirectoryInfo(modFolder);
        FileInfo[] modFiles = directorySelected.GetFiles("*.zip");
        if (modFiles.Length == 0)
        {
            Debug.Log("no mod files found");
            return;
        }
        FileInfo modInfo = modFiles[0];
        string modName = modInfo.Name.Substring(0, modFiles[0].Name.Length - 4);

        si.mod = modName;
        si.level = modName;

        //check if the mod is already unzipped
        extractLoc += modName+"\\";
        if (Directory.Exists(extractLoc))
        {
            modFolderPath = extractLoc;
            Debug.Log("Mod already unzipped at: "+modFolderPath);
            return;
        }

        //unzip modfolder and set modFolderPath
        Directory.CreateDirectory(extractLoc);
        ZipUtil.Unzip(modInfo.FullName, extractLoc);
        modFolderPath = extractLoc;
    }

}
