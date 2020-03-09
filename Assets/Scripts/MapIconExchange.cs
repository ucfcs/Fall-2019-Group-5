using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapIconExchange : MonoBehaviour
{

    public string Name = "<NAME>";
    public Transform Spawnable;
    public Transform SwitchableObject;
    public MoveObjectToMouse TargetSwapTarget;
    public Vector2 location = Vector2.zero;

    public Color activeColor = Color.blue;
    public Color inactiveColor = Color.white;

    public bool isActive = false;

    public int ourID = 0;
    // Start is called before the first frame update
    void Start()
    {
        SwitchableObject = Instantiate(Spawnable);
        SwitchableObject.SetParent(this.transform.parent);
        ToggleObject();
    }

    public void AssignName(string name)
    {
        Name = name;
        this.GetComponentInChildren<Text>().text = Name;
        return;
    }

    public void ToggleObject()
    {
        SwitchableObject.gameObject.active = true;
        if (SwitchableObject.gameObject.active)
        {
            TargetSwapTarget.ObjectToMove = SwitchableObject;
            foreach(MapIconExchange disableObject in transform.parent.GetComponentsInChildren<MapIconExchange>())
            {
                if(disableObject != this.GetComponent<MapIconExchange>())
                {
                    disableObject.SwitchableObject.gameObject.active = false;
                }
            }
        }
        else
        {
            //TargetSwapTarget.ObjectToMove = null;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0) && SwitchableObject.gameObject.active == true) {
            Data.MapLocations[ourID] = TargetSwapTarget.location;
        }

        if(SwitchableObject.gameObject.active == true)
        {
            this.GetComponent<Image>().color = activeColor;

            isActive = true;
        }
        else
        {
            this.GetComponent<Image>().color = inactiveColor;

            isActive = false;
        }

    }
}
