using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	//[SerializeField] private GameManager gameManager; //<-- !!! This has not been done in class
	public float minimumFingerDistance = 0.25f; //minimum finger distance travelled needed to validate the input
	public bool isActive;


	Vector3 locationOfFinger;   //stores the current location of finger
	Vector3 startDragPosition;  //stores the position of the first touch
	Vector3 endDragPosition;	//stores the position of the last touch

	private UpdatedCannon cannon; //reference to the Cannon instance
	GameManager gameManager;
	FingerFeedback fingerFeedback;

	public void Awake()
    {
		
	}

    public void Start()
	{
		cannon = FindObjectOfType<UpdatedCannon>();
		gameManager = GetComponent<GameManager>();
		fingerFeedback = FindObjectOfType<FingerFeedback>();

		fingerFeedback.SetThreshold(minimumFingerDistance);
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
			fingerFeedback.StartDrag(locationOfFinger);
		}

		//If the finger is holding down and dragging across the screen
		if (Input.GetMouseButton(0))
		{
			//Call the ContinueDrag function passing location of finger as parameter
			ContinueDrag(locationOfFinger);
			fingerFeedback.Dragging((locationOfFinger));
		}

		//If the finger is lifted from the screen
		if (Input.GetMouseButtonUp(0))
		{
			//Shifted to here to solve the aimline and no input named as the "Aditya" bug
			cannon.DoOnButtonUp(); //Makes Aimline disappear only irrespective of whether it's a tap or a drag
			fingerFeedback.EndDrag();

			if ((startDragPosition - endDragPosition).magnitude >= minimumFingerDistance) //If finger drag distance is more than minimum, validate the input
			{
				EndDrag(); //Call enddrag which calls the release ball function and change game state function
			}

		}
	}

	void StartDrag(Vector3 fingerPositionInWorld)
	{
		//Task List
		//Record the starting position of the touch
		startDragPosition = fingerPositionInWorld;

		cannon.DoOnButtonDown();
	}

	void ContinueDrag(Vector3 fingerPositionInWorld)
	{
		//Task List
		//Start storing the current location of the finger in a new variable
		endDragPosition = fingerPositionInWorld;

		float diffX = startDragPosition.x - endDragPosition.x; //Find the difference in x 
		float diffY = Mathf.Max(0.25f, (startDragPosition.y - endDragPosition.y)); //Find the difference in y and selects the maximum of 2 values

		float angleOfRotation = Mathf.Rad2Deg * Mathf.Atan(diffX / diffY); //Get the tan inverse of the differences and convert it to Degrees from Radians
		
		cannon.DoOnButtonHold(angleOfRotation);
	}

	void EndDrag()
	{
		startDragPosition = Vector3.zero;
		endDragPosition = Vector3.zero;
		cannon.ReleaseBallsWithGap(); //Call releasethyballs function from cannon script
		gameManager.ChangeGameState(GameManager.GameStatesDATA.WAIT); //Call changegamestate function and set it to Action
		//gameManager.InputEnded(); //<-- !!! Ref to GM has not been added in class, and I didn't ask them to do it in assignment, hence this is commented
	}

	/// <summary>
	/// Function to check whether the Input manager is active or not
	/// </summary>
	/// <returns>isActive boolean variable</returns>
	public bool IsInputManagerActive() 
	{
		if (this.enabled /*if script component is enabled*/)
		{
			isActive = true; //Set isActive boolean value to true
		}
		else /*if script component is disabled*/
		{
			isActive = false; //Set isActive boolean value to false
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
