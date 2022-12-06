using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
	public GameObject aimLineGO;    //reference to the Aim Line child GO
	public GameObject ballPrefabGO; //Reference to the ball prefab

	public float ballSpeed = 10f; //Set the speed of the ball, edit in Inspector for a different value
	public float timeGapBetweenBalls = 0.1f; ////Set the time gap between the balls, edit in Inspector for a different value

	int ballCollisionCounter;

	GameManager gameManager;
	PoolManager poolManager;

    private void Awake()
    {
		aimLineGO.SetActive(false); //Make the Aimline disappear on level load
		gameManager = FindObjectOfType<GameManager>();
		poolManager = FindObjectOfType<PoolManager>();
	}


    private void Start()
    {
		ResetCannonBallCount();
    }

    public void DoOnButtonDown()
	{
		aimLineGO.SetActive(true);
	}

	public void DoOnButtonHold(float angleOfRotation)
	{
		//Set the angle of the rotation of the transform to the angle calculated
		transform.rotation = Quaternion.Euler(0f, 0f, -angleOfRotation);
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
			spawnedBall.transform.position = transform.position;
			yield return null;
			spawnedBall.GetComponent<Rigidbody2D>().AddForce(transform.up * ballSpeed, ForceMode2D.Impulse);
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
		transform.rotation = Quaternion.Euler(0f, 0f, 0f);
	}

	public void BallCollision(Vector2 pos)
	{
		ballCollisionCounter -= 1;//ballCollisionCounter = ballCollisionCounter - 1 //ballCollisionCounter--

		if (ballCollisionCounter <= 0)
        {
			gameManager.ChangeGameState(GameManager.GameStatesDATA.PREPARATION);
		}
		
	}

	public void DoOnPickupExtraBall()
	{
		
	}
}
