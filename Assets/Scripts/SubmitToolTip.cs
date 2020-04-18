using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitToolTip : MonoBehaviour
{

    public PopulateVideoStrings ListOwner;
    public MapIconExchange OurExchanger;
    bool weHere = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(weHere)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                OnSubmit(this.GetComponent<UnityEngine.UI.InputField>().text);
            }
        }
    }

    public void Summon()
    {
        weHere = true;
        this.GetComponent<RectTransform>().localPosition = new Vector3(0f,0f,0f);
        Transform current = null;
        foreach(Transform t in ListOwner.SpawnedList)
        {
            if (t.GetComponent<MapIconExchange>().isActive)
            {
                current = t;
                OurExchanger = current.GetComponent<MapIconExchange>();
                break;
                
            }
            
        }
        this.GetComponent<UnityEngine.UI.InputField>().text = Data.ToolTipList[current.GetComponent<MapIconExchange>().ourID];
    }

    public void OnSubmit(string value)
    {
        weHere = false;
        Data.ToolTipList[OurExchanger.ourID] = value;
        this.GetComponent<RectTransform>().localPosition = new Vector3(555f, 555f, 0f);
    }
}
