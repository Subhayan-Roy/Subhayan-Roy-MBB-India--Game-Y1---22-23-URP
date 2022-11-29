using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public enum GameStatesDATA //Define a custom data type which can ONLY accept the values in the following code block
	{
		PREPARATION,
		WAIT,
		ACTION,
		LEVELVICTORY,
		LEVELDEFEAT,
		GAMEVICTORY
	}

	public GameStatesDATA gameStatesVAR; //Make a public variable out of our custom Data Type, GameStatesDATA

	InputManager inputManager;

    private void Awake()
    {
		gameStatesVAR = GameStatesDATA.PREPARATION; //Make sure the default state is set to Preparation State
		inputManager = this.GetComponent<InputManager>(); //Access the the script InputManager script component attached to the Managers GameObject
    }

    private void Update()
    {
		switch (gameStatesVAR)
		{
			case GameStatesDATA.PREPARATION:
				if (!inputManager.enabled /*if input manager is disabled*/)
				{
					inputManager.enabled = true; //then enable it to make sure it runs only ONCE
				}
				break;

			case GameStatesDATA.WAIT:

				break;

			case GameStatesDATA.ACTION:
				if (inputManager.enabled /*if input manager is enabled*/)
				{
					inputManager.enabled = false; //then disable it to make sure it runs only ONCE
				}
				break;

			case GameStatesDATA.LEVELVICTORY:
				break;

			case GameStatesDATA.LEVELDEFEAT:
				break;

			case GameStatesDATA.GAMEVICTORY:
				break;

		}
    }

    public void BrickIsBorn()
	{
		
	}

	public void BrickHasDied()
	{
		
	}

	public void ChangeGameState(GameStatesDATA gameState)
	{
		gameStatesVAR = gameState;
	}
}