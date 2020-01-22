using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class renameObjectClass : Button
{
    //	Object DATA
    [Header("Object Data")]
    //	Data for object
    [Tooltip("Info Data")]
    public string oInfo;

    public Button Interact;
    public bool hovering = false;
    //need a buttonState var?

    public override void OnPointerEnter(UnityEngine.EventSystems.PointerEventData e)
    {
        hovering = true;
    }

    public override void OnPointerExit(UnityEngine.EventSystems.PointerEventData e)
    {
        hovering = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Interact = this.GetComponent<Button>() ;
        FindObjectOfType<StatManager>().field1.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
