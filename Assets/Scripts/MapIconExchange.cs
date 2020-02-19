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
    // Start is called before the first frame update
    void Start()
    {
        SwitchableObject = Instantiate(Spawnable);
        SwitchableObject.SetParent(this.transform);
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
        SwitchableObject.gameObject.active = !SwitchableObject.gameObject.active;
        if (SwitchableObject.gameObject.active)
        {
            TargetSwapTarget.ObjectToMove = SwitchableObject;
        }
        else
        {
            TargetSwapTarget.ObjectToMove = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
