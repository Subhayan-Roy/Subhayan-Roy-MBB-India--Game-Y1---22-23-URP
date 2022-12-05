using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public enum GameStatesDATA //Define a custom data type which can ONLY accept the values in the following code block
	{
		GAMESTART,
		PREPARATION,
		WAIT,
		ACTION,
		LEVELVICTORY,
		LEVELDEFEAT,
		GAMEVICTORY
	}

	public GameStatesDATA gameStatesVAR; //Make a public variable out of our custom Data Type, GameStatesDATA

	InputManager inputManager;
	Cannon cannon;

	public float gameStartTime = 3f;

    private void Awake()
    {
		gameStatesVAR = GameStatesDATA.GAMESTART; //Make sure the default state is set to Preparation State
		inputManager = this.GetComponent<InputManager>(); //Access the the script InputManager script component attached to the Managers GameObject
		cannon = FindObjectOfType<Cannon>();
	}

    private void Start()
    {
		StartCoroutine(StartGameCountDown());
    }

    private void Update()
    {
		switch (gameStatesVAR)
		{
			case GameStatesDATA.GAMESTART:
				if (inputManager.IsInputManagerActive() /*if input manager is enabled*/)
				{
					inputManager.DisableInputManager(); //then disable it to make sure it runs only ONCE
				}
				break;

			case GameStatesDATA.PREPARATION:
				if (!inputManager.IsInputManagerActive() /*if input manager is disabled*/)
				{
					cannon.ResetCannon();
					inputManager.EnableInputManager(); //then enable it to make sure it runs only ONCE
				}
				break;

			case GameStatesDATA.WAIT:

				break;

			case GameStatesDATA.ACTION:
				if (inputManager.IsInputManagerActive() /*if input manager is enabled*/)
				{
					inputManager.DisableInputManager(); //then disable it to make sure it runs only ONCE
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

	/// <summary>
	/// Activates a timer at the beginning of the level before the start of the game. Define a function of type IENumerator
	/// </summary>
	IEnumerator StartGameCountDown()
	{

		yield return new WaitForSeconds(gameStartTime); //This is the syntax to remember. Put the time you want to wait for within the parentheses

		gameStatesVAR = GameStatesDATA.PREPARATION; //Change the state of the game manager to Preparation
	}
}