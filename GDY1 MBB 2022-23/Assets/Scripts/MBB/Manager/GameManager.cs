using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	InputManager inputManager;
	UIManager uiManager;
	UpdatedCannon cannon;
	bool isInActiveState;
	
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

	[Header("Game State")]
	public GameStatesDATA gameStatesVAR; //Make a public variable out of our custom Data Type, GameStatesDATA

	[Header("Initial Settings")]
	public float gameStartTime = 3f;

	[Header("Rounds Settings")]
	public int maxNumberOfRounds = 10;
	public int currentRound;

	[Header("Bricks Settings")]
	public int brickCount;

	private void Awake()
    {
		gameStatesVAR = GameStatesDATA.GAMESTART; //Make sure the default state is set to Preparation State
		inputManager = this.GetComponent<InputManager>(); //Access the the script InputManager script component attached to the Managers GameObject
		uiManager = this.GetComponent<UIManager>();
		cannon = FindObjectOfType<UpdatedCannon>();

		
		isInActiveState = false;
	}

    private void Start()
    {
		StartCoroutine(StartGameCountDown());
		uiManager.MakeUIObjectInactive(uiManager.recallButtonGO);
		uiManager.MakeUIObjectInactive(uiManager.victoryPanelGO);
		uiManager.MakeUIObjectInactive(uiManager.losePanelGO);
		uiManager.SetLevelNumber(SceneManager.GetActiveScene().buildIndex);
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
				if (brickCount != 0)
				{
					if (currentRound <= maxNumberOfRounds)
					{
						if (!inputManager.IsInputManagerActive() /*if input manager is disabled*/)
						{
							cannon.ResetCannonBallCount();
							cannon.CannonMove();
							isInActiveState = false;
							inputManager.EnableInputManager(); //then enable it to make sure it runs only ONCE
							uiManager.MakeUIObjectInactive(uiManager.recallButtonGO);

							currentRound += 1;
							uiManager.SetRoundNumber(currentRound);
						}
					}
					else if (currentRound > maxNumberOfRounds)
					{
						if (gameStatesVAR != GameStatesDATA.LEVELDEFEAT)
						{
							gameStatesVAR = GameStatesDATA.LEVELDEFEAT;
							break;
						}
					}
				}
				else if (brickCount == 0)
				{
					if (gameStatesVAR != GameStatesDATA.LEVELVICTORY)
					{
						gameStatesVAR = GameStatesDATA.LEVELVICTORY;
						break;
					}
				}
				
				break;

			case GameStatesDATA.WAIT:
				if (inputManager.IsInputManagerActive() /*if input manager is enabled*/)
				{
					inputManager.DisableInputManager(); //then disable it to make sure it runs only ONCE
				
				}
				break;

			case GameStatesDATA.ACTION:
				if (!isInActiveState)
				{
					isInActiveState = true;
					cannon.ResetCannonAngle();
					uiManager.MakeUIObjectActive(uiManager.recallButtonGO);
				}
				break;

			case GameStatesDATA.LEVELVICTORY:

				if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings - 1)
				{
					SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1, LoadSceneMode.Single);
				}
				else
				{
					if (!uiManager.victoryPanelGO.activeInHierarchy)
					{
						uiManager.MakeUIObjectActive(uiManager.victoryPanelGO);
					}
				}
				

				break;

			case GameStatesDATA.LEVELDEFEAT:

                if (inputManager.IsInputManagerActive() /*if input manager is enabled*/)
                {
                    inputManager.DisableInputManager(); //then disable it to make sure it runs only ONCE
                    uiManager.MakeUIObjectActive(uiManager.losePanelGO);

                }
				

				break;

			case GameStatesDATA.GAMEVICTORY:
				break;

		}
    }

    public void BrickIsBorn()
	{
		brickCount += 1;
	}

	public void BrickHasDied()
	{
		brickCount -= 1;
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
		uiManager.StartCoroutine(uiManager.CountDown());

		yield return new WaitForSeconds(gameStartTime); //This is the syntax to remember. Put the time you want to wait for within the parentheses

		gameStatesVAR = GameStatesDATA.PREPARATION; //Change the state of the game manager to Preparation
	}
}