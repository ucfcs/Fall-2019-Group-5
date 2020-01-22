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
    public ArrayList field1 = new ArrayList();

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

        CheckCardList(field1);

        ManageCardStats();

        AddStatistics(field1);


    }



    void ManageCardStats()
    {
        bool running = true;
        for (int i = 0; i < NumberOfManagedStatistics && running; i++)
        {
            bool found = false;
            CardStatistic stat = ManagedStatistics[i] as CardStatistic;

            if (field1.Contains(stat.unit as renameObjectClass) != false)
                found = true;

            if (!found)
            {
                ManagedStatistics.Remove(stat);
                running = false;
            }
        }
    }

    public void AddStatistics(ArrayList f)
    {
        ArrayList tlist = f;
        foreach (renameObjectClass a in tlist)
        {
            if (!(a is renameObjectClass c)) continue;
            CardStatistic stat = FindCardInStatList(c);

            if (stat == null)
            {
                stat = ScriptableObject.CreateInstance<CardStatistic>();
                stat.Create(c);
                stat.unit = c;
                ManagedStatistics.Add(stat);
            }
        }
    }

    void CheckCardList(ArrayList f)
    {
        ArrayList tlist = f;
        foreach (renameObjectClass c in tlist)
        {
            if (!(c is renameObjectClass u)) continue;
            renameObjectClass interact = u.GetComponentInChildren<renameObjectClass>();
            StatPanel panel = null;
            CardStatistic stat = FindCardInStatList(u);
            if (stat == null)
                return;

            if ((interact.hovering))
            {
                panel = FindCardInPanels(u);

                if (panel == null)
                {
                    panel = CreatePanel(u);
                    Panels.Add(panel);
                }

                stat.ConfirmHealthChanged();

                panel.Run(panelLifeTime);
            }
        }
    }

    public void RemovePanel(StatPanel target)
    {
        Panels.Remove(target);
        Destroy(target.gameObject);
    }

    StatPanel CreatePanel(renameObjectClass o)
    {
        StatPanel result = Instantiate(PanelPrefab);
        result.gameObject.active = true;

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

    CardStatistic FindCardInStatList(renameObjectClass c)
    {
        CardStatistic result = null;
        foreach (CardStatistic p in ManagedStatistics)
        {
            if (p.unit == c)
                result = p;
        }
        return result;
    }

    protected class CardStatistic : ScriptableObject
    {
        public renameObjectClass unit;

        public void Create(renameObjectClass unit)
        {
            this.unit = unit;
        }

        private void Update()
        {
            return;

        }

        public void ConfirmHealthChanged()
        {
            return;
        }
    }
}
