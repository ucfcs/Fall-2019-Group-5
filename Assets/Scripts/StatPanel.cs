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

    [Header("Data")]
    protected float remainingLifetime;
    protected float maxLifetime;

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
        UpdateTime(Time.deltaTime);

        if (remainingLifetime < 0f)
        {
            CancelInvoke("SlowUpdate");
            lManager.RemovePanel(this);
        }
        UpdateTextEntries();
        Locate();
    }

    private void SlowUpdate()
    {
        foreach (Image c in this.transform.GetComponentsInChildren<Image>())
        {
            Color temp = c.color;
            c.color = new Color(temp.r, temp.g, temp.b, Mathf.Pow(remainingLifetime / maxLifetime, 0.25f));
        }

        foreach (Text t in this.transform.GetComponentsInChildren<Text>())
        {
            Color temp = t.color;
            t.color = new Color(temp.r, temp.g, temp.b, Mathf.Pow(remainingLifetime / maxLifetime, 0.25f));
        }
    }

    void UpdateTextEntries()
    {
        cInfo.text = vInfo;
    }

        public void Run(float time)
    {
        remainingLifetime = time;
        maxLifetime = time;
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

        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + lManager.panelOffset + distanceBonus;
        return;
    }

    public void UpdateTime(float delta)
    {
        remainingLifetime += -delta;
        return;
    }

}
