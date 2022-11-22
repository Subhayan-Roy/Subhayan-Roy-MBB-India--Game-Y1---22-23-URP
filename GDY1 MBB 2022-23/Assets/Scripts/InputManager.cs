using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Vector3 locationOfFinger; //stores the current location of finger
    public Vector3 startDragPosition;//stores the position of the first touch
    public Vector3 endDragPosition;  //stores the position of the last touch

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Convert finger position from pixels to units
        locationOfFinger = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //Switch off the z component of the finger position
        locationOfFinger.z = 0f;

        //If the finger has touched the screen once
        if (Input.GetMouseButtonDown(0))
        {
            //Call the StartDrag function passing location of finger as parameter
            StartDrag(locationOfFinger);
        }

        //If the finger is holding down and dragging across the screen
        if (Input.GetMouseButton(0))
        {
            //Call the ContinueDrag function passing location of finger as parameter
            ContinueDrag(locationOfFinger);
        }

        //If the finger is lifted from the screen
        if (Input.GetMouseButtonUp(0))
        {
            //Call the EndDrag function
            EndDrag();
        }
    }

    void StartDrag(Vector3 fingerPositionInWorld)
    {
        //Task List
        //Record the starting position of the touch
        startDragPosition = fingerPositionInWorld;

        //Make the aim line visible and set the starting position of the aimline to the cannon's position
    }

    void ContinueDrag(Vector3 fingerPositionInWorld)
    {
        //Task List
        //Start storing the current location of the finger in a new variable
        endDragPosition = fingerPositionInWorld;

        //Check the distance the finger has travelled
        //If the finger does not travel the minimum distance from the starting point, we cancel the input.
        //If the finger travels the minimum distance from the starting point,

        //Calculate the direction and magnitude of the vector from start position to end position
        //make sure the vertical component of the direction vector does not go below a minimum value to avoid aiming downwards
        //Set the endpoint of the aimline to something we will discuss later
    }

    void EndDrag()
    {
        //Task List
        //Remove the aimline
        //Normalize the direction vector
        //Shoot the damn balls
    }
}
