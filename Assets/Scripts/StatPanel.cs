using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPanel : MonoBehaviour
{
    [Header("Link")]
    public StatManager lManager;
    public renameObjectClass lEvidence;

    [Header("Values")]
    public string vName;
    public string vInfo;

    [Header("Containers")]
    public Text cInfo;

    public void Initialize(StatManager manage, renameObjectClass evidence, string name, string info)
    {
        lManager = manage;
        lEvidence = evidence;
        vName = name;
        vInfo = info;



        

        Locate();
        return;
    }

    private void Start()
    {
        InvokeRepeating("SlowUpdate", 0f, Data.SlowUpdateRate / 5f);
    }

    // Update is called once per frame
    void Update()
    {

        Locate();
    }

    

    void Locate()
    {
        Vector3 distanceBonus = lManager.panelDistanceBonus;
        Camera cam = FindObjectOfType<Camera>();
        float DistanceToCamera = (this.transform.position - cam.transform.position).magnitude;

        switch (lManager.panelDistanceFunction)
        {
            case Data.MathFunction.Add:
                distanceBonus = distanceBonus.normalized
                    * (distanceBonus.magnitude + lManager.panelDistanceFactor * DistanceToCamera);
                break;
            case Data.MathFunction.Subtract:
                distanceBonus = distanceBonus.normalized
                    * (distanceBonus.magnitude - lManager.panelDistanceFactor * DistanceToCamera);
                break;
            case Data.MathFunction.Multiply:
                distanceBonus = distanceBonus.normalized
                    * (distanceBonus.magnitude * lManager.panelDistanceFactor * DistanceToCamera);
                break;
            case Data.MathFunction.Divide:
                distanceBonus = distanceBonus.normalized
                    * (distanceBonus.magnitude / (lManager.panelDistanceFactor * DistanceToCamera));
                break;
        }

        this.transform.position = lEvidence.transform.position + lManager.panelOffset + distanceBonus;
        return;
    }



}
