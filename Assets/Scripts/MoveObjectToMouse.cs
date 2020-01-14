using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectToMouse : MonoBehaviour
{
    public Transform ObjectToMove;
    public Vector2 location; 
    public RectTransform RelativeObject;
    [Range(-1000.0f, 1000.0f)]
    public float leftBounding;
    [Range(-1000.0f, 1000.0f)]
    public float bottomBounding;
    [Range(-1000.0f, 1000.0f)]
    public float rightBounding;
    [Range(-1000.0f, 1000.0f)]
    public float topBounding;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Move()
    {
        ObjectToMove.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 88f));
        location = new Vector2(
            (ObjectToMove.position.x - RelativeObject.position.x) / ScaleUI.GetScale().x,
            (ObjectToMove.position.y - RelativeObject.position.y) / ScaleUI.GetScale().y
            );

        location = new Vector2(
            (location.x + leftBounding) / rightBounding,
            (location.y + bottomBounding) / topBounding
            );

    }
}
