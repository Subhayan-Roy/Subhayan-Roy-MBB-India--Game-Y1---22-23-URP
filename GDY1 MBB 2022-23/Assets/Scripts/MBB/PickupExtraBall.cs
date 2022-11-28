using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupExtraBall : MonoBehaviour
{
	private Cannon cannon;

	private void Start()
	{
		cannon = FindObjectOfType<Cannon>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//We check if the colliding object is a Ball
		if (collision.GetComponent<Ball>())
		{
			GetPickedUp();
		}
	}

	private void GetPickedUp()
	{
		cannon.DoOnPickupExtraBall();
		//play pickup fx
		Destroy(gameObject);
	}
}
