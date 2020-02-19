using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateVideoStrings : MonoBehaviour
{

    public MapIconExchange Spawnable;

    public ArrayList StringTexts;
    public ArrayList SpawnedList;

    public int verticalOffsetValue = 32;
    // Start is called before the first frame update
    void Start()
    {

        SpawnedList = new ArrayList();
        StringTexts = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (Data.VideoFileAndArtifactLocs.ToArray().Length == 0)
                return;

            foreach (List<string> list in Data.VideoFileAndArtifactLocs)
            {
                if (StringTexts[Data.VideoFileAndArtifactLocs.IndexOf(list)] as string == list[0])
                    return;

                Object temporary = Instantiate(Spawnable);

                SpawnedList.Add(
                   temporary
                );

                (temporary as GameObject).GetComponent<MapIconExchange>().AssignName(
                        list[0].Substring(
                            0, list[0].IndexOf(',')
                            )
                        );

                (temporary as GameObject).GetComponent<RectTransform>().position = new Vector3(100f, (Data.VideoFileAndArtifactLocs.IndexOf(list) * verticalOffsetValue) * 1f);
            }
        }catch(System.Exception ex)
        {


        }
    }
}
