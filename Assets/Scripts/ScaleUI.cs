using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUI : MonoBehaviour
{

    private ArrayList UIelements;
    private ArrayList UIelementsOriginal;
    private int elementID = 0;
    [Header("Display Settings")]
    [Tooltip("Original Value used for scaling.")]
    public int DefinedWindowWidth;
    [Tooltip("Original Value used for scaling.")]
    public int DefinedWindowHeight;
    [Space(20)]
    [Tooltip("Display Scaling Multiplier")]
    [Range(0.0f,1.0f)]
    public float DisplayZ_ScaleFactor = 0.5f;
    private float oWidth, oHeight;

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
        UIelements = new ArrayList();
        UIelementsOriginal = new ArrayList();
        if (UseDefinedWindowSettings)
        {
            oWidth = DefinedWindowWidth + 0.0f;
            oHeight = DefinedWindowHeight + 0.0f;
        }
        else { 
            oWidth = Screen.width + 0.0f;
            oHeight = Screen.height + 0.0f;
        }

        foreach(UnityEngine.RectTransform element in this.transform.GetComponentsInChildren<RectTransform>())
        {

            UIelements.Add(element);
            UIelementsOriginal.Add(element);
        }

        InvokeRepeating("UpdateGraphic", 0f, ThreadUpdateRateSeconds);
    }

    protected void UpdateGraphic()
    {
        RectTransform tempElement = UIelements[elementID] as RectTransform;
        RectTransform tEOriginal = UIelementsOriginal[elementID] as RectTransform;

        tempElement.localScale = new Vector3(Screen.width / oWidth, Screen.height / oHeight, DisplayZ_ScaleFactor);
        Debug.Log("Ran on ID " + elementID);

        if(!ScaleOnlyThisObject)
            elementID = (elementID + 1) % UIelements.Count;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UseDefinedWindowSettings)
        {
            oWidth = DefinedWindowWidth + 0.0f;
            oHeight = DefinedWindowHeight + 0.0f;
        }
    }
}
