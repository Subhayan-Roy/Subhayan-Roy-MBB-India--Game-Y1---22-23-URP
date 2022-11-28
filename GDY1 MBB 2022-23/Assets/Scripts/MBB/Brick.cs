using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	public int hp;

	private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
		//Find the GameManager instance in the Scene
		gameManager = FindObjectOfType<GameManager>();

		//The Brick informs the Game Manager that a Brick is in the Scene
		gameManager.BrickIsBorn();
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//We check if the colliding object is a ball
		Ball collidingBall = collision.gameObject.GetComponent<Ball>();
			//returns a reference to the Ball component of the incoming game object
			//(returns null if there is no Ball component)

		if (collidingBall != null) //if collidingBall is not a null object...
		{
			Damage();
		}
	}

	private void Damage()
	{
		hp--;
		
		if (hp <= 0) // <-- !!! This will probably need to be changed to '==' to avoid Die() to be called more than once for a same Brick before it actually disappears
		{
			Die();
		}
	}

	private void Die()
	{
		gameManager.BrickHasDied();
		//instantiate destruction fx
		Destroy(gameObject);
	}
}
