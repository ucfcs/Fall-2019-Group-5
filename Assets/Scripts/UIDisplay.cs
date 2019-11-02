using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("UI/UI Display")]

public class UIDisplay : MonoBehaviour
{
    [Header("Data")]
    protected UIController control;
    public string Identifier = "";
    public bool IsMain = false;
    public bool IsVisible = true;


    // Start is called before the first frame update
    void Start()
    {
        //ASSIGN CONTROL A VALUE
        control = FindController();

        //ADD US TO THE CONTROLLER
        AddToController();
    }

    protected UIController FindController()
    {
        //FIND AND REPORT CONTROL OBJECT
        return FindObjectOfType<UIController>();
    }

    protected void AddToController()
    {
        //CALL CONTROL OBJECT'S METHOD
        control.AddDisplay(this);
    }

    public void EnableDisplay()
    {
        //TODO: WRITE STUFF HERE
    }

    public void DisableDisplay()
    {
        //TODO: WRITE STUFF HERE
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
