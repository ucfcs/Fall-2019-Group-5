using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PopulateVideoStrings : MonoBehaviour
{

    public Transform Spawnable;

    public ArrayList StringTexts;
    public ArrayList SpawnedList;
    public int numberOfItemsManaged;

    public int range = 4;
    public int rangeLocator = 0;

    public float xLocation = 100f;

    public int verticalOffsetValue = 32;
    // Start is called before the first frame update
    void Start()
    {

        SpawnedList = new ArrayList();
        StringTexts = new ArrayList();


        //Start the threaded method
        InvokeRepeating("ThreadedOperationPopulate", 0f, 0.25f);

        Data.MapLocations = new List<Vector2>();
    }

    public void RangeLocatorUp()
    {

        rangeLocator++;
        if (rangeLocator >= SpawnedList.Count)
            rangeLocator = SpawnedList.Count - 1;
    }

    public void RangeLocatorDown()
    {

        rangeLocator--;
        if (rangeLocator <0)
            rangeLocator = 0;
    }

    public void DeleteActiveVideo()
    {

        int listSize = SpawnedList.Count; //grab the size of the list
        for (int i = 0; i < listSize; i++) //cycle through the entire list, foreach could potentially break if the list changes, this method isn't threaded, so it needs to run
        {
            if ((SpawnedList[i] as Transform).GetComponent<MapIconExchange>().isActive)
            {
                MapIconExchange temporary = ((SpawnedList[i] as Transform).GetComponent<MapIconExchange>());

                SpawnedList.RemoveAt(i);
                Destroy(temporary.SwitchableObject.gameObject);
                Data.VideoFileAndArtifactLocs.RemoveAt(i);
                Data.MapLocations.RemoveAt(i);
                Data.ToolTipList.RemoveAt(i);
                StringTexts.RemoveAt(i);
                Destroy(temporary.gameObject);
                numberOfItemsManaged--;

            }
        }
    }
    public void UpdateListLocations()
    {
        int listSize = SpawnedList.Count; //grab the size of the list

        for(int i = 0; i<listSize; i++) //cycle through the entire list, foreach could potentially break if the list changes, this method isn't threaded, so it needs to run
        {
            if(i < rangeLocator || i >= rangeLocator + range) //outside the range, shove it somewhere far far away
            {
                (SpawnedList[i] as Transform).GetComponent<RectTransform>().localPosition = new Vector3(xLocation, 10000000f, -15f);

            }
            else //we are inside the range, so display to the USER
            {
                (SpawnedList[i] as Transform).GetComponent<RectTransform>().localPosition = new Vector3(xLocation, ((i-rangeLocator) * verticalOffsetValue) * 1f, -15f);
            }

            (SpawnedList[i] as Transform).GetComponent<MapIconExchange>().ourID = i;

            (SpawnedList[i] as Transform).SetParent(this.transform);
        }

    }

    // Threaded method
    void ThreadedOperationPopulate()
    {
        if (!this.gameObject.active)
        {
            Debug.Log("Thread Paused");
            return;
        }
        Debug.Log("Thread Ran");
        try
        {
            if (Data.VideoFileAndArtifactLocs.ToArray().Length == 0)
            {
                Debug.Log("Found no videos");
                return;

            }

            foreach (List<string> list in Data.VideoFileAndArtifactLocs) //this method is run as a thread, so there's no worries if the arraylist changes for some reason, we can just try again on the next cycle
            {
                if (list.ToArray().GetLength(0) == 0)
                {
                    Debug.Log("Found no content");
                    break;
                }
                //if (StringTexts.ToArray().GetLength(0) > 0 && StringTexts[Data.VideoFileAndArtifactLocs.IndexOf(list)] as string == list[0])
                //    return;
                if (StringTexts.Contains(list[0]))
                {
                    Debug.Log("Item " + StringTexts.IndexOf(list[0]) + " " + list[0] + " already exists\nUpdating Location...");
                    try
                    {
                        Data.MapLocations[StringTexts.IndexOf(list[0])] = (SpawnedList[StringTexts.IndexOf(list[0])] as Transform).GetComponent<MapIconExchange>().location;
                    }
                    catch (System.Exception ex) { }
                    string fname = System.IO.Path.GetFileNameWithoutExtension(list[0]);
                    (SpawnedList[StringTexts.IndexOf(list[0])] as Transform).GetComponent<MapIconExchange>().AssignName(fname);
                }
                else { 
                    Transform temporary = Instantiate(Spawnable);

                    StringTexts.Add(list[0]);
                    string fname = System.IO.Path.GetFileNameWithoutExtension(list[0]);


                    SpawnedList.Add(
                       temporary
                    );

                    Data.MapLocations.Add(Vector2.zero);
                    Data.ToolTipList.Add("");

                    numberOfItemsManaged++;

                    temporary.GetComponent<MapIconExchange>().AssignName(
                            fname
                            );
                    temporary.SetParent(this.transform);
                    temporary.GetComponent<RectTransform>().localPosition = new Vector3(xLocation, (Data.VideoFileAndArtifactLocs.IndexOf(list) * verticalOffsetValue) * 1f, -15f);
                    temporary.GetComponent<RectTransform>().localScale = Vector3.one;
                }
                
            }
        }catch(System.Exception ex)
        {
 

        }

        UpdateListLocations();
    }
}
