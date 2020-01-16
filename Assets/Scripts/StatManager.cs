using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{

    [Header("Prefabs")]
    public StatPanel PanelPrefab;

    [Header("Values")]
    public int MaxCharsToTruncate = 12;
    public string TruncationString = "...";
    public Vector3 panelOffset = Vector3.up;
    public Vector3 panelDistanceBonus = Vector3.up;
    public float panelDistanceFactor = 1f;
    public Data.MathFunction panelDistanceFunction;
    public float panelLifeTime = 5f;
    public float panelUpdateTime = 2f;

    [Header("Management")]
    public ArrayList Panels = new ArrayList();
    public ArrayList ManagedStatistics = new ArrayList();
    public int NumberOfManagedStatistics = 0;
    public int NumberOfPanels = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    string Truncate(string text)
    {
        char[] result = new char[MaxCharsToTruncate + TruncationString.Length];

        if (text.Length > MaxCharsToTruncate)
        {
            for (int i = 0; i < MaxCharsToTruncate; i++)
            {
                result[i] = text[i];
            }

            for (int i = MaxCharsToTruncate; i < MaxCharsToTruncate + TruncationString.Length; i++)
            {
                result[i] = TruncationString[i - MaxCharsToTruncate];
            }
        }
        else
            result = text.ToCharArray();
        return new string(result);
    }

    // Update is called once per frame
    void Update()
    {

        
        NumberOfPanels = Panels.Count;
        NumberOfManagedStatistics = ManagedStatistics.Count;


    }

    

    

    public void RemovePanel(StatPanel target)
    {
        Panels.Remove(target);
        Destroy(target.gameObject);
    }

    StatPanel CreatePanel(renameObjectClass o)
    {
        StatPanel result = Instantiate(PanelPrefab);

        result.Initialize(this, o, Truncate(o.name), Truncate(o.oInfo));

        return result;
    }

    StatPanel FindCardInPanels(renameObjectClass o)
    {
        StatPanel result = null;
        foreach (StatPanel p in Panels)
        {
            if (p.lEvidence == o)
                result = p;
        }
        return result;
    }

    
}
