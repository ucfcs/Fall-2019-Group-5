using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("UI/UI Display")]

public class UIDisplay : MonoBehaviour
{
    [Header("Data")]
    public UIController control;
    public string Identifier = "";
    public bool IsMain = false;
    public bool IsVisible = true;
    public ArrayList ChildObjects = new ArrayList();


    // Start is called before the first frame update
    void Start()
    {
        //ASSIGN CONTROL A VALUE
        control = FindController();

        //FIND CHILDREN IN ADVANCE
        FindChildren();

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

    public void FindChildren()
    {
        //NON-LINEAR SEARCH
        foreach (Transform g in this.gameObject.GetComponentsInChildren<Transform>())
        {
            //ADD CHILDREN TO LIST
            ChildObjects.Add(g.gameObject);
        }
        return;
    }

    public void EnableDisplay()
    {
        //NON-LINEAR SEARCH
        foreach (GameObject g in ChildObjects)
        {
            //SET EACH OBJECT TO ON
            g.SetActive(true);

            Debug.Log("Set object visible: " + g.name);

            
        }

        Debug.Log("Done");
        //TRIGGER VISIBLE BOOLEAN
        IsVisible = true;
    }

    public void DisableDisplay()
    {
        //NON-LINEAR SEARCH
        foreach (GameObject g in ChildObjects)
        {
            //SET EACH OBJECT TO ON
            g.SetActive(false);
        }
        //TRIGGER VISIBLE BOOLEAN
        IsVisible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
