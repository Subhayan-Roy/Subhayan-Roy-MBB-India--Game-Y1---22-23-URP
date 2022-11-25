using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Vector3 locationOfFinger; //stores the current location of finger
    Vector3 startDragPosition;//stores the position of the first touch
    Vector3 endDragPosition;  //stores the position of the last touch

    public GameObject aimAssist; //stores the data of the gameobject named aimAssist
    public GameObject cannon; //stores the data of the gameobject named Cannon
    public float minimumFingerDistance = 0.25f; //minimum finger distance travelled needed to launch balls

    LineRenderer aimLine; //Store the data of the line renderer

    public float aimLineLength = 6f;

    public void Awake()
    {
        aimAssist = GameObject.Find("AimAssist"); //Find the gameobject named AimAssist and populate the variable
        cannon = GameObject.Find("Cannon"); //Find the gameobject named Cannon and populate the variable
        aimLine = aimAssist.GetComponent<LineRenderer>(); //Find the component line renderer from the aim assist gameobject and store it here
        aimAssist.SetActive(false); //Switch off the aimAssist gameObject
    }

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
            aimAssist.SetActive(false); //Make aimline disappear
            aimAssist.transform.rotation = Quaternion.Euler(0f, 0f, 0f); //Set the rotation back to zero

            if ((startDragPosition - endDragPosition).magnitude >= minimumFingerDistance) //If finger drag distance is more than minimum, launch the balls
            {
                //Call the EndDrag function
                EndDrag(); //Call function to launch Balls
            }
           
        }
    }

    void StartDrag(Vector3 fingerPositionInWorld)
    {
        //Task List
        //Record the starting position of the touch
        startDragPosition = fingerPositionInWorld;

        
    }

    void ContinueDrag(Vector3 fingerPositionInWorld)
    {
        //Task List
        //Start storing the current location of the finger in a new variable
        endDragPosition = fingerPositionInWorld;

        //Make the aim line visible
        aimAssist.SetActive(true);

        aimAssist.transform.position = cannon.transform.position; //Set the aimassist's position to the cannon's position
        aimLine.SetPosition(1, new Vector3(0f, aimLineLength, 0f));

        float diffX = startDragPosition.x - endDragPosition.x; //Find the difference in x 
        float diffY = Mathf.Max(0.25f,(startDragPosition.y - endDragPosition.y)); //Find the difference in y and selects the maximum of 2 values

        //if (diffY <= 0)
        //{
        //    diffY = 0.25f;
        //}

        float angleOfRotation = Mathf.Rad2Deg * Mathf.Atan(diffX / diffY); //Get the tan inverse of the differences and convert it to Degrees from Radians

        aimAssist.transform.rotation = Quaternion.Euler(0f, 0f, -angleOfRotation); //Set the angle of the rotation of the transform to the angle calculated
    }

    void EndDrag()
    {
        //Task List

        Debug.Log("Launch thy balls");
    }
}
