using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
	public GameObject aimLineGO;    //reference to the Aim Line child GO
	public GameObject ballPrefabGO; //Reference to the ball prefab

	GameManager gameManager;

    private void Awake()
    {
		aimLineGO.SetActive(false); //Make the Aimline disappear on level load
		gameManager = FindObjectOfType<GameManager>();
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
		ReleaseThyBalls();
	}

	public void ReleaseThyBalls()
	{
		//Instantiate(Blueprint to be spawned, where to spawn it, at what angle to be spawned)
		GameObject spawnedBall = Instantiate(ballPrefabGO, transform.position, Quaternion.identity) as GameObject;
		spawnedBall.GetComponent<Rigidbody2D>().AddForce(transform.up * 10f, ForceMode2D.Impulse);
		gameManager.ChangeGameState(GameManager.GameStatesDATA.ACTION);

	}

	public void BallCollision(Vector2 pos)
	{
		gameManager.ChangeGameState(GameManager.GameStatesDATA.PREPARATION);
	}

	public void DoOnPickupExtraBall()
	{
		
	}
}
