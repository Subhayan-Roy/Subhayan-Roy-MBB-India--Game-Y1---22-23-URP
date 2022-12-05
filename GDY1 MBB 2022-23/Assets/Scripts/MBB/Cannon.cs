using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
	public GameObject aimLineGO;    //reference to the Aim Line child GO
	public GameObject ballPrefabGO; //Reference to the ball prefab

	public float ballSpeed = 10f; //Set the speed of the ball, edit in Inspector for a different value
	public int maxBallCount = 10; //Set the maximum number of balls in this level, edit in Inspector for a different value
	public float timeGapBetweenBalls = 0.1f; ////Set the time gap between the balls, edit in Inspector for a different value

	int ballCollisionCounter;

	GameManager gameManager;

    private void Awake()
    {
		aimLineGO.SetActive(false); //Make the Aimline disappear on level load
		gameManager = FindObjectOfType<GameManager>();
	}


    private void Start()
    {
		ResetCannon();
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


	public IEnumerator ReleaseThyBalls()
	{
		for (int i = 0; i < maxBallCount; i++)
		{
			//Instantiate(Blueprint to be spawned, where to spawn it, at what angle to be spawned)
			GameObject spawnedBall = Instantiate(ballPrefabGO, transform.position, Quaternion.identity) as GameObject;
			spawnedBall.GetComponent<Rigidbody2D>().AddForce(transform.up * ballSpeed, ForceMode2D.Impulse);
			yield return new WaitForSeconds(timeGapBetweenBalls);
		}
		
		

	}

	public void ResetCannon()
	{
		ballCollisionCounter = maxBallCount;
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
