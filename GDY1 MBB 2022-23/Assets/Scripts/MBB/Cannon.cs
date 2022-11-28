using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
	public GameObject aimLineGO;	//reference to the Aim Line child GO

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

	public void BallIsOut(Vector2 pos)
	{

	}

	public void DoOnPickupExtraBall()
	{
		
	}
}
