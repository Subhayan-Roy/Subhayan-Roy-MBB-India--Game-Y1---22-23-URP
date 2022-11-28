using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	GameManager gameManager;
	Cannon cannon;

	private void Start()
	{
		//Find the GameManager and Cannon instances in the Scene
		gameManager = FindObjectOfType<GameManager>();
		cannon = FindObjectOfType<Cannon>(); //<-- Eventually we provide this info when the Ball is instantiated by the Cannon
	}

	private void Update()
	{
		if (false /* transform.position.y < bottomLine.position.y */)
		{
			cannon.BallIsOut(transform.position);
		}
	}
}