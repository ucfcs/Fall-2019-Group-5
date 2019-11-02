using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("UI/UI Controller")]

public class UIController : MonoBehaviour
{

    [Header("Lists")]
    public ArrayList DisplayList = new ArrayList();
    public UIDisplay CurrentDisplay = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FindMainDisplay()
    {
        //TODO: WRITE STUFF HERE
    }

    public bool SwitchToDisplay(string Identifier)
    {
        //TODO: WRITE STUFF HERE
        return true;
    }

    public bool AddDisplay(UIDisplay display)
    {
        try
        {
            //ADD TO LIST
            DisplayList.Add(display);

            //SUCCEEDED, SO RETURN TRUE
            return true;
        }catch(Exception ex) {

            //LOG ERRROR
            Debug.Log("Failed to add display "
                +display.Identifier
                +":"
                +display.name
                +" to DisplayList!\n"
                +ex.StackTrace);

            //FAILED, SO RETURN FALSE
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
