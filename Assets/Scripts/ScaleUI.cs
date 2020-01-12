using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUI : MonoBehaviour
{

    private ArrayList UIelements;
    private ArrayList UIelementsOriginal;
    private int elementID = 0;
    [Header("Display Setting")]
    public int DefinedWindowWidth;
    public int DefinedWindowHeight;
    public float DisplayZ_ScaleFactor = 0.5f;
    private float oWidth, oHeight;
    [Header("Refinements")]
    public bool ScaleOnlyThisObject = true;
    public bool UseDefinedWindowSettings = true;

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

        InvokeRepeating("UpdateGraphic", 0f, Data.SlowUpdateRate);
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
