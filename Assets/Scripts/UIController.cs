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
        //DISABLE CURRENT DISPLAY IF IT EXISTS
        if (CurrentDisplay != null)
            CurrentDisplay.DisableDisplay();

        //NON-LINEAR SEARCH
        foreach (UIDisplay UI in DisplayList)
        {
            //IF WE FIND ONE THAT MATCHES THE INPUT
            if (UI.Identifier == Identifier)
            {

                //SET CURRENT DISPLAY TO NEW DISPLAY AND ENABLE
                CurrentDisplay = UI;
                UI.EnableDisplay();
                return true;
            }
        }

        //WE DIDN'T FIND OUR DISPLAY, SO WE FAILED
        CurrentDisplay.EnableDisplay();
        return false;
    }

    public bool AddDisplay(UIDisplay display)
    {
        try
        {
            //ADD TO LIST
            DisplayList.Add(display);
            display.DisableDisplay();

            //IF THIS IS THE MAIN DISPLAY, SET IT ACTIVE ASAP
            if (display != null && display.IsMain )
            {
                SwitchToDisplay(display.Identifier);
            }

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
