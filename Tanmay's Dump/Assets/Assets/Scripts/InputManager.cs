using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Vector3 locationOfFinger; //Stores the current location of mouse 
    Vector3 StartDragPosition; //stores the position of the first touch
    Vector3 endDragPosition; //stores the position of the last touch

    //Game Objects are objects in hierarchy 
    
    private Cannon cannon; //reference to the Cannon instance
    public float minimumFingerDistance = 0.25f; //minimum finger distance travelled needed to launch balls 
    public bool isActive;
    GameManager gameManager;

    public void Awake()
    {
        cannon = FindObjectOfType<Cannon>();
        gameManager = GetComponent<GameManager>();
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
            //Call the startDrag function passing location of finger as parameter

            StartDrag(locationOfFinger);

        }

        //If the finger i holding down and dragging across the screen 
        if (Input.GetMouseButton(0))
        {
            //Call the ContinueDrag function passing location of finger as parameter
            ContinueDrag(locationOfFinger);
        }

        //If the finger is lifted from the screen
        if (Input.GetMouseButtonUp(0))
        {
            //Shifted to here to solve the aimline and no input named as the "Aditya" bug
            cannon.DoOnButtonUp(); //Makes the Aimline dissapear only irrespective ofwhether its a tap or a drag

            if ((StartDragPosition - endDragPosition).magnitude >= minimumFingerDistance) //If finger drag distance is more than minimum, launch the balls
            {
                
                //Call the EndDrag Function
                EndDrag(); //Call enddrag which calls the release ball function and change game state function 
            }

        }
    }

    void StartDrag(Vector3 fingerPositionInWorld)
    {
        //Task List
        //Recording the Starting position of the touch
        StartDragPosition = fingerPositionInWorld;
        cannon.DoOnButtonDown();

        //Make the aim line visible and set the starting position of the aimline to the cannon's position
    }

    void ContinueDrag(Vector3 fingerPositionInWorld)
    {
        //Task List
        //Start storing the current location of the finger in a new variable
        endDragPosition = fingerPositionInWorld;


        //aimAssist.transform.position = cannon.transform.position; // Set the starting position of the aimline to the cannon's position 
        //aimLine.SetPosition(1, new Vector3(0f, aimLineLength, 0f));

        float diffX = StartDragPosition.x - endDragPosition.x; // Find the difference in X
        float diffY = Mathf.Max(0.25f, (StartDragPosition.y - endDragPosition.y)); // Find the difference in y and select the maximum in 2 values 


        float angleOfRotation = Mathf.Rad2Deg * Mathf.Atan(diffX / diffY); //Get the tan inverse of differences and convert it to degrees from radians

        cannon.DoOnButtonHold(angleOfRotation);


        //Check the distance the finger has travelled 
        //If the finger does not travel the minimum ditance from the starting point, we cancel the input
        //If the finger travels the minimum distance from the starting point, 
        //Start recording the current location of the finger in a new variable
        //Calculate the direction and magnitude of the vector from start position to end position 
        //make sure the vertical component of the direction vector does not go below a minimum value to avoid aiming downwards.
        //Set the endpoint of the aimline to something we will discuss later
    }

    void EndDrag()
    {
        //Task List

       
        cannon.ReleaseBallsWithGap(); //call releasethyballs functions from cannon script
        gameManager.ChangeGameState(GameManager.GameStatesDATA.ACTION); //Call the changegamestate function and set it to Action 
        //Debug.Log("Launch thy balls");
        //gameManager.InputEnded(); //<-- !!! Ref to GM has not been added in class, and I did not ask them to do it in assignment 

    }

    /// <summary>
    /// Function to check whether the Input Manager is active or not
    /// </summary>
    /// <returns>isActive boolean variable</returns>
    public bool IsInputManagerActive() 
    {
        if(this.enabled /*if script component is enabled*/)
        {
            isActive = true; //Set isActive boolean value to true 
        }
        else /* if script component is disabled*/
        {
            isActive = false;  //Set isActive boolean value to false 
        }

        return isActive; //return the value of the boolean isActive 
    }
    /// <summary>
    /// Enable the Input Manager Script Component 
    /// </summary>
    public void EnableInputManager()
    {
        this.enabled = true; //set the enabled property of this script to true
    }
    /// <summary>
    /// Disable the Input Manager Script Component 
    /// </summary>
    public void DisableInputManager()
    {
        this.enabled = false; //set the enabled property of this script to false  
    }
}
