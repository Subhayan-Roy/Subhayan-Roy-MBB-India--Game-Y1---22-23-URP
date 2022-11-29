using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	//[SerializeField] private GameManager gameManager; //<-- !!! This has not been done in class
	public float minimumFingerDistance = 0.25f; //minimum finger distance travelled needed to validate the input

	Vector3 locationOfFinger;   //stores the current location of finger
	Vector3 startDragPosition;  //stores the position of the first touch
	Vector3 endDragPosition;	//stores the position of the last touch

	private Cannon cannon; //reference to the Cannon instance

	public void Start()
	{
		cannon = FindObjectOfType<Cannon>();
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
			

			if ((startDragPosition - endDragPosition).magnitude >= minimumFingerDistance) //If finger drag distance is more than minimum, validate the input
			{
				EndDrag();
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
		cannon.DoOnButtonUp();
		//gameManager.InputEnded(); //<-- !!! Ref to GM has not been added in class, and I didn't ask them to do it in assignment, hence this is commented
	}
}
