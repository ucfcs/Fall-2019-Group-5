using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUI : MonoBehaviour
{
    //Blah blah blah array list stuff.
    //Stores elements of the User Interface
    private ArrayList UIelements;
    private ArrayList UIelementsOriginal;
    private static ScaleUI instance;

    //Internal counter for our element in the array list
    private int elementID = 0;

    //Public values for the inspector
    [Header("Display Settings")]
    [Tooltip("Original Value used for scaling.")]
    [Min(1)]
    public int DefinedWindowWidth;
    [Tooltip("Original Value used for scaling.")]
    [Min(1)]
    public int DefinedWindowHeight;
    [Space(20)]
    [Tooltip("Display Scaling Multiplier")]
    [Range(0.0f,1.0f)]
    public float DisplayZ_ScaleFactor = 0.5f;

    //We use this internally
    private float oWidth, oHeight;

    //Public values for the inspector
    [Header("Refinements")]
    [Tooltip("When checked, only this object will scale. When unchecked, every object will be scaled individually, one object per thread cycle.")]
    public bool ScaleOnlyThisObject = true;
    [Tooltip("WHen checked, the defined original values will be used. When unchecked, values will be assigned at runtime start.")]
    public bool UseDefinedWindowSettings = true;
    
    [Header("Thread Settings")]
    [Tooltip("The delay time between iterations through this object's main thread. Do not set this value too low.")]
    [Range(0.01f, 1.0f)]
    public float ThreadUpdateRateSeconds = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //Define array lists
        UIelements = new ArrayList();
        UIelementsOriginal = new ArrayList();

        //Window value setup
        if (UseDefinedWindowSettings)
        {
            oWidth = DefinedWindowWidth + 0.0f;
            oHeight = DefinedWindowHeight + 0.0f;
        }
        else { 
            oWidth = Screen.width + 0.0f;
            oHeight = Screen.height + 0.0f;
        }

        //Load up the arraylists
        foreach(UnityEngine.RectTransform element in this.transform.GetComponentsInChildren<RectTransform>())
        {

            UIelements.Add(element);
            UIelementsOriginal.Add(element);
        }

        //Start the thread
        InvokeRepeating("UpdateGraphic", 0f, ThreadUpdateRateSeconds);
    }

    protected void UpdateGraphic()
    {
        //Grab our objects from array lists
        //  We edit this one
        RectTransform tempElement = UIelements[elementID] as RectTransform;
        //  Stores original data
        //     Not currently used
        RectTransform tEOriginal = UIelementsOriginal[elementID] as RectTransform;

        //Scale the objects
        tempElement.localScale = new Vector3(Screen.width / oWidth, Screen.height / oHeight, DisplayZ_ScaleFactor);
        /*Debug.Log("Ran on ID " + elementID);*/

        //Increment the counter if we are doing every object
        if(!ScaleOnlyThisObject)
            elementID = (elementID + 1) % UIelements.Count;
        
    }

    // Update is called once per frame
    void Update()
    {

        //Grab window stuff or whatever
        if (UseDefinedWindowSettings)
        {
            oWidth = DefinedWindowWidth + 0.0f;
            oHeight = DefinedWindowHeight + 0.0f;
        }
    }

    public static Vector3 GetScale()
    {
        return new Vector3(Screen.width / instance.oWidth, Screen.height / instance.oHeight, instance.DisplayZ_ScaleFactor);
    }
}
