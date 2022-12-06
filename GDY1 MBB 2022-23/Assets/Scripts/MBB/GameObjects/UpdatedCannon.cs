using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedCannon : MonoBehaviour
{

    public Transform thingToRotate;    //reference to the thing to rotate gameobject
    public GameObject aimLineGO;       //reference to the Aim Line child GO

    public float ballSpeed = 10f; //Set the speed of the ball, edit in Inspector for a different value
    public float timeGapBetweenBalls = 0.1f; ////Set the time gap between the balls, edit in Inspector for a different value

    int ballCollisionCounter;

    GameManager gameManager;
    PoolManager poolManager;

	Vector3 newCannonPosition;

	public float firstMovementPercentage = 25f;

	Vector3 screenWidth;
	Vector3 screenCenter;

	Vector3 percentageCoordinates;
	Vector3 percentageCoordinatesInUnits;

    private void Awake()
    {
        aimLineGO.SetActive(false); //Make the Aimline disappear on level load
        gameManager = FindObjectOfType<GameManager>();
        poolManager = FindObjectOfType<PoolManager>();

		FirstRandomization();
    }

    private void Start()
    {
        ResetCannonBallCount();
	}

    private void Update()
    {

    }

    public void DoOnButtonDown()
    {
        aimLineGO.SetActive(true);
    }

    public void DoOnButtonHold(float angleOfRotation)
    {
        //Set the angle of the rotation of the transform to the angle calculated
        thingToRotate.rotation = Quaternion.Euler(0f, 0f, -angleOfRotation);
    }

	public void DoOnButtonUp()
	{
		aimLineGO.SetActive(false); //Make aimline disappear

	}

	public void ReleaseBallsWithGap()
	{
		StartCoroutine(ReleaseThyBalls());
	}

	/// <summary>
	/// Fetch balls from balls list, set the position of the ball to the cannon's position and then 
	/// shoot the balls in an upward direction compared to the cannon
	/// </summary>
	/// <returns></returns>
	public IEnumerator ReleaseThyBalls()
	{
		for (int i = 0; i < poolManager.maxBallCount; i++)
		{
			//Instantiate(Blueprint to be spawned, where to spawn it, at what angle to be spawned)
			GameObject spawnedBall = poolManager.FetchBallFromList();
			spawnedBall.transform.position = thingToRotate.position;
			yield return null;
			spawnedBall.GetComponent<Rigidbody2D>().AddForce(thingToRotate.up * ballSpeed, ForceMode2D.Impulse);
			yield return new WaitForSeconds(timeGapBetweenBalls);
		}

		yield return new WaitForSeconds(0.25f);

		gameManager.ChangeGameState(GameManager.GameStatesDATA.ACTION);

	}

	public void ResetCannonBallCount()
	{
		ballCollisionCounter = poolManager.maxBallCount;
	}

	public void ResetCannonAngle()
	{
		thingToRotate.rotation = Quaternion.Euler(0f, 0f, 0f);
	}

	public void BallCollision(Vector2 pos)
	{
		ballCollisionCounter -= 1;//ballCollisionCounter = ballCollisionCounter - 1 //ballCollisionCounter--

		if (ballCollisionCounter == poolManager.maxBallCount - 1)
		{
			newCannonPosition = new Vector3(pos.x,newCannonPosition.y,newCannonPosition.z);
		}

		if (ballCollisionCounter <= 0)
		{
			gameManager.ChangeGameState(GameManager.GameStatesDATA.PREPARATION);
		}

	}

	public void CannonMove()
	{
		transform.position = newCannonPosition;
	}

	/// <summary>
	/// Get a vector (in pixels) till the right end of the screen
	/// Find the center of the screen by dividing the width by 2 (in pixel)
	/// Find a coordinate (in pixel) which is at a percentage distance (default is 25) from the center of the screen
	/// Convert the pixel coordinate into units
	/// Randomize the x coordinate of the percentage coordinate between negative(left) and positive(right)
	/// Assign the x component of the percentage coordinate to new cannon position while keeping y and z component same as cannon's
	/// </summary>
	void FirstRandomization()
	{
		screenWidth = new Vector3(Screen.width, 0f, 0f);
		screenCenter = screenWidth / 2;
		percentageCoordinates = screenCenter + (firstMovementPercentage / 100) * screenWidth;
		percentageCoordinatesInUnits = Camera.main.ScreenToWorldPoint(percentageCoordinates);
		newCannonPosition = new Vector3(Random.Range(-percentageCoordinatesInUnits.x, percentageCoordinatesInUnits.x), transform.position.y, transform.position.z);
	}

	public void DoOnPickupExtraBall()
	{

	}
}
