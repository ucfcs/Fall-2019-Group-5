using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransfer : MonoBehaviour
{
    [Header("Data")]
    public UIDisplay OwningDisplay = null;
    public string TargetIdentifier = "";
    // Start is called before the first frame update
    void Start()
    {
        FindDisplay();
    }

    protected void FindDisplay()
    {
        OwningDisplay = this.gameObject.GetComponentInParent<UIDisplay>();
        return;
    }

    public void ActivateTargetUI()
    {
        OwningDisplay.control.SwitchToDisplay(TargetIdentifier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
